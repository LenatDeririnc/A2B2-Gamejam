using Movement;
using UnityEditor;
using UnityEngine;

namespace SystemInitializer.Systems.Movement.Editor
{
    [CustomEditor(typeof(MovementContext))]
    public class MovementContextEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var targetValue = (MovementContext)target;
            
            if (GUILayout.Button("Disable All Positions"))
            {
                var points = FindObjectsOfType<MovementPoint>();
                foreach (var point in points)
                {
                    point.Disable();
                }
            };
            
            if (!Application.isPlaying)
                return;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Current Point: ", targetValue.CurrentMovementPoint.name);
        }
    }
}