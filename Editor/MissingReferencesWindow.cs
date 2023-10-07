using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class MissingReferencesWindow : EditorWindow
{
    private Vector2 _scrollPosition;
    private int _currentPage;
    private int _totalPages;
    private bool _isSearchFinished;
    private static List<Object> _missingRefObjects;

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
            MissingReferencesFinder.FindAssets(AssetSearch.AssetsList);
            _missingRefObjects = MissingReferencesFinder.AssetsWithMissingRef;
            _totalPages = Mathf.CeilToInt((float)_missingRefObjects.Count / ItemsPerPage);
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
            
            var startIndex = _currentPage * ItemsPerPage;
            var endIndex = (_currentPage * ItemsPerPage) + ItemsPerPage;
            if (_missingRefObjects.Count < endIndex)
            {
                endIndex = _missingRefObjects.Count == 1 ? 1 : _missingRefObjects.Count - 1;
            }

            for (var i = startIndex; i < endIndex; i++)
            {
                CreateContentRow(i);
            }
            
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.Space(5);
        }
    }

    private static void CreateContentRow(int assetIndex)
    {
        var assetObject = _missingRefObjects[assetIndex];
        GUILayout.BeginHorizontal();
        EditorGUILayout.ObjectField(assetObject, typeof(Object), true, GUILayout.Width(250));
        GUILayout.Label("asset path");
        GUILayout.EndHorizontal();  
    }
}
