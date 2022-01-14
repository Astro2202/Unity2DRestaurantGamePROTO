using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientGroup : MonoBehaviour
{
    public Table assignedTable;
    private bool allClientsSeated = false;
    private bool finished = false;
    internal System.Random random;
    internal List<Client> clients;
    private int patience = 15; //default = 200
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (finished)
        {
            if (clients.Count <= 0)
            {
                Destroy(gameObject);
            }
            return;

        }

        if (!allClientsSeated)
        {
            ManageChairs();
        }
        else if(!assignedTable.HasOrders())
        {
            ManageOrders();
        }

        if (assignedTable.HasOrdersTaken())
        {
            foreach(Client client in clients)
            {
                client.OrderIsTaken();
            }
        }

        if(patience <= 0)
        {
            FinishVisit();
        }
    }

    private void ManageOrders()
    {
        List<Order> orders = new List<Order>();
        foreach (Client c in clients)
        {
            orders.Add(c.TakeOrder());
        }
        assignedTable.SetOrder(orders);
    }

    private void ManageChairs()
    {
        int seatedClients = 0;

        foreach(Client client in clients)
        {
            if (!client.assignedChair)
            {
                AssignChair(client);
            }
            else if (client.IsSeated())
            {
                seatedClients++;
            }
        }

        if(seatedClients == clients.Count)
        {
            allClientsSeated = true;
            StartCoroutine(PatienceCalculation());
        }
    }

    private bool AssignChair(Client client)
    {
        if(assignedTable.availableChairs.Count > 0)
        {
            Chair chair = assignedTable.availableChairs[random.Next(assignedTable.availableChairs.Count)];
            assignedTable.availableChairs.Remove(chair);
            client.assignedChair = chair;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FinishVisit()
    {
        StopCoroutine(PatienceCalculation());

        foreach(Client client in clients)
        {
            client.FinishVisit();
        }

        allClientsSeated = false;
        finished = true;

        assignedTable.Reset();
    }

    IEnumerator PatienceCalculation()
    {
        while (allClientsSeated)
        {
            if(patience > 0)
            {
                patience--;
            }
            //Debug.Log(patience);
            foreach(Client c in clients)
            {
                c.patience = patience;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
