using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class scr_TeleportationActivator : MonoBehaviour
{
    public XRRayInteractor teleportInteractor;
    public InputActionProperty teleportActivatorAction;

    public void Start()
    {
        // disables the teleport interactor and trigger teleportation action
        teleportInteractor.gameObject.SetActive(false);

        teleportActivatorAction.action.performed += Action_performed;
    }

    // enables teleport interactor when action is performed
    private void Action_performed(InputAction.CallbackContext obj)
    {
        teleportInteractor.gameObject.SetActive(true);
    }

    public void Update()
    {
        // if action no longer performed, then disable teleporter
        if (teleportActivatorAction.action.WasReleasedThisFrame())
        {
            teleportInteractor.gameObject.SetActive(false);
        }
    }
}
