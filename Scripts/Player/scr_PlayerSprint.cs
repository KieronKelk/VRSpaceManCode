using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class scr_PlayerSprint : MonoBehaviour
{
    public bool isSprintEnabled;
    public GameObject moveObject;
    public InputActionProperty teleportActivatorAction;

    public void Start()
    {
        teleportActivatorAction.action.performed += Action_performed;
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
    }

    public void Update()
    {
        // Get all input devices that are controllers

        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, devices);

        // for all controlers, check if the Axis is clicked, if it is then sprint is enabled
        foreach (var device in devices)
        {
            bool primary2DAxisClicked;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out primary2DAxisClicked) && primary2DAxisClicked)
            {
                isSprintEnabled = true;
            }
        }

        // sets player movement speed to sprint when using continous
        if (isSprintEnabled == true)
        {
            moveObject.GetComponent<ContinuousMoveProvider>().moveSpeed = 2f;
        }
        
        // sets player movement speed to walk when using continous
        else
        {
            moveObject.GetComponent<ContinuousMoveProvider>().moveSpeed = 1f;
        }

        // disables sprint, so the sprint has to be the button is held down
        isSprintEnabled = false;
    }
}
