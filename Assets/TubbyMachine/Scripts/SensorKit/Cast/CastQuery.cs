using UnityEngine;

namespace ThreeDISevenZeroR.SensorKit
{
    public struct CastQuery
    {
        public Ray ray;
        public float distance;
        public Quaternion rotation;
        public Vector3 scale;
    }
}