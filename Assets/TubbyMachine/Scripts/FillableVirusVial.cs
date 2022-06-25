using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillableVirusVial : MonoBehaviour
{
    public VirusVial vial;
    public Collider trigger;

    public float amountPerBall = 0.1f;
    public float maxAmount = 1f;

    public bool IsFilled => vial.progress >= maxAmount;

    private void OnTriggerEnter(Collider other)
    {
        var ball = other.GetComponent<CustardBall>();

        if (ball)
        {
            vial.progress += amountPerBall;

            if (vial.progress >= maxAmount)
            {
                vial.progress = maxAmount;
                trigger.enabled = false;
            }
            
            Destroy(ball.gameObject);
        }
    }
}
