using System;
using UnityEngine;

namespace ThreeDISevenZeroR.Utils
{
    [Serializable]
    public abstract class SpringTween<T> where T : struct
    {
        public Spring spring = Spring.GetDefault();
        
        public T current;
        public T velocity;
        public T target;

        public abstract bool Update(float deltaTime);

        public void Finish()
        {
            current = target;
            velocity = default;
        }
    }

    [Serializable]
    public class SpringFloatTween : SpringTween<float>
    {
        public override bool Update(float deltaTime)
        {
            return AnimationUtils.SolveSpring(spring, ref current, ref velocity, target, deltaTime);
        }
    }
    
    [Serializable]
    public class SpringVector2Tween : SpringTween<Vector2>
    {
        public override bool Update(float deltaTime)
        {
            return AnimationUtils.SolveSpring(spring, ref current.x, ref velocity.x, target.x, deltaTime) |
                   AnimationUtils.SolveSpring(spring, ref current.y, ref velocity.y, target.y, deltaTime);
        }
    }
    
    [Serializable]
    public class SpringVector3Tween : SpringTween<Vector3>
    {
        public override bool Update(float deltaTime)
        {
            return AnimationUtils.SolveSpring(spring, ref current.x, ref velocity.x, target.x, deltaTime) | 
                   AnimationUtils.SolveSpring(spring, ref current.y, ref velocity.y, target.y, deltaTime) | 
                   AnimationUtils.SolveSpring(spring, ref current.z, ref velocity.z, target.z, deltaTime);
        }
    }
}