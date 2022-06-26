using UnityEditor;
using UnityEngine;

namespace Common.MainMenu.SettingsMenu
{
    [CustomEditor(typeof(SetMonitor))]
    public class SetMonitorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space();
            var targetObject = (SetMonitor) target;
            if (GUILayout.Button("Clear Player Prefs"))
            {
                PlayerPrefs.DeleteKey(SetMonitor.PlayerPrefsSelectedDisplayKey);
            };
        }
    }
}