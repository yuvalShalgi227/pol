using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveAround : LocomotionProvider
{

    // How much do we turn?
    public float turnSegment = 45.0f;

    // How long does it take to turn?
    public float turnTime = 3.0f;

    // Basic input - I'd recommend replacing with an input solution.
    //InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
    //device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    public InputHelpers.Button rightTurnButton = InputHelpers.Button.PrimaryAxis2DRight;
    public InputHelpers.Button leftTurnButton = InputHelpers.Button.PrimaryAxis2DLeft;
    
    // List of the controllers we're going to use
    public List<XRController> contollers = new List<XRController>();

    // The amount we're turning to
    public float targetTurnAmount = 0.0f;
    private void Update()
    {
        // Let's ask the locomotion system if we can move
        if (CanBeginLocomotion())
        {
            CheckForInput();
        }
    }

    private void CheckForInput()
    {
        foreach (XRController controller in contollers)
        {
            targetTurnAmount = CheckForTurn(controller);
            if (targetTurnAmount!= 0.0f)
            {
                TrySmoothTurn();
            }
        }
    }

    private void CheckForTurn()
    {
        // Go through the controllers

        // Check for input, return the segment with positive or negative 

        // If we actually got a value from input, try to turn
    }

    // Check for input, this can be done cleaner with a more robust input solution
    private float CheckForTurn(XRController controller)
    {
        // Check if we pressed right
        if (controller.inputDevice.IsPressed(rightTurnButton, out bool rightPress))
        {
            if (rightPress)
            {
                return turnSegment;
            }
        }
        // Check if we pressed left
        if (controller.inputDevice.IsPressed(leftTurnButton, out bool leftPress))
        {
            if (leftPress)
            {
                return -turnSegment;
            }
        }
        // If we hit nothing return 0
        return 0.0f;
    }

    private void TrySmoothTurn()
    {
        // Let's stry turning with the amount we got
        StartCoroutine(TurnRoutine(targetTurnAmount));
        // Since the value has been used, let's clear it out
        targetTurnAmount = 0.0f;
    }

    private IEnumerator TurnRoutine(float turnAmount)
    {
        // We need to store this since we only want to pass the difference
        float pervTurnChange = 0.0f;
        // Record the whole time of the loop for proper lerp
        float elapseTime = 0.0f;

        // Let the motion begin
        BeginLocomotion();

        while (elapseTime <= turnTime)
        {
            // How far are we into the lerp?
            float blend = elapseTime / turnTime;
            float turnChange = Mathf.Lerp(0, turnAmount, blend);
            // Figure out the difference and apply it
            float turnDiff = turnChange - pervTurnChange;
            // Save the current amount we've moved, and add to elapsed time
            system.xrRig.RotateAroundCameraUsingRigUp(turnDiff);
            pervTurnChange = turnChange;
            elapseTime += Time.deltaTime;
            // Yield or we're crashing
            yield return null;
        }
        // Let the motion end
        EndLocomotion();
    }
}