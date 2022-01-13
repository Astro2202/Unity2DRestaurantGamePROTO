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
    internal Food middleFrontSlot;
    internal Food middleBackSlot;
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
            }
            else
            {
                rightSlot.transform.position = rightPosition;
            }
        }
        if (leftSlot)
        {
            if (flipped)
            {
                leftSlot.transform.position = leftPositionFlipped;
            }
            else
            {
                leftSlot.transform.position = leftPosition;
            }
        }
    }

    public void UpdatePositions()
    {
        rightPosition = new Vector2(transform.position.x + 0.35f, transform.position.y + 0.2f);
        rightPositionFlipped = new Vector2(transform.position.x - 0.35f, transform.position.y - 0.1f);
        leftPosition = new Vector2(transform.position.x + 0.35f, transform.position.y - 0.1f);
        leftPositionFlipped = new Vector2(transform.position.x - 0.35f, transform.position.y + 0.2f);
    }

    public bool PutFood(Food food)
    {
        if (!rightSlot)
        {
            rightSlot = food;
            food.transform.parent = transform;
            food.spriteRenderer.sortingOrder = 30;
            return true;
        }
        else if (!leftSlot)
        {
            leftSlot = food;
            food.transform.parent = transform;
            food.spriteRenderer.sortingOrder = 30;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PutDrink(Food drink)
    {
        if (!middleBackSlot)
        {
            middleBackSlot = drink;
            drink.transform.parent = transform;
            drink.spriteRenderer.sortingOrder=30;
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
