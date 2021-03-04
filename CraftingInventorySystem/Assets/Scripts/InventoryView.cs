using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    private Slot[] slots;
    
    [SerializeField]
    private Item[] items;
    public int pointerIndex { get; set; }

    [SerializeField]
    public int currentHoveredIndex;
    public Item currentItemHeld { get; set; }

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
        for (int i = 0; i < 5; i++)
        {
            slots[i].SetItemInSlot(Instantiate(items[Random.Range(1, items.Length)]));
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

    public bool IsItemInInventory(Item newItem)
    {
        if (newItem == null)
        {
            return false;
        }
        return items.Any(item => item == newItem);
    }

    public void SetItemToCursor()
    {

    }
}
