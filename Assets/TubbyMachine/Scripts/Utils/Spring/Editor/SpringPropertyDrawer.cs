using UnityEditor;
using UnityEngine;

namespace ThreeDISevenZeroR.Utils
{
    [CustomPropertyDrawer(typeof(Spring))]
    public class SpringPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var multiProperty = property.Copy();
            multiProperty.Next(true);
            
            EditorGUI.MultiPropertyField(position, new GUIContent[]
            {
                new("M", "Mass"), 
                new("S", "Stiffness"), 
                new("D", "Damping")
            }, multiProperty, label);
            
            var massProperty = property.FindPropertyRelative(nameof(Spring.mass));
            var stiffnessProperty = property.FindPropertyRelative(nameof(Spring.stiffness));
            var dampingProperty = property.FindPropertyRelative(nameof(Spring.damping));
            
            var indentedPosition = EditorGUI.IndentedRect(position);
            var bgRect = Rect.MinMaxRect(indentedPosition.xMin, indentedPosition.yMin + EditorGUIUtility.singleLineHeight, 
                indentedPosition.xMax, indentedPosition.yMax);
            
            var previewRect = Rect.MinMaxRect(
                bgRect.xMin + BgPadding, bgRect.yMin + BgPadding, 
                bgRect.xMax - BgPadding, bgRect.yMax - BgPadding);
            
            GUI.Box(bgRect, $"Preview, {PreviewDurationSeconds} seconds", new GUIStyle(EditorStyles.helpBox)
            {
                alignment = TextAnchor.LowerRight
            });
            
            DrawSpringPreview(new Spring
            {
                mass = massProperty.floatValue,
                stiffness = stiffnessProperty.floatValue,
                damping = dampingProperty.floatValue
            }, previewRect);
        }

        private static void DrawSpringPreview(Spring spring, Rect rect)
        {
            var yCurrent = 0f;
            var yTarget = rect.height / 2f;
            var yVelocity = 0f;
            var rectWidth = rect.width;

            for (var i = 0; i < SharedPreviewArray.Length; i++)
            {
                var xCurrent = rectWidth * (i / (SharedPreviewArray.Length - 1f));
                var value = new Vector3(xCurrent, yCurrent);
                
                AnimationUtils.SolveSpring(spring, ref yCurrent, ref yVelocity, yTarget, DeltaTime);

                SharedPreviewArray[i] = value;
            }

            GUI.BeginClip(rect);
            Handles.color = spring.isValid() 
                ? new Color(0f, 1f, 0f, 0.5f) 
                : new Color(1f, 0f, 0f, 0.5f);
            
            Handles.DrawAAPolyLine(Texture2D.whiteTexture, 1f, SharedPreviewArray);
            GUI.EndClip();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 100 + EditorGUIUtility.singleLineHeight;
        }

        private const int PreviewFps = 120;
        private const float PreviewDurationSeconds = 3f;
        private const float DeltaTime = 1f / PreviewFps;
        private const float BgPadding = 2;
        
        private static readonly Vector3[] SharedPreviewArray = new Vector3[(int) (PreviewFps * PreviewDurationSeconds)];
    }
}