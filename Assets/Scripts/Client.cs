using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Client : MonoBehaviour
{
    public Chair assignedChair;
    public bool seated = false;
    internal bool flipX = false;
    private float speed = 3.0f;
    internal int patience;
    public Animator animator;
    internal System.Random random;
    private Order order;
    //TODO: Order animatie speelt nog voor iedereen zit omdat de Hasordered altijd true returned. Dit is omdat een enum geen null waarde kan hebben.
    // Dit moet dus vervangen worden met een bool of iets anders
    void Start()
    {
        patience = 200;
    }

    void Update()
    {
        //Debug.Log(order.ToString());
        if (assignedChair == null) return;
        
        if (!seated && !this.transform.parent.GetComponent<ClientGroup>().finished)
        {
            MoveToChair();
        }
        else
        {
            if (flipX)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            if (order != null)
            {
                animator.SetBool("order", true);
            }
            if (this.transform.parent.GetComponent<ClientGroup>().seated)
            {
                //Order();
            }
        }

        if (this.transform.parent.GetComponent<ClientGroup>().finished)
        {
            Leave();
        }

        animator.SetInteger("patience", patience);
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
            animator.SetBool("seated", true);
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.3f);
        }
    }

    public Order CreateOrder()
    {
        animator.SetBool("order", true);
        Array foodTypes = Enum.GetValues(typeof(Consumables.FoodType));
        Consumables.FoodType chosenFood = (Consumables.FoodType)foodTypes.GetValue(random.Next(foodTypes.Length));
        order = new Order(chosenFood);
        Debug.Log("Creating order: " + order.ToString());
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

    private void Leave()
    {
        if (seated)
        {
            seated = false;
            animator.SetBool("seated", false);
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
        }

        flipX = true;

        float step = Time.deltaTime * speed;
        Vector2 movePos = new Vector2(transform.position.x - 1f * step, transform.position.y);
        transform.position = movePos;

        if(transform.position.x < -12f)
        {
            this.transform.parent.GetComponent<ClientGroup>().clients.Remove(this);
            Destroy(gameObject);
        }
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
