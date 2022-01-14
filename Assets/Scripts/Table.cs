using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour, IInteractable
{
    public List<Chair> availableChairs;
    public TableUI ui;
    internal Player player;
    internal int interactDuration;
    internal List<Order> orders;
    private bool hasOrders = false;
    private bool hasOrdersTaken = false;

    // Start is called before the first frame update
    void Start()
    {
        orders = new List<Order>();
        availableChairs = new List<Chair>();
        foreach(Chair chair in GetComponentsInChildren<Chair>())
        {
            availableChairs.Add(chair);
        }
        ui.xPositionTable = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetOrder(List<Order> orders)
    {
        this.orders = orders;
        interactDuration = 2 * orders.Count;
        hasOrders = true;
        ui.RequestOrder();
    }

    public void Reset()
    {
        Debug.Log("Reset table");
        orders.Clear();
        transform.parent.GetComponent<Restaurant>().availableTables.Add(this);
        hasOrders = false;
        hasOrdersTaken = false;
        ui.Reset();
    }

    public bool HasOrders()
    {
        return hasOrders;
    }

    public bool HasOrdersTaken()
    {
        return hasOrdersTaken;
    }

    public List<Order> TakeOrder()
    {
        hasOrdersTaken = true;
        ui.NoteOrders(orders);
        return orders;
    }

    public InteractableEnum.Interactables GetInteractable()
    {
        return InteractableEnum.Interactables.Table;
    }

    public int GetInteractableDuration()
    {
        if (hasOrders && !hasOrdersTaken)
        {
            interactDuration = orders.Count * 2;
        }
        else
        {
            interactDuration = 1;
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
