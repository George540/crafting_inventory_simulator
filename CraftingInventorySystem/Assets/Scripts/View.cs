using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class View : MonoBehaviour
{
    protected Slot[] slots;

    [SerializeField]
    private GameObject _craftingView;
    
    [SerializeField]
    public int currentHoveredIndex;
    public Item currentItemHeld { get; set; }

    protected void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotIndex = i;
        }

        var craftingSlots = _craftingView.GetComponentsInChildren<Slot>();
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            craftingSlots[i].slotIndex = slots.Length + i;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(Item newItem, int slotIndex)
    {
        if (newItem == null)
        {
            return;
        }
        slots[slotIndex].SetItemInSlot(newItem);
    }

    public void RemoveItem(int slotIndex)
    {
        if (!slots[slotIndex].IsSlotReserved())
        {
            slots[slotIndex].RemoveItemInSlot();
        }
    }

   /* public bool IsItemInInventory(Item newItem)
    {
        if (newItem == null)
        {
            return false;
        }
        return items.Any(item => item == newItem);
    }*/

    public void SetItemToCursor()
    {

    }
}
