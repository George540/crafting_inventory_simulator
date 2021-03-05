using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Material : Item, IStackable
{
    [SerializeField]
    private int _amount;

    [SerializeField]
    private TMP_Text _amountTextField;

    public bool isTest = true;

    private void Start()
    {
        if (isTest)
            SetAmount(Random.Range(4, 13));
    }

    public void SetAmountTextField(int amount)
    {
        _amountTextField.text = $"{amount}";
    }

    public void StackItem(Material otherItem)
    {
        _amount += otherItem.GetAmount();
        SetAmountTextField(_amount);
    }

    public int GetAmount()
    {
        return _amount;
    }

    public void SetAmount(int newAmount)
    {
        _amount = newAmount;
        SetAmountTextField(_amount);
    }

    public void AddAmount(int amount)
    {
        _amount += amount;
        SetAmountTextField(_amount);
    }

    public void RemoveAmount(int amount)
    {
        _amount -= amount;
        SetAmountTextField(_amount);
    }

    public void IncrementAmount()
    {
        _amount++;
        SetAmountTextField(_amount);
    }

    public void DecrementAmount()
    {
        _amount--;
        SetAmountTextField(_amount);
    }
}
