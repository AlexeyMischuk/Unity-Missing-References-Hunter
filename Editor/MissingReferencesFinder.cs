using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class MissingReferencesFinder
{
    public static List<AssetMissingRef> AssetsWithMissingRef { get; } = new();

    public static void FindAssets(string[] assetsArray)
    {
        foreach (var assetGuid in assetsArray)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            var componentsDictionary = AnalyzeAsset(assetPath);
            if (componentsDictionary != null)
            {
                var assetObject = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                AssetsWithMissingRef.Add(new AssetMissingRef(assetObject, componentsDictionary));
            }
        }
        
    }

    private static Dictionary<string, int> AnalyzeAsset(string assetPath) //key - component name; value - number of missing refs in this component
    {
        var componentsWithMissingRefs = new Dictionary<string, int>();
        var assetText = System.IO.File.ReadAllLines(assetPath, Encoding.Default);
        var isAnyMissingRef = false;
        
        string componentName = null;
        foreach (var assetString in assetText)
        {
            if (IsComponentName(assetString)) 
            {
                componentName = assetString.TrimEnd(':');
                try
                {
                    componentsWithMissingRefs.Add(componentName, 0);
                }
                catch
                {
                    var newComponentName = componentName + "";
                }

                continue;
            }

            if (assetString.Contains("guid:"))
            {
                var guidIndex = assetString.IndexOf("guid:", StringComparison.Ordinal);
                var guid = assetString.Substring(guidIndex + 6, 32); // 6 - length of "guid: ", 32 - length of every GUID
                if (!TryAccessAsset(guid))
                {
                    if (componentName != null) componentsWithMissingRefs[componentName]++;
                    if (!isAnyMissingRef) isAnyMissingRef = true;
                }
            }
        }
        return componentsWithMissingRefs;
    }

    private static bool TryAccessAsset(string guid)
    {
        try
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);

            return asset != null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }   
    }

    private static bool IsComponentName(string assetString)
    {
        return !assetString.StartsWith(' ') && !assetString.StartsWith('-') && !assetString.StartsWith('%');
    }
}
