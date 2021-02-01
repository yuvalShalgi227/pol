using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class HandScript : MonoBehaviour
{
    // Start is called before the first frame update

    private InputDevice targetDevice;

    void Start()
    {
        
        List<InputDevice> devices = new List<InputDevice>();
        try
        {

            Debug.Log("start");
            
            InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
            InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
            foreach (var item in devices)
            {
                Debug.Log(item.name + item.characteristics);
            }

            if(devices.Count > 0)
            {
                targetDevice = devices[0];
            }
        }
        catch
        {
            Debug.Log("catched");
        }
    }

    // Update is called once per frame
    void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);

        if (primaryButtonValue)
            Debug.Log("pressing primary button"); 
    }
}
