using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public Consumables.FoodType FoodType { get; }
    public Consumables.DrinkType DrinkType { get; }

    private bool orderCompleted;

    public bool OrderCompleted
    {
        get { return orderCompleted; }
    }

    private bool drinkOrderCompleted;

    public bool DrinkOrderCompleted
    {
        get { return drinkOrderCompleted; }
        set
        {
            drinkOrderCompleted = value;
            if (foodOrderCompleted)
            {
                orderCompleted = true;
            }
        }
    }

    private bool foodOrderCompleted;

    public bool FoodOrderCompleted
    {
        get { return foodOrderCompleted; }
        set
        {
            foodOrderCompleted = value;
            if (drinkOrderCompleted)
            {
                orderCompleted = true;
            }
        }
    }

    public Order(Consumables.FoodType foodType, Consumables.DrinkType drinkType)
    {
        this.FoodType = foodType;
        this.DrinkType = drinkType;
    }

    public override string ToString()
    {
        return "Order; food type: " + this.FoodType.ToString() + ", drink type: " + this.DrinkType.ToString();
    }
}
