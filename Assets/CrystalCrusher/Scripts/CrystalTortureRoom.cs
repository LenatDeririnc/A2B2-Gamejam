using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace CrystalCrusher.Scripts
{
    public class CrystalTortureRoom : MonoBehaviour
    {
        public HalfLifeVox vox;
        public DrCoomer coomer;
        public CrystalPress press;
        public CrystalCannon cannon;
        public bool playWelcome;
        public Animator controlsAnimator;

        public AudioClip countdownThree;
        public AudioClip countdownTwo;
        public AudioClip countdownOne;
        public AudioClip countdownGo;

        public AudioClip[] winPhraseGood;
        public AudioClip[] winPhraseMedium;
        public AudioClip[] winPhrasePoor;
        public AudioClip[] tauntPhrase;
        public AudioClip[] welcomeSequence;

        private bool _isCrystalThrowInProcess;
        private int _misses;
        private bool _disableUntilVoxFinishes;

        public Action onGameCompleted;

        private void Awake()
        {
            if(playWelcome) 
                BeginWelcomeSequence();
        }


        public void BeginWelcomeSequence()
        {
            _disableUntilVoxFinishes = true;
            vox.Say(welcomeSequence);
        }

        private void Update()
        {
            if (!vox.IsSpeaking)
                _disableUntilVoxFinishes = false;

            controlsAnimator.SetBool("IsEnabled", !_disableUntilVoxFinishes);
        }

        public void BeginThrowCrystalSequence()
        {
            if(vox.IsSpeaking)
                return;
            
            if(_isCrystalThrowInProcess)
                return;
            
            _isCrystalThrowInProcess = true;
            
            StartCoroutine(StartCountdownCoroutine(() =>
            {
                cannon.Throw(result =>
                {
                    if (result)
                    {
                        OnHit();
                    }
                    else
                    {
                        OnMiss();
                    }
                });
            }));
        }

        public void BeginHitSequence()
        {
            press.StartPress();
        }

        private void OnHit()
        {
            if (_misses == 0)
            {
                vox.Say(winPhraseGood);
            }
            else if (_misses >= 3)
            {
                vox.Say(winPhrasePoor);
            }
            else
            {
                vox.Say(winPhraseMedium);
            }

            StartCoroutine(CallActionAfterDelay(onGameCompleted, 4f));
        }

        private IEnumerator CallActionAfterDelay(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        private void OnMiss()
        {
            _misses++;
            _isCrystalThrowInProcess = false;
            
            coomer.OnMiss();

            if (_misses == 3)
            {
                vox.Say(tauntPhrase);
                _disableUntilVoxFinishes = true;
            }
        }

        private IEnumerator StartCountdownCoroutine(Action onCountdownFinished)
        {
            vox.Say(countdownThree);
            yield return new WaitForSeconds(1f);
            vox.Say(countdownTwo);
            yield return new WaitForSeconds(1f);
            vox.Say(countdownOne);
            yield return new WaitForSeconds(1f);
            vox.Say(countdownGo);
            onCountdownFinished?.Invoke();
        }
    }
}