using System;

namespace ThreeDISevenZeroR.Utils
{
    [Serializable]
    public struct Spring
    {
        public float mass;
        public float stiffness;
        public float damping;

        public bool isValid()
        {
            return mass > 0f && stiffness > 0f && damping > 0f;
        }

        public static Spring GetDefault()
        {
            return new()
            {
                mass = 1f,
                stiffness = 20f,
                damping = 2.5f
            };
        }
    }
}