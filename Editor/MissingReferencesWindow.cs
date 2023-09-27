using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class MissingReferencesWindow : EditorWindow
{
    private Vector2 _scrollPosition;
    private int _currentPage;
    private int _totalPages;
    private bool _isSearchFinished;

    private const int ItemsPerPage = 10;
    
    [MenuItem("Window/Missing References Hunter")]
    public static void ShowWindow()
    {
        GetWindow<MissingReferencesWindow>("Missing References Hunter");
    }

    public void OnGUI()
    {
        GUILayout.Space(30);
        
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        var buttonStyle = new GUIStyle(GUI.skin.button)
        {
            fixedWidth = 0,
            stretchWidth = true
        };
        if (GUILayout.Button("Find Missing References", buttonStyle, GUILayout.MaxWidth(300)))
        {
            AssetSearch.FindAllAssets();
            _totalPages = Mathf.CeilToInt((float)AssetSearch.AssetArray.Length / ItemsPerPage);
            _isSearchFinished = true;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        
        GUILayout.Space(10);

        if (_isSearchFinished)
        {
            var pageButtonStyle = new GUIStyle(GUI.skin.button)
            {
                fixedWidth = 30
            };
            
            GUILayout.BeginHorizontal();
            for (var i = 1; i <= _totalPages; i++)
            {
                if (GUILayout.Button($"{i}", pageButtonStyle))
                {
                    _currentPage = i - 1;
                }
            }
            GUILayout.EndHorizontal();
            
            
            _scrollPosition = GUILayout.BeginScrollView(
                _scrollPosition, GUILayout.MaxWidth(position.width-10));
            GUILayout.BeginVertical();
            if (_currentPage == 0)
            {
                for (var j = 0; j < ItemsPerPage; j++)
                {
                    CreateContentRow(j);
                }
            }
            else
            {
                var startIndex = _currentPage * ItemsPerPage;
                var endIndex = startIndex + ItemsPerPage;
                if (_currentPage == _totalPages-1)
                {
                    endIndex = AssetSearch.AssetArray.Length;
                }
                for (var j = startIndex; j < endIndex; j++)
                {
                    CreateContentRow(j);
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.Space(5);
        }
    }

    private static Object RestoreObjectFromGuid(string guid)
    {
        var assetPath = AssetDatabase.GUIDToAssetPath(guid);
        return AssetDatabase.LoadAssetAtPath<Object>(assetPath);
    }

    private static void CreateContentRow(int assetIndex)
    {
        var assetGuid = AssetSearch.AssetArray[assetIndex];
        var assetObject = RestoreObjectFromGuid(assetGuid);
        GUILayout.BeginHorizontal();
        EditorGUILayout.ObjectField(assetObject, typeof(Object), true, GUILayout.Width(250));
        GUILayout.Label("asset path");
        GUILayout.EndHorizontal();  
    }
}
