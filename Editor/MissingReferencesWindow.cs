using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class MissingReferencesWindow : EditorWindow
{
    private Vector2 _scrollPosition;
    private Vector2 _buttonsScrollPosition;
    private int _currentPage;
    private int _totalPages;
    private bool _isSearchFinished;
    private bool[] _foldoutFlag;
    
    private static List<Object> _missingRefObjects;

    private const int ItemsPerPage = 10;
    private const int ObjectFieldWidth = 250;
    private const int ButtonWidth = 130;
    
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
            AssetSearch.FindAssets();
            MissingReferencesFinder.CheckAssets(AssetSearch.AssetsList);
            _missingRefObjects = MissingReferencesFinder.ObjectIndex;
            _totalPages = Mathf.CeilToInt((float)_missingRefObjects.Count / ItemsPerPage);
            _foldoutFlag = new bool[_totalPages * ItemsPerPage];
            _isSearchFinished = true;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        if (_isSearchFinished)
        {
            if (_totalPages > 1)
            {
                var pageButtonStyle = new GUIStyle(GUI.skin.button)
                {
                    fixedWidth = 30
                };
            
                _buttonsScrollPosition = GUILayout.BeginScrollView(_buttonsScrollPosition, GUILayout.Height(50));
                GUILayout.BeginHorizontal();
                for (var i = 1; i <= _totalPages; i++)
                {
                    if (GUILayout.Button($"{i}", pageButtonStyle))
                    {
                        _currentPage = i - 1;
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.EndScrollView();
            }
            
            DrawSeparator(Color.black);
            
            _scrollPosition = GUILayout.BeginScrollView(
                _scrollPosition, GUILayout.MaxWidth(position.width-10));
            GUILayout.BeginVertical();
            
            var startIndex = _currentPage * ItemsPerPage;
            var endIndex = (_currentPage * ItemsPerPage) + ItemsPerPage;
            if (_missingRefObjects.Count < endIndex)
            {
                endIndex = _missingRefObjects.Count;
            }

            for (var i = startIndex; i < endIndex; i++)
            {
                CreateContentRow(i);
            }
            
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.Space(15);
        }
    }

    private void CreateContentRow(int assetIndex)
    {
        var assetObject = _missingRefObjects[assetIndex];
        var childrenObjects = MissingReferencesFinder.GetChildrenInformation(assetObject);
        
        if (assetIndex % ItemsPerPage != 0) 
            DrawSeparator(Color.gray);
        EditorGUILayout.ObjectField(assetObject, typeof(Object), true, GUILayout.Width(ObjectFieldWidth));

        if(childrenObjects != null)
        {
            CreateChildrenObjects(childrenObjects, assetIndex);
        }
    }

    private void CreateChildrenObjects(List<ChildObject> childrenList, int assetIndex)
    {
        _foldoutFlag[assetIndex] = EditorGUILayout.Foldout(_foldoutFlag[assetIndex], "Objects with missing references", true);
        if (_foldoutFlag[assetIndex])
        {
            GUILayout.BeginHorizontal(GUILayout.Width(ButtonWidth));
            foreach (var child in childrenList)
            {
                EditorGUILayout.BeginVertical();
                if(GUILayout.Button(child.ObjectRef.name, GUILayout.Width(ButtonWidth)))
                {
                    Selection.activeObject = child.ObjectRef;
                }
                foreach (var comp in child.ComponentName)       
                {
                    EditorGUILayout.LabelField(comp, GUILayout.Width(ButtonWidth));
                }
                EditorGUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
        }
    }

    private void DrawSeparator(Color color)
    {
        EditorGUILayout.Space();
        Rect lineRect = EditorGUILayout.GetControlRect(false, 1);
        lineRect.height = 1;
        EditorGUI.DrawRect(lineRect, color);
        EditorGUILayout.Space();
    }
}