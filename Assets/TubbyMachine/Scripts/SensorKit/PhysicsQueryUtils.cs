using UnityEngine;

namespace ThreeDISevenZeroR.SensorKit
{
    public static class PhysicsQueryUtils
    {
        public static OverlapQuery TransformQuery(Transform transform)
        {
            return new OverlapQuery
            {
                center = transform.position,
                rotation = transform.rotation,
                scale = transform.lossyScale
            };
        }
        
        public static CastQuery CreateLocalQuery(Transform transform, Vector3 localDirection, 
            float castDistance, float negativeCastDistance = 0f)
        {
            var castPosition = negativeCastDistance != 0 
                ? transform.TransformPoint(localDirection * -negativeCastDistance) 
                : transform.position;

            var rayDirection = transform.TransformVector(localDirection);
            var actualDistance = (castDistance + negativeCastDistance) * rayDirection.magnitude;

            return new CastQuery
            {
                distance = actualDistance,
                ray = new Ray(castPosition, rayDirection),
                rotation = transform.rotation,
                scale = transform.lossyScale
            };
        }
    }
}