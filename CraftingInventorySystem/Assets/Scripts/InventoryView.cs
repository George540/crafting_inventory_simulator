using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    private Slot[] slots;

    private Item[] items;
    public int pointerIndex { get; set; }

    public int currentHoveredIndex;

    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotIndex = i;
        }
        pointerIndex = slots[0].slotIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
        slots[slotIndex].SetItemInSlot(newItem, slotIndex);
    }

    public void RemoveItem(int slotIndex)
    {
        if (!slots[slotIndex].IsSlotReserved())
        {
            slots[slotIndex].RemoveItemInSlot();
        }
    }

    public bool IsItemInInventory(Item newItem)
    {
        if (newItem == null)
        {
            return false;
        }
        return items.Any(item => item == newItem);
    }
}
