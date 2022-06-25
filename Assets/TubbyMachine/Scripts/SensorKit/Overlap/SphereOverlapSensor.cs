using System;
using ThreeDISevenZeroR.SensorKit;
using UnityEngine;

namespace ThreeDISevenZeroR.SensorKit
{
    [Serializable]
    public class SphereOverlapSensor : OverlapSensor
    {
        /// <summary>
        /// <para>Radius of sphere</para>
        /// </summary>
        [Tooltip("Radius of sphere")]
        public float radius;

        /// <summary>
        /// <para>Width of sphere, when non zero, makes this sensor behave like a capsule</para>
        /// </summary>
        [Tooltip("Width of sphere, when non zero, makes this sensor behave like a capsule")]
        public float width;

        /// <summary>
        /// The direction of the capsule
        /// </summary>
        [Tooltip("The direction of the capsule")]
        public AxisDirection widthAxis;
        
        protected override int DoOverlapCheck(Vector3 center, Quaternion rotation, Vector3 scale, Collider[] colliders)
        {
            var scaledRadius = PhysicsSensorUtils.GetScaledCapsuleRadius(radius, scale);
            
            if (width != 0)
            {
                PhysicsSensorUtils.GetCapsulePoints(center, rotation, width, 
                    scale, widthAxis, out var p1, out var p2);
    
                return PhysicsScene.OverlapCapsule(p1, p2, scaledRadius,
                    colliders, layerMask, queryTriggerInteraction);
            }

            return PhysicsScene.OverlapSphere(center, scaledRadius, colliders, layerMask, queryTriggerInteraction);
        }
        
        protected override void DrawColliderShape(Vector3 center, Quaternion rotation, Vector3 scale)
        {
            if (width != 0)
            {
                PhysicsSensorUtils.DrawCapsuleGizmo(center, rotation, scale, width, radius, widthAxis);
            }
            else
            {
                PhysicsSensorUtils.DrawSphereGizmo(center, rotation, scale, radius);
            }
        }
    }
}