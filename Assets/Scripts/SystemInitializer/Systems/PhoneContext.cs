using System.Collections;
using Fungus;
using SystemInitializer.Systems.Movement;
using UnityEngine;

namespace SystemInitializer.Systems
{
    public class PhoneContext : MonoBehaviourContext
    {
        public bool isCalling;
        public AudioClip ringSound;
        public AudioClip GivePhoneSound;
        public AudioSource AudioSource;
        public Flowchart Flowchart;

        public float waitForSeconds;

        private Coroutine callCoroutine;
        
        public string blockName = "Start";

        public void GivePhone()
        {
            if (!isCalling)
                return;
            
            AudioSource.Stop();
            AudioSource.PlayOneShot(GivePhoneSound);
            ContextsContainer.GetContext<MovementContext>().CurrentMovementPoint.Disable();
            ContextsContainer.GetContext<PhoneContext>().Flowchart.ExecuteBlock(blockName);
            isCalling = false;
        }
        
        public void DoCall()
        {
            if (callCoroutine != null)
                return;

            callCoroutine = StartCoroutine(CallCoroutine());
        }

        IEnumerator CallCoroutine()
        {
            yield return new WaitForSeconds(waitForSeconds);
            isCalling = true;
            AudioSource.clip = ringSound;
            AudioSource.loop = true;
            AudioSource.Play();
        }
    }
}