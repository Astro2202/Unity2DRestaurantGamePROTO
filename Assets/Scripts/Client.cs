using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Client : MonoBehaviour
{
    private bool seated = false;
    private bool wantsToOrder = false;
    private bool finished = false;
    private bool flipX = false;
    internal bool canLosePatience = true;
    private float speed = 3.0f;
    internal int patience = 10; // Default = 200(s)
    private const int ADDED_PATIENCE_AT_INTERACT = 10; // Default value to be chosen
    private const int X_POSITION_TO_DESTOY = -12;
    private const float Y_FLOOR_CHAIR_DIFFERENCE = 0.3f;
    private const float CHAIR_POSITION_ACCURACY = 0.01f;
    internal Chair assignedChair;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    internal System.Random random;
    private Order order;
    void Start()
    {

    }

    void Update()
    {
        if (assignedChair == null) return;

        if (finished)
        {
            Leave();
        }
        else if (!seated)
        {
            MoveToChair();
        }

        spriteRenderer.flipX = flipX;
        SetAnimatorParameters();
    }

    private void SetAnimatorParameters()
    {
        animator.SetBool("seated", seated);
        animator.SetInteger("patience", patience);
        animator.SetBool("order", wantsToOrder);
    }

    private void MoveToChair()
    {
        float step = Time.deltaTime * speed;
        float direction = Mathf.Sign(assignedChair.transform.position.x - transform.position.x);
        Vector2 movePos = new Vector2(transform.position.x + direction * step, transform.position.y);
        transform.position = movePos;

        if (Mathf.Abs(transform.position.x - assignedChair.transform.position.x) <= CHAIR_POSITION_ACCURACY)
        {
            seated = true;
            transform.position = new Vector2(assignedChair.transform.position.x, transform.position.y + Y_FLOOR_CHAIR_DIFFERENCE);
            if(assignedChair.transform.position.x > assignedChair.transform.parent.GetComponentInChildren<Table>().transform.position.x)
            {
                flipX = true;
            }
        }
    }

    public bool IsSeated()
    {
        return seated;
    }

    public Order SetOrder()
    {
        wantsToOrder = true;
        Array foodTypes = Enum.GetValues(typeof(Consumables.FoodType));
        Array drinkTypes = Enum.GetValues(typeof(Consumables.DrinkType));
        Consumables.FoodType chosenFood = (Consumables.FoodType)foodTypes.GetValue(random.Next(foodTypes.Length));
        Consumables.DrinkType chosenDrink = (Consumables.DrinkType)drinkTypes.GetValue(random.Next(drinkTypes.Length));
        order = new Order(chosenFood, chosenDrink);
        StartCoroutine(PatienceCalculation());
        return order;
    }

    public Order GetOrder()
    {
        if (order != null)
        {
            return order;
        }
        else
        {
            return SetOrder();
        }
    }

    public void ConfirmOrder()
    {
        wantsToOrder = false;
        patience += ADDED_PATIENCE_AT_INTERACT;
    }

    public void Leave()
    {
        float step = Time.deltaTime * speed;
        Vector2 movePos = new Vector2(transform.position.x - 1f * step, transform.position.y); //-1f indicating direction to the left
        transform.position = movePos;

        if (transform.position.x < X_POSITION_TO_DESTOY)
        {
            transform.parent.GetComponent<ClientGroup>().clients.Remove(this);
            Destroy(gameObject);
        }
    }
    public void FinishVisit()
    {
        wantsToOrder = false;
        seated = false;
        transform.position = new Vector2(transform.position.x, transform.position.y - Y_FLOOR_CHAIR_DIFFERENCE);
        flipX = true;
        finished = true;
        StopAllCoroutines();
    }

    IEnumerator PatienceCalculation()
    {
        while (seated)
        {
            if (canLosePatience)
            {
                if (patience > 0)
                {
                    patience--;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
}
