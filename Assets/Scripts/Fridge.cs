using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour, IInteractable
{
    internal Player player;
    private bool grabbedDrink = false;
    private Drink drink;
    public Sprite[] drinkSpriteArray;
    private const int DRINKPHASES = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public InteractableEnum.Interactables GetInteractable()
    {
        return InteractableEnum.Interactables.Fridge;
    }

    public float GetInteractableDuration()
    {
        return 3;
    }

    public float GetXPosition()
    {
        return transform.position.x;
    }
    private void OnMouseDown()
    {
        player.GoInteract(this);
    }

    public Drink GetDrink()
    {
        grabbedDrink = true;
        return drink;
    }

    public bool GrabbedDrink()
    {
        return grabbedDrink;
    }

    public void SetDrinkCoke()
    {
        grabbedDrink = false;
        drink.drinkType = Consumables.DrinkType.Coke;
        drink.transform.parent = transform;
        List<Sprite> sprites = new List<Sprite>();
        for (int i = 1; i < DRINKPHASES; i++)
        {
            sprites.Add(drinkSpriteArray[i]);
        }
        drink.spriteArray = sprites.ToArray();
    }
}
