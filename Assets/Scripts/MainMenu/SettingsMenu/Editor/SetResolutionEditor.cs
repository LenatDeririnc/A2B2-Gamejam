using UnityEditor;
using UnityEngine;

namespace Common.MainMenu.SettingsMenu
{
    [CustomEditor(typeof(SetResolution))]
    public class SetResolutionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space();
            var targetObject = (SetResolution) target;
            if (GUILayout.Button("Clear Player Prefs"))
            {
                PlayerPrefs.DeleteKey(SetResolution.PlayerPrefsSelectedScreenKey);
            };
        }
    }
}