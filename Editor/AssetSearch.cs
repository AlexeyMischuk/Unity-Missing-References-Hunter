using UnityEditor;

public static class AssetSearch
{
    public static string[] AssetArray { get; private set; }

    public static void FindAllAssets()
    {
        AssetArray = AssetDatabase.FindAssets("",new []{"Assets/"});
    }

    public static void FindAllAssets(string searchPath)
    {
        AssetArray = AssetDatabase.FindAssets("", new[] { searchPath });
    }
}
