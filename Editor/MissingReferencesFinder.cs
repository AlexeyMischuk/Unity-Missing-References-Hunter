using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using Object = UnityEngine.Object;

public static class MissingReferencesFinder
{
    public static List<Object> AssetsWithMissingRef { get; } = new();

    public static void FindAssets(List<string> assetsList)
    {
        AssetsWithMissingRef.Clear();
        
        foreach (var assetPath in assetsList)
        {
            if (AnalyzeAsset(assetPath))
            {
                var assetObject = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                AssetsWithMissingRef.Add(assetObject);
            }
        }
    }

    private static bool AnalyzeAsset(string assetPath)
    {
        var assetText = System.IO.File.ReadAllLines(assetPath, Encoding.Default);
        
        foreach (var assetString in assetText)
        {
            if (assetString.Contains("guid:"))
            {
                var guidIndex = assetString.IndexOf("guid:", StringComparison.Ordinal);
                var guid = assetString.Substring(guidIndex + 6, 32); // 6 - length of "guid: ", 32 - length of every GUID
                if (!TryAccessAsset(guid))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static bool TryAccessAsset(string guid)
    {
        var assetPath = AssetDatabase.GUIDToAssetPath(guid); 
        return !string.IsNullOrEmpty(assetPath);
    }
}
