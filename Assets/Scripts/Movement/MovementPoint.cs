using System;
using Cinemachine;
using UnityEngine;

namespace Movement
{
    public class MovementPoint : MonoBehaviour
    {
        private Transform _transform;

        public Transform Transform
        {
            get
            {
                if (_transform == null)
                    _transform = transform;
                return _transform;
            }
        }

        public CinemachineVirtualCamera VirtualCamera;

        public MovementPoint ForwardPoint;
        public MovementPoint BackPoint;
        public MovementPoint LeftPoint;
        public MovementPoint RightPoint;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            var t = transform;
            Vector3 tpos = t.position;
            Vector3 tfofward = t.forward;
            Vector3 tright = t.right;
            Vector3 tup = t.up;
            Gizmos.DrawLine(tpos, tpos + tfofward * 2);
            Gizmos.DrawLine(tpos + tfofward * 2, tpos + tfofward * 1 + tright * 1);
            Gizmos.DrawLine(tpos + tfofward * 2, tpos + tfofward * 1 - tright * 1);
        }

        public void Enable()
        {
            VirtualCamera.Priority = 100;
        }

        public void Disable()
        {
            VirtualCamera.Priority = 0;
        }
    }
}