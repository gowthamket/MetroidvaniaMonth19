using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Core : MonoBehaviour
{
    
    private List<CoreComponent> components = new List<CoreComponent>();

    private void Awake()
    {
        
    }

    public void LogicUpdate()
    {
        foreach  (CoreComponent component in components)
        {
            component.LogicUpdate();
        }
    }

    public void AddComponent(CoreComponent component)
    {
        if (!components.Contains(component))
        {
            components.Add(component);
        }
    }

    public T GetCoreComponent<T>() where T:CoreComponent
    {
        var comp = components.OfType<T>().FirstOrDefault();

        if (comp)
        {
            return comp;
        }

        comp = GetComponentInChildren<T>();

        if (comp)
        {
            return comp;

            
        }
        Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
        return comp;
    }

    public T GetCoreComponent<T>(ref T value) where T:CoreComponent
    {
        value = GetCoreComponent<T>();
        return value;
    }
}
