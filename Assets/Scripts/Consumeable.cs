using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumeable : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    internal Sprite[] spriteArray;
    internal Consumables.FoodType foodType;
    private int phase = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextPhase()
    {
        phase++;
        spriteRenderer.sprite = spriteArray[phase];
    }
}
