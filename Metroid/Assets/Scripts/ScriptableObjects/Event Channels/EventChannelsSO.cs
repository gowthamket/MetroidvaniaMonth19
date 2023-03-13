using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventChannelsSO<T> : ScriptableObject
{
    public event EventHandler<T> OnEvent;

    public void RaiseEvent(object sender, T context)
    {
        OnEvent?.Invoke(sender, context);
    }
}
