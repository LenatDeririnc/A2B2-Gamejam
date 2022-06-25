using System;
using UnityEngine;

namespace ThreeDISevenZeroR.SensorKit
{
    [Serializable]
    public abstract class PhysicsSensor<T>
    {
        /// <summary>
        /// <para>Maximum amount of detected hits</para>
        /// <para>Every sensor uses no allocation per cast, and it is important to know maximum amount of
        /// objects this sensor is able to detect, to preallocate array
        /// Changing this property at runtime recreates array, so try to not touch if not necessary</para>
        /// </summary>
        [Tooltip("Maximum amount of detected hits\n" +
                 "Every sensor uses no allocation per cast, and it is important to know maximum amount of objects " +
                 "this sensor is able to detect, to preallocate array. " +
                 "Changing this property at runtime recreates array, so try to not touch if not necessary")]
        public int maxResults = 1;
        
        /// <summary>
        /// <para>Layer mask which sensor will use<para>
        /// </summary>
        [Tooltip("Layer mask which sensor will use")]
        public LayerMask layerMask = Physics.DefaultRaycastLayers;
        
        /// <summary>
        /// <para>Should this sensor detect triggers</para>
        /// </summary>
        [Tooltip("Should this sensor detect triggers")]
        public QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
        
        /// <summary>
        /// <para>Physics scene used for physics checks
        /// Defaults to "Physics.defaultPhysicsScene"</para>
        ///
        /// <para>When set to different scene, it is user responsibility to correctly
        /// handle cases when PhysicsScene is destroyed, sensor will not switch to "Physics.defaultPhysicsScene"
        /// automatically</para>
        /// </summary>
        public PhysicsScene PhysicsScene
        {
            get { return _isCustomPhysicsSceneSet ? _customPhysicsScene : Physics.defaultPhysicsScene; }
            set
            {
                _customPhysicsScene = value;
                _isCustomPhysicsSceneSet = true;
            }
        }
        
        /// <summary>
        /// Is sensor detected something?
        /// Returns true when HitCount returns more than zero
        /// </summary>
        public bool HasHit => HitCount > 0;

        /// <summary>
        /// Count of detected objects
        /// </summary>
        public int HitCount => _hitCount;
        
        /// <summary>
        /// Array with colliders of detected objects
        /// This array is cached, and guaranteed to be at least HitCount elements long
        /// </summary>
        public virtual Collider[] HitColliders
        {
            get
            {
                EnsureArrayCapacity(ref _hitColliders);
                return _hitColliders;
            }
        }

        /// <summary>
        /// First collider that was hit, if any
        /// </summary>
        public Collider HitCollider => HitCount > 0 ? HitColliders[0] : null;

        protected int _hitCount;
        protected Collider[] _hitColliders = _emptyColliders;
        
        private bool _isCustomPhysicsSceneSet;
        private PhysicsScene _customPhysicsScene;

        public abstract bool Query(T data);

        public virtual void Clear()
        {
            _hitCount = 0;

            for (var i = _hitColliders.Length - 1; i >= 0; i--)
                _hitColliders[i] = null;
        }
        
        protected void EnsureArrayCapacity<A>(ref A[] array)
        {
            if (array == null || array.Length != maxResults)
                Array.Resize(ref array, maxResults);
        }

        public abstract void DrawLastQueryGizmo();
        public abstract void DrawQueryPreviewGizmo(T data);

        protected abstract void DrawColliderShape(Vector3 center, Quaternion rotation, Vector3 scale);

        private static readonly Collider[] _emptyColliders = new Collider[0];
    }
}