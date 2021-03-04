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

    private const int maxMaterialStackAmount = 15;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerEnter.TryGetComponent<Slot>(out var slot) && eventData.button == PointerEventData.InputButton.Left)
        {
            if (_inventoryView.currentItemHeld == null)
            {
                _inventoryView.currentItemHeld = slot.RemoveItemInSlot();
                _inventoryView.currentItemHeld.gameObject.transform.parent = FindObjectOfType<Canvas>().gameObject.transform;
            }
            else
            {
                if (slot.reservedItem == null)
                {
                    slot.reservedItem = _inventoryView.currentItemHeld;
                    _inventoryView.currentItemHeld = null;

                }
                else
                {
                    if (slot.reservedItem.GetComponent<Material>() != null && _inventoryView.currentItemHeld.GetComponent<Material>() != null)
                    {
                        HandleMaterialSwapping(slot);
                    }
                    else
                    {
                        HandleGeneralSwapping(slot);
                    }
                }

                if (slot.reservedItem != null)
                {
                    slot.reservedItem.gameObject.transform.parent = slot.transform;
                    slot.reservedItem.gameObject.transform.position = slot.transform.position;
                }
            }
        }
    }

    private void HandleGeneralSwapping(Slot slot)
    {
        var tempItem = slot.reservedItem;
        slot.RemoveItemInSlot();
        slot.reservedItem = _inventoryView.currentItemHeld;
        _inventoryView.currentItemHeld = tempItem;
        _inventoryView.currentItemHeld.gameObject.transform.parent = FindObjectOfType<Canvas>().gameObject.transform;
    }

    private void HandleMaterialSwapping(Slot slot)
    {
        var slotMaterial = slot.reservedItem.GetComponent<Material>();
        var hoverMaterial = _inventoryView.currentItemHeld.GetComponent<Material>();

        if (slotMaterial.name.Equals(hoverMaterial.name))
        {
            var additionalAmount = hoverMaterial.GetAmount();
            if (slot.reservedItem.GetComponent<Material>().GetAmount() + additionalAmount <= maxMaterialStackAmount)
            {
                slot.reservedItem.GetComponent<Material>().AddAmount(additionalAmount);
                Destroy(_inventoryView.currentItemHeld.gameObject);
                _inventoryView.currentItemHeld = null;
            }
            else
            {
                var requiredAmountForSlotFilled = maxMaterialStackAmount - slotMaterial.GetAmount();
                hoverMaterial.RemoveAmount(requiredAmountForSlotFilled);
                slotMaterial.SetAmount(maxMaterialStackAmount);
            }
        }
        else
        {
            HandleGeneralSwapping(slot);
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
