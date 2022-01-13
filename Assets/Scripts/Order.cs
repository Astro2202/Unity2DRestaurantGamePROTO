using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public Consumables.FoodType FoodType { get; }
    public Consumables.DrinkType DrinkType { get; }
    public bool HasOrdered { get; }

    public Order(Consumables.FoodType foodType)
    {
        this.FoodType = foodType;
        HasOrdered = true;
    }

    public override string ToString()
    {
        return "Order food type: " + this.FoodType.ToString();
    }
}
