using UnityEditor;
using UnityEngine;
using UVariableSystem;

namespace SystemInitializer.Systems.Movement.Editor
{
    [CustomEditor(typeof(MovementContext))]
    public class MovementContextEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (!Application.isPlaying)
                return;

            var targetValue = (MovementContext)target;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Current Point: ", targetValue.CurrentMovementPoint.name);
        }
    }
}