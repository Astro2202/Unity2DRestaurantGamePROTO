using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    float GetXPosition();
    InteractableEnum.Interactables GetInteractable();
    int GetInteractableDuration();
}
