using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public bool showController = false;
    public GameObject handModelPrefab; 
    
    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    private bool instantiate = false;
    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }
    // Update is called once per frame
    void UpdateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);


    
        //Debug.Log("before devices count");
        if (devices.Count > 0)
        {
            instantiate = true;
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Did not find corrseponding controller model");
                //spwanedHandModule = Instantiate(handModulePrefab, transform);
                spawnedController = Instantiate(controllerPrefabs[0], transform);

            }

            Debug.Log("now instantiate handmodelPrefab");
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();

            Debug.Log("instantiated hand modelPrefab" + spawnedHandModel.ToString());

        }
        else
        {
     //       Debug.LogError("no devices");
        }
    }
    void Update()
    {
        if(!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (instantiate)
            {
                spawnedHandModel.SetActive(true);
                if (instantiate)
                {
                    if (showController)
                    {
                        spawnedHandModel.SetActive(false);
                        spawnedController.SetActive(true);
                    }
                    else
                    {
                        spawnedHandModel.SetActive(true);
                        spawnedController.SetActive(false);
                        UpdateHandAnimation();
                    }
                }
            }

        }


        //print("targetDevice:" + targetDevice.name);
        //bool res = targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue1);

        //Debug.Log("res" + res.ToString());

        //spwanedHandModule.SetActive(true);



    }
}
