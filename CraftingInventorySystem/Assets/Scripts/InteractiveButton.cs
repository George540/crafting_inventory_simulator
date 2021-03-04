using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private bool _interactable = true;

    [SerializeField]
    private Canvas _canvas;

    private InventoryView _inventoryView;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerEnter.TryGetComponent<Slot>(out var slot))
        {
            if (_inventoryView.currentItemHeld == null)
            {
                _inventoryView.currentItemHeld = slot.reservedItem;
                _inventoryView.currentItemHeld.gameObject.transform.parent = FindObjectOfType<Canvas>().gameObject.transform;
                slot.RemoveItemInSlot();
            }
            else
            {
                if (slot.reservedItem == null)
                {
                    slot.reservedItem = _inventoryView.currentItemHeld;
                    _inventoryView.currentItemHeld = null;
                    slot.reservedItem.gameObject.transform.parent = slot.transform;
                    slot.reservedItem.gameObject.transform.position = slot.transform.position;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.TryGetComponent<Slot>(out var slot))
        {
            _inventoryView.currentHoveredIndex = slot.slotIndex;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _inventoryView.currentHoveredIndex = -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        _inventoryView = FindObjectOfType<InventoryView>();
    }

    // Update is called once per frame
    void Update()
    {
        SetItemToCursor();
    }

    private void SetItemToCursor()
    {
        if (_inventoryView.currentItemHeld != null)
        {
            _inventoryView.currentItemHeld.transform.position = Input.mousePosition;
        }
    }
}
