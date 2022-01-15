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

    private void ManageClientGroup()
    {
        if (!clientGroup.AllClientsSeated())
        {
            clientGroup.ManageChairs(availableChairs);
        }
        else if(!hasOrders)
        {
            orders = clientGroup.GetOrders();
            hasOrders = true;
            ui.RequestOrder();
        }
        if (!clientGroup.HasPatience())
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
        Debug.Log("Reset table");
        orders.Clear();
        ResetAvailableChairs();

        if (clientGroup)
        {
            clientGroup.FinishVisit();
            clientGroup = null;
        }
        
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
