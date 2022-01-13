using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour, IInteractable
{
    internal Player player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public InteractableEnum.Interactables GetInteractable()
    {
        return InteractableEnum.Interactables.Trashcan;
    }

    public int GetInteractableDuration()
    {
        return 30;
    }

    public float GetXPosition()
    {
        return transform.position.x;
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on trashcan");
        player.GoInteract(this);
    }
}