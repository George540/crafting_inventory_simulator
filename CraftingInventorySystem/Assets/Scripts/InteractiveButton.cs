using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private bool _interactable = true;

    private InventoryView _inventoryView;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.selectedObject.TryGetComponent<Slot>(out var slot))
        {
            if (slot.reservedItem.TryGetComponent<Tool>(out var tool))
            {
                
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
        
    }
}
