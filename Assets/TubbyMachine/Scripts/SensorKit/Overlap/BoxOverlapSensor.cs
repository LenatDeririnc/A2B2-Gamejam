using System;
using UnityEngine;

namespace ThreeDISevenZeroR.SensorKit
{
    [Serializable]
    public class BoxOverlapSensor : OverlapSensor
    {
        /// <summary>
        /// <para>Half extents of box</para>
        /// </summary>
        [Tooltip("Half extents of box")]
        public Vector3 halfExtents;
        
        protected override int DoOverlapCheck(Vector3 center, Quaternion rotation, Vector3 scale, Collider[] colliders)
        {
            var scaledExtents = PhysicsSensorUtils.GetScaledBoxRadius(halfExtents, scale);

            return PhysicsScene.OverlapBox(center, scaledExtents, colliders, rotation, 
                layerMask, queryTriggerInteraction);
        }

        protected override void DrawColliderShape(Vector3 center, Quaternion rotation, Vector3 scale)
        {
            PhysicsSensorUtils.DrawBoxGizmo(center, rotation, scale, halfExtents);
        }
    }
}