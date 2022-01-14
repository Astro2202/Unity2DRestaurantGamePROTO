using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public Consumables.FoodType FoodType { get; }
    public Consumables.DrinkType DrinkType { get; }
    public bool HasOrdered { get; }

    public Order(Consumables.FoodType foodType, Consumables.DrinkType drinkType)
    {
        this.FoodType = foodType;
        this.DrinkType = drinkType;
        HasOrdered = true;
    }

    public override string ToString()
    {
        return "Order; food type: " + this.FoodType.ToString() + ", drink type: " + this.DrinkType.ToString();
    }
}
