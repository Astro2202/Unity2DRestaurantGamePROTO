using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableUI : MonoBehaviour
{
    public Text orderFoodText1;
    public Text orderDrinkText1;
    public Text orderFoodText2;
    public Text orderDrinkText2;
    public List<Text> ordertexts;
    internal float xPositionTable;
    public CameraControls cameraControls;
    public Image RequestPlayerImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoteOrders(List<Order> orders)
    {
        StopRequestOrder();
        StartCoroutine(RevealNotedOrders(orders));
    }

    public void RequestOrder()
    {
        RequestPlayerImage.enabled = true;
    }

    public void StopRequestOrder()
    {
        RequestPlayerImage.enabled = false;
    }

    public void Reset()
    {
        StopRequestOrder();
        foreach(Text orderText in ordertexts)
        {
            orderText.text = "";
        }
    }

    public void OnClick()
    {
        cameraControls.SnapToLocation(xPositionTable);
        Debug.Log("Table button pressed");
    }

    IEnumerator RevealNotedOrders(List<Order> orders)
    {
        for (int i = 0; i < orders.Count; i++) {
            ordertexts[i * 2].text = orders[i].FoodType.ToString();
            yield return new WaitForSeconds(1);
            ordertexts[(i * 2) + 1].text = orders[i].DrinkType.ToString();
            yield return new WaitForSeconds(1);
        }
    }
}
