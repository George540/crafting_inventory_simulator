using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : Item, IStackable
{
    private int amount;

    public void StackItem(Material otherItem)
    {
        amount += otherItem.GetAmount();
    }

    public int GetAmount()
    {
        return amount;
    }

    public void SetAmount(int newAmount)
    {
        amount = newAmount;
    }

    public void AddAmount(int newAmount)
    {
        amount += newAmount;
    }
}
