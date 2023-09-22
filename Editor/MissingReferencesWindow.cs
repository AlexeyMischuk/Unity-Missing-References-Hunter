using UnityEditor;
using UnityEngine;

public class MissingReferencesWindow : EditorWindow
{
    private Vector2 _scrollPosition;
    private Object _source;
    
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
            Debug.Log("Button clicked!");
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        
        GUILayout.Space(30);

        _scrollPosition = GUILayout.BeginScrollView(
            _scrollPosition, GUILayout.MaxWidth(position.width-10));
        GUILayout.BeginVertical();
        for (var i = 1; i <= 50; i++)
        {
            GUILayout.BeginHorizontal();
            _source = EditorGUILayout.ObjectField(_source, typeof(Object), true, GUILayout.Width(250));
            GUILayout.Label("asset path");
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
    }
}
