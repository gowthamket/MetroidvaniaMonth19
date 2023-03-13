using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void EnableInteraction();
    void DisableInteraction();
    Vector3 GetPosition();
    object GetInteractionContext();
    void SetContext(object obj);
}
