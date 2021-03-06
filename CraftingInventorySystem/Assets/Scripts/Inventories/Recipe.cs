using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    [SerializeField]
    private List<ItemAtSlot> blueprint;
    [SerializeField]
    Item _resultItem;

    public List<ItemAtSlot> GetBlueprint()
    {
        return blueprint;
    }

    public Item GetResultItem()
    {
        return _resultItem;
    }
}

[Serializable]
public struct ItemAtSlot
{
    public Item itemAtSlot;
    public int index;

    public ItemAtSlot(Item item, int i)
    {
        itemAtSlot = item;
        index = i;
    }
}
