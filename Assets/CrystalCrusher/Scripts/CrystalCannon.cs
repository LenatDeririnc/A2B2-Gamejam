using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CrystalCrusher.Scripts
{
    public class CrystalCannon : MonoBehaviour
    {
        public Animator animator;
        public AudioSource throwSource;
        public CrunchyZenCrystal crystal;
        public Transform crystalThrowPosition;
        public float crystalThrowSpeed;
        public float crystalRotationSpeed;

        public void Throw(Action<bool> result)
        {
            var newCrystal = Instantiate(crystal, crystalThrowPosition.position, Quaternion.identity);
            newCrystal.onHitOrMiss = result;
            
            newCrystal.rigidbody.AddForce(crystalThrowPosition.forward * crystalThrowSpeed);
            newCrystal.rigidbody.AddTorque(Random.insideUnitSphere * crystalRotationSpeed);
            animator.SetTrigger("Throw");
            throwSource.Play();
        }
    }
}