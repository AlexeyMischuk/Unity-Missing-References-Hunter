using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class AssetSearch
{
    public static List<string> AssetsList { get; } = new();
    private static SearchTypes _types;

    public static void FindAssets()
    {
        AssetsList.Clear();
        var searchFilter = InitializeSearchExtensions();
        AssetsList.AddRange(AssetDatabase.FindAssets(searchFilter, new[] { "Assets/" }));
    }

    private static string InitializeSearchExtensions()
    {
        var extensionsGuid = AssetDatabase.FindAssets("SearchTypes t:ScriptableObject", new[] { "Assets/Editor" });
        var extensionsPath = AssetDatabase.GUIDToAssetPath(extensionsGuid.First());
        _types = AssetDatabase.LoadAssetAtPath<SearchTypes>(extensionsPath);
        
        if (_types == null)
        {
            Debug.LogError("SearchTypes cannot be loaded");
            return null;
        }

        StringBuilder searchFilter = new();
        foreach (var type in _types.searchTypes)
        {
            searchFilter.Append($"t:{type} ");
        }
        return searchFilter.ToString();
    }
}
