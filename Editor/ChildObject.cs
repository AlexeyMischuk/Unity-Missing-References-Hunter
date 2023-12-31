using System.Collections.Generic;
using UnityEngine;

public class ChildObject
{
    public Object ObjectRef { get; }
    public List<string> ComponentName { get; }

    public ChildObject(Object obj, string componentName)
    {
        ObjectRef = obj;
        ComponentName = new List<string> {componentName};
    }
}
