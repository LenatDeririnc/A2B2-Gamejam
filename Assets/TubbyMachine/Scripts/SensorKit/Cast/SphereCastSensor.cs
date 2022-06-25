using System;
using UnityEngine;

namespace ThreeDISevenZeroR.SensorKit
{
    [Serializable]
    public class SphereCastSensor : CastSensor
    {
        /// <summary>
        /// <para>Radius of sphere, when sphere radius is zero, behaves like a ray</para>
        /// </summary>
        [Tooltip("Radius of sphere, when sphere radius is zero, behaves like a ray")]
        public float radius;

        /// <summary>
        /// <para>Width of sphere, when non zero, makes this sensor behave like a capsule, when zero, behaves like a sphere</para>
        /// </summary>
        [Tooltip("Width of sphere, when non zero, makes this sensor behave like a capsule, when zero, behaves like a sphere")]
        public float width;
        
        /// <summary>
        /// <para>The direction of the capsule</para>
        /// </summary>
        [Tooltip("The direction of the capsule")]
        public AxisDirection widthAxis;
        
        protected override int DoCastQuery(CastQuery query, RaycastHit[] outHits)
        {
            var castRadius = PhysicsSensorUtils.GetScaledCapsuleRadius(radius, query.scale);
            var ray = query.ray;
            
            if (width != 0)
            {
                PhysicsSensorUtils.GetCapsulePoints(ray.origin, query.rotation,
                    width, query.scale, widthAxis, out var p1, out var p2);

                if (outHits.Length == 1)
                {
                    return PhysicsScene.CapsuleCast(p1, p2, castRadius, ray.direction, out outHits[0], query.distance,
                        layerMask, queryTriggerInteraction) ? 1 : 0;
                }
                
                return PhysicsScene.CapsuleCast(p1, p2, castRadius, ray.direction, outHits, query.distance,
                    layerMask, queryTriggerInteraction);
            }

            if (castRadius != 0)
            {
                if (outHits.Length == 1)
                {
                    return PhysicsScene.SphereCast(ray.origin, castRadius, ray.direction, out outHits[0], query.distance, 
                        layerMask, queryTriggerInteraction) ? 1 : 0;
                }
                
                return PhysicsScene.SphereCast(ray.origin, castRadius, ray.direction, outHits, query.distance, 
                    layerMask, queryTriggerInteraction);
            }

            if (outHits.Length == 1)
            {
                return PhysicsScene.Raycast(ray.origin, ray.direction, out outHits[0], query.distance,
                    layerMask, queryTriggerInteraction) ? 1 : 0;
            }
            
            return PhysicsScene.Raycast(ray.origin, ray.direction, outHits, query.distance,
                layerMask, queryTriggerInteraction);
        }

        protected override void DrawColliderShape(Vector3 center, Quaternion rotation, Vector3 scale)
        {
            if (width != 0)
            {
                PhysicsSensorUtils.DrawCapsuleGizmo(center, rotation, scale, width, radius, widthAxis);
            }
            else if (radius != 0)
            {
                PhysicsSensorUtils.DrawSphereGizmo(center, rotation, scale, radius);
            }
        }
    }
}