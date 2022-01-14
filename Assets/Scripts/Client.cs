using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Client : MonoBehaviour
{
    public Chair assignedChair;
    public bool seated = false;
    private bool wantsToOrder = false;
    private bool finished = false;
    
    internal bool flipX = false;
    private float speed = 3.0f;
    internal int patience;
    public Animator animator;
    internal System.Random random;
    private Order order;
    void Start()
    {
        patience = 200;
    }

    void Update()
    {
        if (assignedChair == null) return;
        
        if (!seated && !this.transform.parent.GetComponent<ClientGroup>().finished)
        {
            MoveToChair();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = flipX;
        }

        if (finished)
        {
            Leave();
        }

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

        if (Mathf.Abs(transform.position.x - assignedChair.transform.position.x) <= 0.001f)
        {
            seated = true;
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.3f);
        }
    }

    public Order CreateOrder()
    {
        wantsToOrder = true;
        Array foodTypes = Enum.GetValues(typeof(Consumables.FoodType));
        Array drinkTypes = Enum.GetValues(typeof(Consumables.DrinkType));
        Consumables.FoodType chosenFood = (Consumables.FoodType)foodTypes.GetValue(random.Next(foodTypes.Length));
        Consumables.DrinkType chosenDrink = (Consumables.DrinkType)drinkTypes.GetValue(random.Next(drinkTypes.Length));
        order = new Order(chosenFood, chosenDrink);
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
            return CreateOrder();
        }
    }

    public void Leave()
    {
        float step = Time.deltaTime * speed;
        Vector2 movePos = new Vector2(transform.position.x - 1f * step, transform.position.y);
        transform.position = movePos;

        if (transform.position.x < -12f)
        {
            this.transform.parent.GetComponent<ClientGroup>().clients.Remove(this);
            Destroy(gameObject);
        }
    }
    public void FinishVisit()
    {
        wantsToOrder = false;
        seated = false;
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
        flipX = true;
        finished = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "Client")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
