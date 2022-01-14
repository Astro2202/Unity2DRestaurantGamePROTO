using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour, IInteractable
{
    public List<Chair> chairs;
    public TableUI ui;
    internal Player player;
    internal bool available = true;
    internal int interactDuration;
    internal List<Order> orders;

    // Start is called before the first frame update
    void Start()
    {
        orders = new List<Order>();
        chairs = new List<Chair>();
        foreach(Chair chair in GetComponentsInChildren<Chair>())
        {
            chairs.Add(chair);
        }
        ui.xPositionTable = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (orders.Count <= 0)
        {
            interactDuration = 1;
        }
    }

    public void SetOrder(List<Order> orders)
    {
        this.orders = orders;
        ui.RequestOrder();
        interactDuration = 2 * orders.Count;
    }

    public void ClearClientGroupInfo()
    {
        available = true;
        orders.Clear();
        Debug.Log(orders.Count);
        ui.Reset();
    }

    public List<Order> TakeOrder()
    {
        if(orders.Count > 0)
        {
            ui.NoteOrders(orders);
            return orders;
        }
        else
        {
            return null;
        }
    }

    public InteractableEnum.Interactables GetInteractable()
    {
        return InteractableEnum.Interactables.Table;
    }

    public int GetInteractableDuration()
    {
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
