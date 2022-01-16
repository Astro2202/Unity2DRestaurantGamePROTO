using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMenu : MonoBehaviour
{
    //public GameObject foodMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void OpenFoodMenu()
    {
        gameObject.SetActive(true);
    }

    internal void CloseFoodMenu()
    {
        gameObject.SetActive(false);
    }
}
