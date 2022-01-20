using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serving
{
    private Food food;
    private Drink drink;

    public Serving(Food food, Drink drink)
    {
        this.food = food;
        this.drink = drink;
    }

    public Food GetFood()
    {
        return food;
    }

    public Drink GetDrink()
    {
        return drink;
    }
}
