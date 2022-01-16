using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMenu : MonoBehaviour
{
    public Drink drinkPrefab;
    public Sprite[] drinkSpriteArray;
    private const int DRINKPHASES = 2;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void OpenDrinkMenu()
    {
        gameObject.SetActive(true);
    }

    internal void CloseDrinkMenu()
    {
        gameObject.SetActive(false);
    }
    public void SetDrinkCoke()
    {
        Drink drink = Instantiate(drinkPrefab);
        drink.drinkType = Consumables.DrinkType.Coke;
        drink.transform.parent = transform;
        drink.spriteRenderer.sprite = drinkSpriteArray[0];
        drink.spriteRenderer.sortingOrder = 27;
        List<Sprite> sprites = new List<Sprite>();
        for (int i = 0; i < DRINKPHASES; i++)
        {
            sprites.Add(drinkSpriteArray[i]);
        }
        drink.spriteArray = sprites.ToArray();
        player.SetDrink(drink);
    }
    public void SetDrinkLime()
    {
        Drink drink = Instantiate(drinkPrefab);
        drink.drinkType = Consumables.DrinkType.Lime;
        drink.transform.parent = transform;
        drink.spriteRenderer.sprite = drinkSpriteArray[2];
        drink.spriteRenderer.sortingOrder = 27;
        List<Sprite> sprites = new List<Sprite>();
        for (int i = 0; i < DRINKPHASES; i++)
        {
            sprites.Add(drinkSpriteArray[i + 2]);
        }
        drink.spriteArray = sprites.ToArray();
        player.SetDrink(drink);
    }
    public void SetDrinkOrange()
    {
        Drink drink = Instantiate(drinkPrefab);
        drink.drinkType = Consumables.DrinkType.Orange;
        drink.transform.parent = transform;
        drink.spriteRenderer.sprite = drinkSpriteArray[4];
        drink.spriteRenderer.sortingOrder = 27;
        List<Sprite> sprites = new List<Sprite>();
        for (int i = 0; i < DRINKPHASES; i++)
        {
            sprites.Add(drinkSpriteArray[i + 4]);
        }
        drink.spriteArray = sprites.ToArray();
        player.SetDrink(drink);
    }



}
