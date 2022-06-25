using System;
using UnityEngine;

namespace ThreeDISevenZeroR.SensorKit
{
    [Serializable]
    public class BoxCastSensor : CastSensor
    {
        /// <summary>
        /// <para>Half extents of box</para>
        /// </summary>
        [Tooltip("Half extents of box")]
        public Vector3 halfExtents;
        
        protected override int DoCastQuery(CastQuery query, RaycastHit[] outHits)
        {
            var ray = query.ray;
            var extents = PhysicsSensorUtils.GetScaledBoxRadius(halfExtents, query.scale);

            if (outHits.Length == 1)
            {
                return PhysicsScene.BoxCast(ray.origin, extents, ray.direction, out outHits[0], 
                    query.rotation, query.distance, layerMask, queryTriggerInteraction) ? 1 : 0;
            }
            
            return PhysicsScene.BoxCast(ray.origin, extents, ray.direction, outHits, 
                query.rotation, query.distance, layerMask, queryTriggerInteraction);
        }

        protected override void DrawColliderShape(Vector3 center, Quaternion rotation, Vector3 scale)
        {
            PhysicsSensorUtils.DrawBoxGizmo(center, rotation, scale, halfExtents);
        }
    }
}