using System.Collections.Generic;
using UnityEngine;

public class ChildObject
{
    public Object ObjectRef { get; }
    public List<string> ComponentName { get; }
    public bool IsScriptMissing { get; }

    public ChildObject(Object obj, string componentName, bool isScriptMissing)
    {
        ObjectRef = obj;
        ComponentName = new List<string> {componentName};
        IsScriptMissing = isScriptMissing;
    }
}
