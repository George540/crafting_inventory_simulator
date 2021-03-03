using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string _name;

    private void Start()
    {
        
    }

    public string GetName()
    {
        return _name;
    }
}
