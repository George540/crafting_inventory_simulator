using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Item reservedItem { get; set; }
    public int slotIndex { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsSlotReserved()
    {
        return reservedItem != null;
    }

    public void SetItemInSlot(Item newItem, int slotIndex)
    {
        reservedItem = newItem;
        this.slotIndex = slotIndex;
    }

    public Item RemoveItemInSlot()
    {
        var item = reservedItem;
        reservedItem = null;
        return item;
    }
}
