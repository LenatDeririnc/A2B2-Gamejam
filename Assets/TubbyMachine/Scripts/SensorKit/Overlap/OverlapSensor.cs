using UnityEngine;

namespace ThreeDISevenZeroR.SensorKit
{
    public abstract class OverlapSensor : PhysicsSensor<OverlapQuery>
    {
        private bool _hasLastQuery;
        private OverlapQuery _lastQuery;

        public override bool Query(OverlapQuery query)
        {
            _hasLastQuery = true;
            _lastQuery = query;
            _hitCount = DoOverlapCheck(query.center, query.rotation, query.scale, HitColliders);
            return _hitCount > 0;
        }

        public override void Clear()
        {
            _hasLastQuery = false;
            _lastQuery = default;
            base.Clear();
        }

        protected abstract int DoOverlapCheck(Vector3 center, Quaternion rotation, Vector3 scale,
            Collider[] colliders);

        public override void DrawLastQueryGizmo()
        {
            if (!_hasLastQuery)
                return;

            Gizmos.color = HasHit ? PhysicsSensorUtils.hasHitColor : PhysicsSensorUtils.noHitColor;
            DrawColliderShape(_lastQuery.center, _lastQuery.rotation, _lastQuery.scale);
        }

        public override void DrawQueryPreviewGizmo(OverlapQuery query)
        {
            var count = DoOverlapCheck(query.center, query.rotation, query.scale, _gizmoCollider);
            Gizmos.color = count != 0 ? PhysicsSensorUtils.hasHitColor : PhysicsSensorUtils.noHitColor;
            DrawColliderShape(query.center, query.rotation, query.scale);
        }

        private static readonly Collider[] _gizmoCollider = new Collider[1];
    }
}