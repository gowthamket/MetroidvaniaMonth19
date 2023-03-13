using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeselectUIEventSystem : MonoBehaviour
{
    private EventSystem eventSystem;
    [SerializeField] private TriggerEventChannel triggerDisableSelectedChannel;

    private void Awake()
    {
        eventSystem = GetComponent<EventSystem>();

        triggerDisableSelectedChannel.OnEvent += DisableSelectedUI;
    }

    private void DisableSelectedUI(object sender, EventArgs arg)
    {
        eventSystem.SetSelectedGameObject(null);
    }


}
