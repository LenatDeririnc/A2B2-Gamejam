using System;
using UnityEngine;

namespace ThreeDISevenZeroR.SensorKit
{
    [Serializable]
    public abstract class CastSensor : PhysicsSensor<CastQuery>
    {
        /// <summary>
        /// <para>Ray that was used in last update</para>
        /// </summary>
        public Ray LastRay => _lastQuery.ray;
        
        /// <summary>
        /// <para>Returns first RayHit</para>
        /// <para>Convenience method when maxCount is 1</para>
        /// </summary>
        public RaycastHit RayHit => HitCount > 0 ? RayHits[0] : default;

        /// <summary>
        /// <para>Array with all hits that have been detected during sensor update</para>
        /// <para>This array is cached, and guaranteed to be at least HitCount long</para>
        /// </summary>
        public RaycastHit[] RayHits
        {
            get
            {
                EnsureArrayCapacity(ref _rayHits);
                return _rayHits;
            }
        }
        
        /// <summary>
        /// <para>Returns closest RayHit</para>
        /// <para>Since NonAlloc methods returns array with no order,
        /// method finds most closest hit in result array</para>
        /// </summary>
        public RaycastHit ClosestRayHit
        {
            get
            {
                if (_hitCount == 1)
                    return _rayHits[0];

                if (_hitCount == 0)
                    return default;

                if (_lastClosestIndex == -1)
                {
                    var closestDistance = float.MaxValue;

                    for (var i = 0; i < _hitCount; i++)
                    {
                        var distance = _rayHits[i].distance;

                        if (distance < closestDistance)
                        {
                            _lastClosestIndex = i;
                            closestDistance = distance;
                        }
                    }
                }
                
                return _rayHits[_lastClosestIndex];
            }
        }
        
        public override Collider[] HitColliders
        {
            get
            {
                if (_isCollidersOutdated)
                {
                    UpdateCollidersArray();
                    _isCollidersOutdated = false;
                }

                return _hitColliders;
            }
        }

        private bool _hasLastQuery;
        private CastQuery _lastQuery;
        private RaycastHit[] _rayHits = _emptyRayHits;
        private int _lastClosestIndex = -1;
        private bool _isCollidersOutdated;

        public override bool Query(CastQuery query)
        {
            EnsureArrayCapacity(ref _hitColliders);
            EnsureArrayCapacity(ref _rayHits);
            _hasLastQuery = true;
            _lastQuery = query;
            _hitCount = DoCastQuery(query, _rayHits);
            _isCollidersOutdated = true;
            return _hitCount > 0;
        }
        
        public override void Clear()
        {
            _hasLastQuery = false;
            _lastQuery = default;
            _isCollidersOutdated = false;
            _lastClosestIndex = -1;
            
            for (var i = _rayHits.Length - 1; i >= 0; i--)
                _rayHits[i] = default;

            base.Clear();
        }

        protected abstract int DoCastQuery(CastQuery query, RaycastHit[] outHits);
        
        private void UpdateCollidersArray()
        {
            for (var i = 0; i < _hitCount; i++)
                _hitColliders[i] = _rayHits[i].collider;

            for (var i = _hitCount; i < _hitColliders.Length; i++)
                _hitColliders[i] = null;
        }

        public override void DrawQueryPreviewGizmo(CastQuery query)
        {
            EnsureArrayCapacity(ref _gizmoRayHits);
            var gizmoCount = DoCastQuery(query, _gizmoRayHits);
            DrawCastGizmo(query, _gizmoRayHits, gizmoCount);
        }

        public override void DrawLastQueryGizmo()
        {
            if(_hasLastQuery)
                DrawCastGizmo(_lastQuery, _rayHits, _hitCount);
        }

        private void DrawCastGizmo(CastQuery query, RaycastHit[] hits, int hitCount)
        {
            var rayEnd = query.ray.GetPoint(Mathf.Clamp(query.distance, 0f, 100000f));
            
            if (hitCount > 0)
            {
                for (var i = 0; i < hitCount; i++)
                {
                    var gizmoHit = hits[i];
                    var collisionPoint = query.ray.GetPoint(gizmoHit.distance);

                    Gizmos.color = PhysicsSensorUtils.hasHitColor;
                    Gizmos.DrawLine(query.ray.origin, collisionPoint);
                    DrawColliderShape(collisionPoint, query.rotation, query.scale);
                    Gizmos.matrix = Matrix4x4.identity;
                    Gizmos.color = PhysicsSensorUtils.rayEndColor;
                    Gizmos.DrawLine(collisionPoint, rayEnd);

                    PhysicsSensorUtils.DrawNormal(gizmoHit);
                    PhysicsSensorUtils.DrawCollisionPoints(collisionPoint, gizmoHit);
                    PhysicsSensorUtils.HighlightMeshVertices(gizmoHit);
                    PhysicsSensorUtils.DrawHitInfo(gizmoHit, collisionPoint);
                }
            }
            else
            {
                Gizmos.color = PhysicsSensorUtils.noHitColor;
                Gizmos.DrawLine(query.ray.origin, rayEnd);
                DrawColliderShape(rayEnd, query.rotation, query.scale);
            }
        }
        
        private static RaycastHit[] _emptyRayHits = new RaycastHit[0];
        private static RaycastHit[] _gizmoRayHits = _emptyRayHits;
    }
}