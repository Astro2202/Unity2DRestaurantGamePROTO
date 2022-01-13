using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientGroup : MonoBehaviour
{
    public Table assignedTable;
    internal bool seated = false;
    internal bool finished = false;
    internal System.Random random;
    public List<Client> clients;
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

        if (!seated)
        {
            int seatedClients = 0;

            foreach(Client c in clients)
            {
                if(c.assignedChair == null)
                {
                    AssignChairs();
                }
                if (c.seated)
                {
                    seatedClients++;
                }
            }

            if(seatedClients >= clients.Count)
            {
                seated = true;
                assignedTable.interactDuration = 60 * clients.Count;
                StartCoroutine(PatienceCalculation());
            }
            else
            {
                assignedTable.interactDuration = 10;
            }
        }
        else if(assignedTable.orders.Count < clients.Count)
        {
            foreach(Client c in clients)
            {
                assignedTable.orders.Add(c.GetOrder());
            }
        }

        if(patience <= 0 && !finished)
        {
            FinishVisit();
        }
    }

    private void AssignChairs()
    {
        bool availableChairs = false;

        foreach (Chair c in assignedTable.chairs)
        {
            if (c.available)
            {
                availableChairs = true;
            }
        }

        if (!availableChairs)
        {
            FinishVisit();
            return;
        }

        foreach (Client client in clients)
        {
            while (client.assignedChair == null)
            {
                Chair chair = assignedTable.chairs[random.Next(assignedTable.chairs.Count)];

                if (chair.available)
                {
                    client.assignedChair = chair;
                    chair.available = false;

                    if (chair.transform.position.x > assignedTable.transform.position.x)
                    {
                        client.flipX = true;
                    }
                }
            }
        }
    }

    private void FinishVisit()
    {
        StopCoroutine(PatienceCalculation());

        seated = false;
        finished = true;

        foreach (Chair c in assignedTable.chairs)
        {
            c.available = true;
        }

        assignedTable.ClearClientGroupInfo();

        this.transform.parent.GetComponent<Restaurant>().availableTables.Add(assignedTable);
    }

    IEnumerator PatienceCalculation()
    {
        while (seated)
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
