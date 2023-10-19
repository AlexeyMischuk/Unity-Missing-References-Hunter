using UnityEngine;

public class ChildObject
{
    public Object ObjectRef { get; }
    public string ComponentName { get; }
    public bool IsScriptMissing { get; }

    public ChildObject(Object obj, string componentName, bool isScriptMissing)
    {
        ObjectRef = obj;
        ComponentName = componentName;
        IsScriptMissing = isScriptMissing;
    }
}
