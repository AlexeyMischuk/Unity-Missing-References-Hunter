using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class MissingReferencesFinder
{
    public static List<Object> AssetsWithMissingRef { get; } = new();

    public static void FindAssets(List<string> assetsList)
    {
        AssetsWithMissingRef.Clear();

        foreach (var assetPath in assetsList)
        {
            if (assetPath.Contains(".prefab"))
            {
                var assetObject = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                var components = assetObject.GetComponents<Component>();
                foreach (var comp in components)
                {
                    if (CheckProperties(comp)) AssetsWithMissingRef.Add(assetObject);
                }
            }
            else
            {
                var assetObject = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                if (CheckProperties(assetObject)) AssetsWithMissingRef.Add(assetObject);
            }
        }
    }

    private static bool CheckProperties(Object obj)
    {
        var serializedObject = new SerializedObject(obj);
        var property = serializedObject.GetIterator();

        while (property.NextVisible(true))
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                if (property.objectReferenceInstanceIDValue != 0 && property.objectReferenceValue == null)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
