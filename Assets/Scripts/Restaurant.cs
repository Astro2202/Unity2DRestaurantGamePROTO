using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restaurant : MonoBehaviour
{
    public Client clientPrefab;
    public ClientGroup clientGroupPrefab;

    internal List<Table> availableTables;
    private List<ClientGroup> clientGroups;
    private bool open = true;
    private System.Random random;
    // Start is called before the first frame update
    void Start()
    {
        availableTables = new List<Table>();
        clientGroups = new List<ClientGroup>();
        random = new System.Random();
        foreach (Table t in GetComponentsInChildren<Table>())
        {
            t.player = GetComponentInChildren<Player>();
            availableTables.Add(t);
        }
        GetComponentInChildren<Kitchen>().player = GetComponentInChildren<Player>();
        GetComponentInChildren<Trashcan>().player = GetComponentInChildren<Player>();
        GetComponentInChildren<Fridge>().player = GetComponentInChildren<Player>();
        StartCoroutine(ClientSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ClientSpawner()
    {
        while (open)
        {
            if (availableTables.Count > 0)
            {
                ClientGroup clientGroup = Instantiate(clientGroupPrefab);
                clientGroup.transform.parent = transform;

                Table table = availableTables[random.Next(availableTables.Count)];
                availableTables.Remove(table);

                List<Client> newClients = new List<Client>();

                for (int i = 0; i < random.Next(1, 3); i++)
                {
                    
                    //Debug.Log("Creating client " + i + " for clientgroup");
                    Client client = Instantiate(clientPrefab);
                    client.transform.parent = clientGroup.transform;
                    client.transform.position = new Vector3(-10f - (i * 2), 1.0158f, 0f);
                    client.random = random;
                    newClients.Add(client);
                }
                
                clientGroup.clients = newClients;
                clientGroup.random = random;
                clientGroup.transform.parent = table.transform;
                table.AssignClientGroup(clientGroup);
                clientGroups.Add(clientGroup);
            }
            yield return new WaitForSeconds(10);
        }
    }
}
