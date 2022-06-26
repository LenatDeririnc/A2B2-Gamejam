using System.Collections;
using Cinemachine;
using CrystalCrusher.Scripts;
using SystemInitializer;
using SystemInitializer.Systems.Cinemachine;
using UnityEngine;

public class MG2Context : MonoBehaviourContext
{
    public CinemachineVirtualCamera Camera;
    public CrystalTortureMiniGame MiniGameAsset;
    public MiniGame MiniGame;

    public float WaitForSeconds;
    
    private Coroutine enterCoroutine;
    private Coroutine exitCoroutine;
    
    public void EnterMG()
    {
        if (enterCoroutine != null)
            return;
        
        enterCoroutine = StartCoroutine(EnterSequence());
    }

    public void ExitMG()
    {
        if (exitCoroutine != null)
            return;
        
        exitCoroutine = StartCoroutine(ExitSequence());
    }

    private IEnumerator EnterSequence()
    {
        ContextsContainer.GetContext<FadeContext>().Show();
        yield return new WaitForSeconds(WaitForSeconds);
        ContextsContainer.GetContext<MG2Context>().Camera.Priority = 1000;
        ContextsContainer.GetContext<FadeContext>().Hide();
        yield return new WaitForSeconds(WaitForSeconds);
        ContextsContainer.GetContext<MG2Context>().MiniGameAsset.StartGame(ExitMG);
    }

    private IEnumerator ExitSequence()
    {
        ContextsContainer.GetContext<FadeContext>().Show();
        yield return new WaitForSeconds(WaitForSeconds);
        ContextsContainer.GetContext<MG2Context>().Camera.Priority = 0;
        ContextsContainer.GetContext<FadeContext>().Hide();
        yield return new WaitForSeconds(WaitForSeconds);
        MiniGame.DoTask();
    }
}
