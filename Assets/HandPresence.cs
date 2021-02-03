using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public List<GameObject> controllerPrefabs;

    public bool showController = false;
    private InputDevice targetDevice;
    private GameObject handModulePrefab;
    private GameObject spwanedHandModule;
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
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spwanedHandModule = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Did not find corrseponding controller model");
                //spwanedHandModule = Instantiate(handModulePrefab, transform);
                spwanedHandModule = Instantiate(controllerPrefabs[0], transform);

            }
        }


    }
    // Update is called once per frame
    void Update()
    {
        print("targetDevice:" + targetDevice.name);
        bool res = targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue1);

        Debug.Log("res" + res.ToString());

        spwanedHandModule.SetActive(true);



    }
}
