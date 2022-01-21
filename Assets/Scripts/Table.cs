using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour, IInteractable
{
    public List<Chair> availableChairs;
    public TableUI ui;
    internal Player player;
    internal float interactDuration;
    internal List<Order> orders;
    private bool hasOrders = false;
    private bool hasOrdersTaken = false;
    private ClientGroup clientGroup;

    // Start is called before the first frame update
    void Start()
    {
        orders = new List<Order>();
        availableChairs = new List<Chair>();
        ResetAvailableChairs();
        ui.xPositionTable = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (clientGroup)
        {
            ManageClientGroup();
        }
    }

    public Order GetOrderWithFood(Food food)
    {
        foreach (Order order in orders)
        {
            if (order.FoodType == food.foodType)
            {
                if (!order.FoodOrderCompleted)
                {
                    return order;
                }
            }
        }
        return null;
    }

    public Order GetOrderWithDrink(Drink drink)
    {
        foreach (Order order in orders)
        {
            if (order.DrinkType == drink.drinkType)
            {
                if (!order.DrinkOrderCompleted)
                {
                    return order;
                }
            }
        }
        return null;
    }

    public bool SetFood(Order order, Food food)
    {
        if (order.FoodType == food.foodType)
        {
            Client client = clientGroup.SetFood(food);
            if (client != null)
            {
                order.FoodOrderCompleted = true;
                food.transform.parent = transform;
                food.spriteRenderer.sortingOrder = 2;
                if (client.IsFlipped())
                {
                    food.transform.position = new Vector2(transform.position.x + 0.75f, transform.position.y + 0.5f);
                }
                else
                {
                    food.transform.position = new Vector2(transform.position.x - 0.75f, transform.position.y + 0.5f);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public bool SetDrink(Order order, Drink drink)
    {
        if (order.DrinkType == drink.drinkType)
        {
            Client client = clientGroup.SetDrink(drink);
            if (client != null)
            {
                order.DrinkOrderCompleted = true;
                drink.transform.parent = transform;
                drink.spriteRenderer.sortingOrder = 2;
                if (client.IsFlipped())
                {
                    drink.transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y + 0.6f);
                }
                else
                {
                    drink.transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.4f);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void ManageClientGroup()
    {
        if (!clientGroup.AllClientsSeated())
        {
            clientGroup.ManageChairs(availableChairs);
        }
        else if (!hasOrders)
        {
            orders = clientGroup.GetOrders();
            hasOrders = true;
            ui.RequestOrder();
        }
        if (!clientGroup.HasPatience())
        {
            Reset();
        }
        else if (clientGroup.VisitCompleted())
        {
            Reset();
        }
    }

    public void AssignClientGroup(ClientGroup clientGroup)
    {
        this.clientGroup = clientGroup;
    }

    public void Reset()
    {
        orders.Clear();
        ResetAvailableChairs();
        clientGroup.FinishVisit();
        clientGroup = null;
        transform.parent.GetComponent<Restaurant>().availableTables.Add(this);
        hasOrders = false;
        hasOrdersTaken = false;
        ui.Reset();
    }

    private void ResetAvailableChairs()
    {
        availableChairs.Clear();
        foreach (Chair chair in GetComponentsInChildren<Chair>())
        {
            availableChairs.Add(chair);
        }
    }

    public bool HasOrders()
    {
        return hasOrders;
    }

    public bool HasOrdersTaken()
    {
        return hasOrdersTaken;
    }

    public List<Order> GetOrders()
    {
        return orders;
    }

    public List<Order> TakeOrders()
    {
        clientGroup.PausePatience(GetInteractableDuration());
        hasOrdersTaken = true;
        clientGroup.ConfirmOrder();
        ui.NoteOrders(orders);
        return orders;
    }

    public InteractableEnum.Interactables GetInteractable()
    {
        return InteractableEnum.Interactables.Table;
    }

    public float GetInteractableDuration()
    {
        if (hasOrders && !hasOrdersTaken)
        {
            interactDuration = orders.Count * 2;
        }
        else
        {
            interactDuration = 0.5f;
        }
        return interactDuration;
    }

    public float GetXPosition()
    {
        return transform.position.x;
    }

    private void OnMouseDown()
    {
        player.GoInteract(this);
    }
}
