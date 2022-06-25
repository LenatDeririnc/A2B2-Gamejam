using System;
using UnityEngine;

namespace ThreeDISevenZeroR.SensorKit
{
    [Serializable]
    public class BoxCylinderCastSensor : CastSensor
    {
        /// <summary>
        /// <para>Amount of box casts to estimate cylinder cast</para>
        /// <para>Higher amount creates more precise cylinder shape, but slower</para>
        /// </summary>
        [Tooltip("Amount of box casts to estimate cylinder cast")]
        public int boxCastCount = 4;
        
        /// <summary>
        /// <para>Radius of sphere, when sphere radius is zero, behaves like a ray</para>
        /// </summary>
        [Tooltip("Radius of sphere, when sphere radius is zero, behaves like a ray")]
        public float radius = 0.5f;
        
        /// <summary>
        /// <para>Height of cylinder</para>
        /// </summary>
        [Tooltip("Height of cylinder")]
        public float height = 0.5f;

        /// <summary>
        /// <para>The direction of the cylinder</para>
        /// </summary>
        [Tooltip("The direction of the cylinder")]
        public AxisDirection cylinderAxis;
        
        private float GetAnglePerCast() => 180f / boxCastCount;

        private Quaternion GetRotationOffset(AxisDirection axis, float halfRotationAxis)
        {
            switch (axis)
            { 
                case AxisDirection.XAxis: return Quaternion.Euler(halfRotationAxis, 0, 90);
                case AxisDirection.YAxis: return Quaternion.Euler(0, halfRotationAxis, 0);
                case AxisDirection.ZAxis: return Quaternion.Euler(halfRotationAxis, 90, 90);
                    
                default: return Quaternion.identity;
            }
        }
        
        private Vector3 GetHalfExtents(Vector3 scale, float anglePerCast)
        {
            var forward = new Vector3(0, 0, PhysicsSensorUtils.GetScaledSphereRadius(radius, scale));
            var rotatedForward = Quaternion.AngleAxis(anglePerCast, Vector3.up) * forward;
            var edgeCenter = (forward + rotatedForward) / 2f;
            var boxWidth = Vector3.Distance(forward, rotatedForward);
            return new Vector3(edgeCenter.magnitude, height / 2f, boxWidth / 2f);
        }
        
        protected override int DoCastQuery(CastQuery query, RaycastHit[] outHits)
        {
            var ray = query.ray;
            
            if (radius == 0)
            {
                return PhysicsScene.Raycast(ray.origin, ray.direction, outHits, query.distance, 
                    layerMask, queryTriggerInteraction);
            }
            
            var anglePerCast = GetAnglePerCast();
            var currentRotation = query.rotation * GetRotationOffset(cylinderAxis, anglePerCast / 2f);
            var halfExtents = GetHalfExtents(query.scale, anglePerCast);
            
            var closestHitDistance = float.MaxValue;
            var hasHit = false;

            for (var i = 0; i < boxCastCount; i++)
            {
                var castRotation = currentRotation * Quaternion.AngleAxis(anglePerCast * i, Vector3.up);

                if (PhysicsScene.BoxCast(ray.origin, halfExtents, ray.direction, out var boxHitInfo,
                        castRotation, query.distance, layerMask, queryTriggerInteraction) && 
                    closestHitDistance > boxHitInfo.distance)
                {
                    closestHitDistance = boxHitInfo.distance;
                    outHits[0] = boxHitInfo;
                    hasHit = true;
                }
            }

            return hasHit ? 1 : 0;
        }

        protected override void DrawColliderShape(Vector3 center, Quaternion rotation, Vector3 scale)
        {
            var anglePerCast = GetAnglePerCast();
            var currentRotation = rotation * GetRotationOffset(cylinderAxis, anglePerCast / 2f);
            var halfExtents = GetHalfExtents(scale, anglePerCast);

            for (var i = 0; i < boxCastCount; i++)
            {
                var castRotation = currentRotation * Quaternion.AngleAxis(anglePerCast * i, Vector3.up);
                Gizmos.matrix = Matrix4x4.TRS(center, castRotation, Vector3.one);
                Gizmos.DrawWireCube(Vector3.zero, halfExtents * 2);
            }
            Gizmos.matrix = Matrix4x4.identity;
        }
    }
}