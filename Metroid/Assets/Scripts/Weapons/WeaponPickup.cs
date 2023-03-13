using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private SO_WeaponData data;

    private SpriteRenderer graphics;

    private void Awake()
    {
        graphics = GetComponentInChildren<SpriteRenderer>();

        Init();

        
    }

    private void Init()
    {
       // graphics.sprite = data.PickupSprite;
    }

    public object GetInteractionContext()
    {
        return data;
    }

    public void SetContext(object obj)
    {
        switch (obj)
        {
            case null:
                gameObject.SetActive(false);
                break;
            case SO_WeaponData so:
                data = so;
                Init();
                break;
        }
    }

    public void EnableInteraction()
    {
        
    }

    public void DisableInteraction()
    {
        
    }

    public Vector3 GetPosition() => transform.position;
}

