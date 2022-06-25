using UnityEditor;
using UnityEngine;

namespace Movement.Editor
{
    [CustomEditor(typeof(MovementPoint))]
    public class MovementPointEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space();
            var targetObject = (MovementPoint) target;
            if (GUILayout.Button("Enable"))
            {
                targetObject.Enable();
            };
            
            if (GUILayout.Button("Disable"))
            {
                targetObject.Disable();
            };
        }
    }
}