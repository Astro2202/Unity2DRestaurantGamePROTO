using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
    public List<Chair> chairs;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (available)
        {
            interactDuration = 10;
        }
    }

    public void ClearClientGroupInfo()
    {
        available = true;
        orders.Clear();
    }

    public List<Order> TakeOrder()
    {
        if(orders != null)
        {
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
