using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    internal Sprite[] spriteArray;
    internal Consumables.DrinkType drinkType;
    private int phase = 0;
    private int sipsRemaining = 10;
    private const int LAST_PHASE = 1;
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

    public bool TakeSip()
    {
        if (sipsRemaining > 0)
        {
            sipsRemaining--;
            if (sipsRemaining <= 0)
            {
                NextPhase();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetRemainingSips()
    {
        return sipsRemaining;
    }
}
