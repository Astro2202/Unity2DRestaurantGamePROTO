using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    internal float xTarget;
    private float speed = 5.0f;
    private bool interacting = false;
    private bool keepInteracting = false;
    private IInteractable interactable;
    private const float DEFAULT_INTERACT_TIME = 0.5f;
    internal float interactDuration = DEFAULT_INTERACT_TIME;
    public Animator animator;
    public FoodMenu foodMenu;
    public DrinkMenu drinkMenu;
    private List<Food> carriedFood;
    public Tray trayPrefab;
    private Tray tray;

    // Start is called before the first frame update
    void Start()
    {
        xTarget = transform.position.x;
        carriedFood = new List<Food>();
        tray = Instantiate(trayPrefab);
        tray.transform.parent = transform;
        tray.transform.position = transform.position;
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!AtTarget())
        {
            animator.SetBool("walking", true);
            FlipX();
            GoToTargetLocation();
        }  

        if (interacting)
        {
            tray.DisableFoodSprite();
        }
        else
        {
            tray.EnableFoodSprite();
        }
    }

    private bool FlipX()
    {
        if (transform.position.x > xTarget)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            tray.flipped = true;
            return true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            tray.flipped = false;
            return false;
        }
    }

    private void GoToTargetLocation()
    {
        float step = Time.deltaTime * speed;
        float direction = Mathf.Sign(xTarget - transform.position.x);
        Vector2 movePos = new Vector2(transform.position.x + direction * step, transform.position.y);
        transform.position = movePos;

        if (AtTarget())
        {
            Interact();
        }
    }

    internal void GoInteract(IInteractable interactable)
    {
        if (!interacting)
        {
            xTarget = interactable.GetXPosition();
            this.interactable = interactable;

            if (AtTarget())
            {
                Interact();
            }
        }
    }

    private void Interact()
    {
        interactDuration = interactable.GetInteractableDuration();
        StartCoroutine(InteractDurationLogic(interactable.GetInteractableDuration()));
        animator.SetBool("walking", false);
        interacting = true;

        switch (interactable.GetInteractable())
        {
            case InteractableEnum.Interactables.Kitchen:
                Kitchen kitchen = (Kitchen)interactable;
                if (kitchen.foodReady)
                {
                    Food food = kitchen.GetComponentInChildren<Food>();
                    if (!tray.PutFood(food))
                    {
                        Debug.Log("Tray full!");
                    }
                }
                else
                {
                    foodMenu.OpenFoodMenu();
                }
                break;
            case InteractableEnum.Interactables.Fridge:
                keepInteracting = true;
                Fridge fridge = (Fridge)interactable;
                if (!drinkMenu.player)
                {
                    drinkMenu.player = this;
                }
                drinkMenu.OpenDrinkMenu();
                break;
            case InteractableEnum.Interactables.Trashcan:
                Trashcan trashcan = (Trashcan)interactable;
                tray.RemoveContent();
                break;
            case InteractableEnum.Interactables.Table:
                Table table = (Table)interactable;
                if(!table.HasOrdersTaken() && table.HasOrders())
                {
                    table.GetOrders();
                }
                break;
        }
    }

    public void SetDrink(Drink drink)
    {
        if (!tray.PutDrink(drink))
        {
            Debug.Log("Tray full!");
        }
        Debug.Log("Drink set");
        keepInteracting = false;
    }

    private bool AtTarget()
    {
        return Mathf.Abs(transform.position.x - xTarget) <= 0.01f;
    }

    IEnumerator InteractDurationLogic(float duration)
    {
        animator.SetBool("walking", false);
        interacting = true;

        do
        {
            animator.SetBool("interacting", true);

            yield return new WaitForSeconds(duration);
        }
        while (keepInteracting);

        animator.SetBool("interacting", false);
        interacting = false;
    }
}
