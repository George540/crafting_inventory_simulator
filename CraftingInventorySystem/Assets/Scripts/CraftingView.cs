using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CraftingView : MonoBehaviour
{
    [SerializeField]
    Slot[] _craftingSlots;
    [SerializeField]
    List<Recipe> _recipes;

    Slot _resultSlot;

    private void Start()
    {
        _craftingSlots = GetComponentsInChildren<Slot>();
        for (int i = 0; i < _craftingSlots.Length; i++)
        {
            _craftingSlots[i].slotIndex = i;
        }
    }

    public Item CheckForRecipe(Recipe recipe)
    {
        int requiredChecks = recipe.GetBlueprint().Count;
        int checks = 0;
        for (int i = 0; i < _craftingSlots.Length; i++)
        {
            if (_craftingSlots[i].reservedItem != null)
            {
                if (recipe.GetBlueprint().Any(itemAtSlot => itemAtSlot.index == i))
                {
                    if (_craftingSlots[i].reservedItem.GetName() == recipe.GetBlueprint()[i].itemAtSlot.GetName())
                    {
                        checks++;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        return requiredChecks == checks ? recipe.GetResultItem() : null;
    }

    public List<Recipe> GetRecipes()
    {
        return _recipes;
    }

    public Slot GetResultSlot()
    {
        return _resultSlot;
    }
}
