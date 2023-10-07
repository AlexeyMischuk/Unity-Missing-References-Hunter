using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;

public static class AssetSearch
{
    public static List<string> AssetsList { get; private set; }

    public static void FindAllAssets()
    {
        AssetsList = CheckForFolders(
            AssetDatabase.FindAssets("",new []{"Assets/"}));
    }

    public static void FindAllAssets(string searchPath)
    {
        AssetsList = CheckForFolders(
            AssetDatabase.FindAssets("", new[] { searchPath }));
    }

    private static List<string> CheckForFolders(string[] allAssets)
    {
        var assetsWithoutFolders = new List<string>();
        foreach (var assetGuid in allAssets)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            const string pattern = @"(?!.*\.cs$)\.[^.]+$"; // any file except .cs
            if (Regex.IsMatch(assetPath, pattern))
            {
                assetsWithoutFolders.Add(assetPath);
            }
        }
        return assetsWithoutFolders;
    }
}
