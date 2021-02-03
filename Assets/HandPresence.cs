﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public List<GameObject> ControllerPrefabs;

    private InputDevice targetDevice;
    private GameObject handModulePrefab;
    private GameObject SpwanhandModule;
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharactersitics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharactersitics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }
    // Update is called once per frame
    void Update()
    {
        print("targetDevice:" + targetDevice.name);
        bool res = targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue1);

        Debug.Log("res" + res.ToString());

        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out res) && res )
        {
            Debug.Log("Pressing Primary Button");
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f) 
        {
            Debug.Log("Trigger Pressed" + triggerValue);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        {
            Debug.Log("Primary Touchpad " + primary2DAxisValue);
        }
    }
}
