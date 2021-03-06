using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private bool _interactable = true;

    [SerializeField]
    private Canvas _canvas;

    public InventoryView _inventoryView;
    public CraftingView _craftingView;

    private const int maxMaterialStackAmount = 15;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_inventoryView != null)
        {
            eventData.pointerEnter.TryGetComponent<Slot>(out var slot);
            if (slot != null && eventData.button == PointerEventData.InputButton.Left)
            {
                HandleLeftMouseButtonEvents(slot);
            }
            else if (slot != null && eventData.button == PointerEventData.InputButton.Right)
            {
                HandleRightMouseButtonEvents(slot);
            }
        }
    }

    private void HandleLeftMouseButtonEvents(Slot slot)
    {
        if (_inventoryView.currentItemHeld == null)
        {
            _inventoryView.currentItemHeld = slot.RemoveItemInSlot();
            _inventoryView.currentItemHeld.gameObject.transform.SetParent(FindObjectOfType<Canvas>().gameObject.transform);
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
                if (slot.reservedItem.gameObject.GetComponent<Material>() != null && _inventoryView.currentItemHeld.gameObject.GetComponent<Material>() != null)
                {
                    HandleMaterialSwapping(slot);
                    InstantiateResultObject();
                }
                else
                {
                    HandleGeneralSwapping(slot);
                    InstantiateResultObject();
                }
            }

            if (slot.reservedItem != null)
            {
                slot.reservedItem.gameObject.transform.SetParent(slot.transform);
                slot.reservedItem.gameObject.transform.position = slot.transform.position;
            }
        }
    }

    private void HandleRightMouseButtonEvents(Slot slot)
    {
        if (_inventoryView.currentItemHeld != null && _inventoryView.currentItemHeld.TryGetComponent<Material>(out var inventoryMaterial))
        {
            if (slot.reservedItem == null)
            {
                slot.reservedItem = Instantiate(inventoryMaterial);
                slot.reservedItem.gameObject.transform.SetParent(slot.gameObject.transform);
                slot.reservedItem.gameObject.transform.localPosition = Vector3.zero;
                slot.reservedItem.gameObject.GetComponent<Material>().isTest = false;
                slot.reservedItem.gameObject.GetComponent<Material>().SetAmount(1);
                DecrementInventoryMaterial(inventoryMaterial);
            }
            else
            {
                var slotMaterial = slot.reservedItem.gameObject.GetComponent<Material>();
                if (slotMaterial.GetAmount() < maxMaterialStackAmount)
                {
                    slotMaterial.IncrementAmount();
                    DecrementInventoryMaterial(inventoryMaterial);
                }
            }
        }
        else if (_inventoryView.currentItemHeld == null && slot.reservedItem.TryGetComponent<Material>(out var slotMaterial))
        {
            if (slot.reservedItem != null)
            {
                _inventoryView.currentItemHeld = Instantiate(slotMaterial);
                _inventoryView.currentItemHeld.gameObject.transform.SetParent(FindObjectOfType<Canvas>().gameObject.transform);
                var inventoryNewMaterial = _inventoryView.currentItemHeld.gameObject.GetComponent<Material>();
                inventoryNewMaterial.isTest = false;
                inventoryNewMaterial.SetAmount(slotMaterial.GetAmount()/2);
                slotMaterial.SetAmount(slotMaterial.GetAmount() - inventoryNewMaterial.GetAmount());
            }
        }
    }

    private void DecrementInventoryMaterial(Material material)
    {
        if (material.GetAmount() > 1)
        {
            material.DecrementAmount();
        }
        else
        {
            Destroy(_inventoryView.currentItemHeld.gameObject);
            _inventoryView.currentItemHeld = null;
        }
    }

    private void HandleGeneralSwapping(Slot slot)
    {
        var tempItem = slot.reservedItem;
        slot.RemoveItemInSlot();
        slot.reservedItem = _inventoryView.currentItemHeld;
        _inventoryView.currentItemHeld = tempItem;
        _inventoryView.currentItemHeld.gameObject.transform.SetParent(FindObjectOfType<Canvas>().gameObject.transform);
    }

    private void HandleMaterialSwapping(Slot slot)
    {
        var slotMaterial = slot.reservedItem.GetComponent<Material>();
        var hoverMaterial = _inventoryView.currentItemHeld.GetComponent<Material>();

        if (slotMaterial.GetName().Equals(hoverMaterial.GetName()))
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
        if (_inventoryView != null && eventData.pointerEnter.TryGetComponent<Slot>(out var slot))
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
        
    }

    // Update is called once per frame
    void Update()
    {
        SetItemToCursor();
    }

    private void SetItemToCursor()
    {
        if (_inventoryView != null && _inventoryView.currentItemHeld != null)
        {
            _inventoryView.currentItemHeld.transform.position = Input.mousePosition;
        }
    }


    private void InstantiateResultObject()
    {
        if (CheckForRecipes(_craftingView.GetRecipes()) != null)
        {
            GameObject resultItem = Instantiate(CheckForRecipes(_craftingView.GetRecipes()).gameObject, _craftingView.GetResultSlot().transform);
            _craftingView.GetResultSlot().reservedItem = resultItem.GetComponent<Item>();
            _craftingView.GetResultSlot().reservedItem.gameObject.transform.SetParent(_craftingView.GetResultSlot().gameObject.transform);
        }
    }
    private Item CheckForRecipes(List<Recipe> recipes)
    {
        foreach (var recipe in recipes)
        {
            if (_craftingView.CheckForRecipe(recipe) != null)
            {
                return _craftingView.CheckForRecipe(recipe);
            }
        }
        return null;
    }
}
