using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                var components = assetObject.GetComponentsInChildren<Component>(true);
                foreach (var comp in components)
                {
                    if (comp == null)
                    {
                        AssetsWithMissingRef.Add(assetObject);
                    }
                    else if (CheckProperties(comp))
                    {
                         AssetsWithMissingRef.Add(assetObject);
                    }
                }
            }
            else if (assetPath.Contains(".unity"))
            {
                var assetObject = AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath);
                var currentScenePath = SceneManager.GetActiveScene().path;
                var scene = EditorSceneManager.OpenScene(assetPath, OpenSceneMode.Single);
                var sceneObjects = scene.GetRootGameObjects();
                foreach (var obj in sceneObjects)
                {
                    var components = obj.GetComponentsInChildren<Component>();
                    foreach (var comp in components)
                    {
                        if (comp == null)
                        {
                            AssetsWithMissingRef.Add(assetObject);
                        }
                        else if (CheckProperties(comp))
                        {
                            AssetsWithMissingRef.Add(assetObject);
                        }
                    }
                }
                EditorSceneManager.OpenScene(currentScenePath);
            }
            else
            {
                var subAssets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
                foreach (var asset in subAssets)
                {
                    if (CheckProperties(asset)) AssetsWithMissingRef.Add(asset);
                }
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
