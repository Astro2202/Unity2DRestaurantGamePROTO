using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    internal Sprite[] spriteArray;
    internal Consumables.FoodType foodType;
    private int phase = 0;
    private const int LAST_PHASE = 3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
    }

    public void NextPhase()
    {
        if (!(phase == LAST_PHASE))
        {
            phase++;
            spriteRenderer.sprite = spriteArray[phase];
        }
    }

    public bool Empty()
    {
        return phase >= LAST_PHASE;
    }
}
