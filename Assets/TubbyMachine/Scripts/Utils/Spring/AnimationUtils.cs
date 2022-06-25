using UnityEngine;

namespace ThreeDISevenZeroR.Utils
{
    public static class AnimationUtils
    {
        public static bool SolveSpring(
            Spring spring,
            ref float current,
            ref float velocity,
            float target,
            float deltaTime)
        {
            if (current == target && velocity == 0f)
                return false;
            
            if (deltaTime > 0.02f)
                deltaTime = 0.02f;

            var fSpring = -spring.stiffness * (current - target);
            var fDamping = -spring.damping * velocity;
            var velocityChange = (fSpring + fDamping) / spring.mass;

            velocity += velocityChange * deltaTime;
            current += velocity * deltaTime;
            
            if (Mathf.Approximately(current, target) && Mathf.Approximately(velocity, 0f))
            {
                velocity = 0f;
                current = target;
            }

            return true;
        }
    }
}