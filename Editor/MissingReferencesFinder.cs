using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

public static class MissingReferencesFinder
{
    private static Dictionary<Object, List<ChildObject>> _objectInformation = new();
   
    public static List<Object> ObjectIndex { get; } = new();

    public static void FindAssets(List<string> assetsList)
    {
        _objectInformation.Clear();
        ObjectIndex.Clear();
        
        foreach (var assetPath in assetsList)
        {
            if (assetPath.Contains(".prefab"))
            {
                var assetObject = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                var components = assetObject.GetComponents<Component>();
                CheckComponents(components, assetObject, assetObject);
                SearchInChildren(assetObject, assetObject);
            }
            else if (assetPath.Contains(".unity"))
            {
                var assetObject = AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath);
                var scene = EditorSceneManager.OpenScene(assetPath, OpenSceneMode.Additive);
                var sceneObjects = scene.GetRootGameObjects();
                foreach (var obj in sceneObjects)
                {
                    var components = obj.GetComponents<Component>();
                    CheckComponents(components, obj, assetObject);
                    SearchInChildren(obj, assetObject);
                }
            }
            else
            {
                var mainAsset = AssetDatabase.LoadMainAssetAtPath(assetPath);
                var subAssets = AssetDatabase.LoadAllAssetsAtPath(assetPath);
                foreach (var asset in subAssets)
                {
                    if (PropertiesHasMissing(asset))
                    {
                        var childObject = new ChildObject(asset, null, false);
                        _objectInformation.Add(mainAsset, new List<ChildObject> {childObject});
                        ObjectIndex.Add(mainAsset);
                    }
                }
            }
        }
    }

    private static bool PropertiesHasMissing(Object obj)
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

    public static List<ChildObject> GetChildrenInformation(Object objectRef)
    {
        if (_objectInformation.ContainsKey(objectRef))
        {
            return _objectInformation[objectRef];
        }
        return null;
    }

    private static void CheckComponents(Component[] components, Object currentObject, Object rootObject)
    {
        foreach (var comp in components)
        {
            if (comp == null)
            {
                var missingScriptComp = new ChildObject(null, null, true);
                if (_objectInformation.ContainsKey(rootObject))
                {
                    _objectInformation[rootObject].Add(missingScriptComp);
                }
                else
                {
                    _objectInformation.Add(rootObject, new List<ChildObject> { missingScriptComp });
                    ObjectIndex.Add(rootObject);
                }
                
            }
            else if (PropertiesHasMissing(comp))
            {
                var componentName = comp.GetType().Name;
                var componentWithMissing = new ChildObject(currentObject, componentName, false);
                if (_objectInformation.ContainsKey(rootObject))
                {
                    _objectInformation[rootObject].Add(componentWithMissing);
                }
                else
                {
                    _objectInformation.Add(rootObject,new List<ChildObject> {componentWithMissing});
                    ObjectIndex.Add(rootObject);
                }
            }
        } 
    }

    private static void SearchInChildren(GameObject currentObject, Object rootObject)
    {
        var childCount = currentObject.transform.childCount;
        for (var i = 0; i < childCount; i++)
        {
            var childObject = currentObject.transform.GetChild(i).gameObject;
            var childComponents = childObject.GetComponents<Component>();
            CheckComponents(childComponents, childObject, rootObject);
            SearchInChildren(childObject.gameObject, rootObject);
        } 
    }
}
