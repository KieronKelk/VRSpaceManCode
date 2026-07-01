using UnityEngine;
using UnityEngine.InputSystem;

public class scr_AnimateOnHandInput : MonoBehaviour
{
    public InputActionProperty triggerValue;
    public InputActionProperty gripValue;

    public Animator handAnimator;

    public void Update()
    {
        // sets the hand animator values based on button inputs on controllers
        float trigger = triggerValue.action.ReadValue<float>();
        float grip = gripValue.action.ReadValue<float>();

        handAnimator.SetFloat("Trigger", trigger);
        handAnimator.SetFloat("Grip", grip);
    }
}
