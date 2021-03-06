using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryView : View
{
    public int pointerIndex { get; set; }

    [SerializeField]
    protected Item[] items;

    private new void Awake()
    {
        base.Awake();
        pointerIndex = slots[0].slotIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 13; i++)
        {
            slots[i].SetItemInSlot(Instantiate(items[Random.Range(1, items.Length)]));
        }
    }
}
