﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSetter : MonoBehaviour
{
    public GameObject pinSet;
    
    private float distanceToRaise = 40.0f;
    private float pinLaneDistance = 1829.0f;

    private Animator animator;
    private Ball ball;
    private PinCounter pinCounter;
    private GameEnd gameEnd;

    // Use this for initialization
    void Start()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
        gameEnd = GameObject.FindObjectOfType<GameEnd>();
    }

    // Sets pin collision to continuous dynamic when ball enters pin setter box collider
    void OnTriggerEnter(Collider collider)
    {
        GameObject ball = collider.gameObject;

        if (ball.GetComponent<Ball>())
        {
            foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
            {
                pin.SetCollisionContinousDynamic();
            }
        }
    }

    // Handles pin interface
    #region PinInterface
    // Raise pins
    public void RaisePins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.RaiseIfStanding(distanceToRaise);
        }
    }

    // Lower pins
    public void LowerPins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.Lower(distanceToRaise);
        }
    }

    // Create new pinset
    public void RenewPins()
    {
        Instantiate(pinSet, new Vector3(0, distanceToRaise, pinLaneDistance), Quaternion.identity);
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.RenewNewPin();
        }
    }
    #endregion

    // Handles animation triggers
    #region AnimationTriggers
    // Animation event prevents ball launch before pins have set
    public void CanLaunch() { ball.SetCanLaunch(); }

    // Process animation triggers
    public void ProcessAction(ActionMaster.Action action)
    {
        if (action == ActionMaster.Action.Tidy)
        {
            animator.SetTrigger("tidyTrigger");
        }
        else if (action == ActionMaster.Action.EndTurn)
        {
            animator.SetTrigger("resetTrigger");
            pinCounter.Reset();
        }
        else if (action == ActionMaster.Action.Reset)
        {
            animator.SetTrigger("resetTrigger");
            pinCounter.Reset();
        }
        else if (action == ActionMaster.Action.EndGame)
        {
            Debug.Log("Game is Finished!");
            gameEnd.EndGame();
        }
    }
    #endregion
}
