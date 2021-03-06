using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Item reservedItem { get; set; }
    public int slotIndex { get; set; }

    public bool IsSlotReserved()
    {
        return reservedItem != null;
    }

    public void SetItemInSlot(Item newItem)
    {
        reservedItem = newItem;
        reservedItem.gameObject.transform.SetParent(gameObject.transform);
        reservedItem.gameObject.transform.position = gameObject.transform.position;
    }

    public Item RemoveItemInSlot()
    {
        var item = reservedItem;
        reservedItem = null;
        return item;
    }
}
