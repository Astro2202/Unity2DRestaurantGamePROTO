using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour, IInteractable
{
    internal Player player;
    public Sprite[] foodSpriteArray;
    public Food foodPrefab;
    private const int FOODPHASES = 3;
    internal bool foodReady = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInChildren<Food>() != null)
        {
            foodReady = true;
        }
        else
        {
            foodReady = false;
        }
    }
    public InteractableEnum.Interactables GetInteractable()
    {
        return InteractableEnum.Interactables.Kitchen;
    }

    public float GetInteractableDuration()
    {
        return 1;
    }

    public float GetXPosition()
    {
        return transform.position.x;
    }

    public void CookSpaghetti()
    {
        Food food = Instantiate(foodPrefab);
        food.foodType = Consumables.FoodType.Pasta;
        food.transform.parent = transform;
        List<Sprite> sprites = new List<Sprite>();
        sprites.Add(foodSpriteArray[0]);
        for (int i = 1; i < (FOODPHASES + 1); i++)
        {
            sprites.Add(foodSpriteArray[i]);
        }
        food.spriteArray = sprites.ToArray();
        food.NextPhase();
    }

    public void CookSteak()
    {
        Food food = Instantiate(foodPrefab);
        food.foodType = Consumables.FoodType.Steak;
        food.transform.parent = transform;
        List<Sprite> sprites = new List<Sprite>();
        sprites.Add(foodSpriteArray[0]);
        for (int i = 1; i < (FOODPHASES + 1); i++)
        {
            sprites.Add(foodSpriteArray[i + 3]);
        }
        food.spriteArray = sprites.ToArray();
        food.NextPhase();
    }

    private void OnMouseDown()
    {
        player.GoInteract(this);
    }
}
