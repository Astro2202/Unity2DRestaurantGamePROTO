using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientGroup : MonoBehaviour
{
    //public Table assignedTable;
    private bool allClientsSeated = false;
    private bool finished = false;
    internal System.Random random;
    internal List<Client> clients;
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
    }

    public bool HasPatience()
    {
        foreach(Client client in clients)
        {
            if(client.patience <= 0)
            {
                return false;
            }
        }
        return true;
    }

    public bool AllClientsSeated()
    {
        return allClientsSeated;
    }

    public List<Order> GetOrders()
    {
        List<Order> orders = new List<Order>();
        foreach (Client c in clients)
        {
            orders.Add(c.GetOrder());
        }
        return orders;
    }

    public void ConfirmOrder()
    {
        foreach(Client c in clients)
        {
            c.ConfirmOrder();
        }
    }

    public void ManageChairs(List<Chair> chairs)
    {
        int seatedClients = 0;

        foreach(Client client in clients)
        {
            if (!client.assignedChair)
            {
                AssignChair(client, chairs);
            }
            else if (client.IsSeated())
            {
                seatedClients++;
            }
        }

        if(seatedClients == clients.Count)
        {
            allClientsSeated = true;
        }
    }

    private bool AssignChair(Client client, List<Chair> chairs)
    {
        if(chairs.Count > 0)
        {
            Chair chair = chairs[random.Next(chairs.Count)];
            chairs.Remove(chair);
            client.assignedChair = chair;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FinishVisit()
    {
        foreach(Client client in clients)
        {
            client.FinishVisit();
        }
        allClientsSeated = false;
        finished = true;
    }

    public void PausePatience(float seconds)
    {
        StartCoroutine(PausePatienceCalculation(seconds));
    }

    IEnumerator PausePatienceCalculation(float seconds)
    {
        foreach(Client client in clients)
        {
            client.canLosePatience = false;
        }
        yield return new WaitForSeconds(seconds);
        foreach (Client client in clients)
        {
            client.canLosePatience = true;
        }
    }
}
