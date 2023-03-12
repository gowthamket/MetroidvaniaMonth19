using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericNotImplementedError<T>
{
    public T TryGet(T value, string name)
    {
        if (value != null)
        {
            return value;
        }

        Debug.LogError(typeof(T) + " not implemented on " + name);
        return default;
    }
}
