using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    private Vector2 rightPosition;
    private Vector2 leftPosition;
    private Vector2 middleFrontPosition;
    private Vector2 middleBackPosition;
    private Vector2 rightPositionFlipped;
    private Vector2 leftPositionFlipped;
    private Vector2 middleFrontPositionFlipped;
    private Vector2 middleBackPositionFlipped;
    internal Food rightSlot;
    internal Food leftSlot;
    internal Drink middleFrontSlot;
    internal Drink middleBackSlot;
    internal bool flipped = false;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePositions();
        if (rightSlot)
        {
            if (flipped)
            {
                rightSlot.transform.position = rightPositionFlipped;
                rightSlot.spriteRenderer.sortingOrder = 25;
            }
            else
            {
                rightSlot.transform.position = rightPosition;
                rightSlot.spriteRenderer.sortingOrder = 30;
            }
        }
        if (leftSlot)
        {
            if (flipped)
            {
                leftSlot.transform.position = leftPositionFlipped;
                leftSlot.spriteRenderer.sortingOrder = 30;
            }
            else
            {
                leftSlot.transform.position = leftPosition;
                leftSlot.spriteRenderer.sortingOrder = 25;
            }
        }
        if (middleBackSlot)
        {
            if (flipped)
            {
                middleBackSlot.transform.position = middleBackPositionFlipped;
            }
            else
            {
                middleBackSlot.transform.position= middleBackPosition;
            }
        }
        if (middleFrontSlot)
        {
            if (flipped)
            {
                middleFrontSlot.transform.position = middleFrontPositionFlipped;
            }
            else
            {
                middleFrontSlot.transform.position = middleFrontPosition;
            }
        }
    }

    public void UpdatePositions()
    {
        rightPosition = new Vector2(transform.position.x + 0.4f, transform.position.y - 0.1f);
        rightPositionFlipped = new Vector2(transform.position.x - 0.4f, transform.position.y + 0.2f);
        leftPosition = new Vector2(transform.position.x + 0.4f, transform.position.y + 0.2f);
        leftPositionFlipped = new Vector2(transform.position.x - 0.4f, transform.position.y - 0.1f);
        middleBackPosition = new Vector2(transform.position.x + 0.25f, transform.position.y);
        middleBackPositionFlipped = new Vector2(transform.position.x - 0.25f, transform.position.y);
        middleFrontPosition = new Vector2(transform.position.x + 0.5f, transform.position.y);
        middleFrontPositionFlipped = new Vector2(transform.position.x - 0.5f, transform.position.y);
    }

    public bool PutFood(Food food)
    {
        if (!rightSlot)
        {
            rightSlot = food;
            food.transform.parent = transform;
            //food.spriteRenderer.sortingOrder = 30;
            return true;
        }
        else if (!leftSlot)
        {
            leftSlot = food;
            food.transform.parent = transform;
            //food.spriteRenderer.sortingOrder = 30;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PutDrink(Drink drink)
    {
        if (!middleBackSlot)
        {
            middleBackSlot = drink;
            drink.transform.parent = transform;
            drink.spriteRenderer.sortingOrder = 27;
            return true;
        }
        else if (!middleFrontSlot)
        {
            middleFrontSlot = drink;
            drink.transform.parent = transform;
            drink.spriteRenderer.sortingOrder = 27;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveContent()
    {
        if (rightSlot)
        {
            Destroy(rightSlot.gameObject);
        }
        if (leftSlot)
        {
            Destroy(leftSlot.gameObject);
        }
        if (middleFrontSlot)
        {
            Destroy(middleFrontSlot.gameObject);
        }
        if (middleBackSlot)
        {
            Destroy(middleBackSlot.gameObject);
        }
    }

    public void EnableFoodSprite()
    {
        if (rightSlot)
        {
            rightSlot.spriteRenderer.enabled = true;
        }
        if (leftSlot)
        {
            leftSlot.spriteRenderer.enabled = true;
        }
        if (middleFrontSlot)
        {
            middleFrontSlot.spriteRenderer.enabled = true;
        }
        if (middleBackSlot)
        {
            middleBackSlot.spriteRenderer.enabled = true;
        }
    }

    public void DisableFoodSprite()
    {
        if (rightSlot)
        {
            rightSlot.spriteRenderer.enabled = false;
        }
        if (leftSlot)
        {
            leftSlot.spriteRenderer.enabled = false;
        }
        if (middleFrontSlot)
        {
            middleFrontSlot.spriteRenderer.enabled = false;
        }
        if (middleBackSlot)
        {
            middleBackSlot.spriteRenderer.enabled = false;
        }
    }
}
