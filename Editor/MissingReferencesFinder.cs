using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public static class MissingReferencesFinder
{
    private static Dictionary<Object, Dictionary<string, int>> _objectInformation = new();
    public static List<Object> ObjectIndex = new();

    private const string MissingScript = "Missing script";

    public static void FindAssets(List<string> assetsList)
    {
        _objectInformation.Clear();
        
        foreach (var assetPath in assetsList)
        {
            if (assetPath.Contains(".prefab"))
            {
                var assetObject = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                var components = assetObject.GetComponents<Component>();
                CheckComponents(components, assetObject);
                SearchInChildren(assetObject);
                
                
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
                            // ObjectInformation.Add(assetObject);
                        }
                        else if (CheckProperties(comp))
                        {
                            // ObjectInformation.Add(assetObject);
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
                    // if (CheckProperties(asset)) ObjectInformation.Add(asset);
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

    public static Dictionary<string,int> GetComponentName(Object objectRef)
    {
        if (_objectInformation.ContainsKey(objectRef))
        {
            return _objectInformation[objectRef];
        }
        return null;
    }

    private static void CheckComponents(Component[] components, Object assetObject)
    {
        foreach (var comp in components)
        {
            if (comp == null)
            {
                if (_objectInformation.ContainsKey(assetObject))
                {
                    if (!_objectInformation[assetObject].TryAdd(MissingScript, 1))
                    {
                        _objectInformation[assetObject][MissingScript]++;
                    }
                }
                else
                {
                    _objectInformation.Add(assetObject, new Dictionary<string, int>
                    {
                        {MissingScript, 1}
                    });
                    ObjectIndex.Add(assetObject);
                }
            }
            else if (CheckProperties(comp))
            {
                var componentName = comp.gameObject.name;
                if (_objectInformation.ContainsKey(assetObject))
                {
                    if (!_objectInformation[assetObject].TryAdd(componentName, 1))
                    {
                        _objectInformation[assetObject][componentName]++;
                    }
                }
                else
                {
                    _objectInformation.Add(assetObject, new Dictionary<string, int>
                    {
                        {componentName, 1}
                    });
                    ObjectIndex.Add(assetObject);
                }
            }
        } 
    }

    private static void SearchInChildren(GameObject gObject)
    {
        var childCount = gObject.transform.childCount;
        for (var i = 0; i < childCount; i++)
        {
            var childObject = gObject.transform.GetChild(childCount);
            var childComponents = childObject.GetComponents<Component>();
            CheckComponents(childComponents, childObject);
            SearchInChildren(childObject.gameObject);
        } 
    }
}
