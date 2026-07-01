using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class scr_RepairableObject : MonoBehaviour
{
    public static scr_RepairableObject instance;
    public float numberOfPartsRepaired, totalNumberOfParts;
    public bool isDoorGoingUp;
    public Material repairedPartFixed, repairedPartBroken;
    public GameObject door, enginge;
    public Vector3 doorStopMovementPositionUpWards, doorStopMovementPositionDownWards;

    public void Awake()
    {
        // makes reference to this script
        instance = this;
    }

    public void Start()
    {
        // starts engine sound, at the repiar object position
        scr_AudioManager.instance.enginge.start();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(scr_AudioManager.instance.enginge, enginge);
    }
    public void FixedUpdate()
    {
        // if the door is going up and is at the maximum height, then disables door movement and stop the door moving sound
        if (door.GetComponent<Transform>().position.y >= doorStopMovementPositionUpWards.y && isDoorGoingUp == true && door.GetComponent<Rigidbody>().isKinematic == false)
        {
            door.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0f, 0f);
            door.GetComponent<Rigidbody>().isKinematic = true;
            scr_AudioManager.instance.doorMovement.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        
        // if the door is going down and is at the minimum height, then disables door movement and stop the door moving sound
        else if (door.GetComponent<Transform>().position.y <= doorStopMovementPositionDownWards.y && isDoorGoingUp == false && door.GetComponent<Rigidbody>().isKinematic == false)
        {
            scr_AudioManager.instance.doorMovement.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            door.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0f, 0f);
            door.GetComponent<Rigidbody>().isKinematic = true;
            door.GetComponent<Transform>().position = new Vector3 (door.GetComponent<Transform>().position.x, 0.88f, door.GetComponent<Transform>().position.z);
        }
        
    }

    public void PartRepaired()
    {
        // increase amount of repaired parts
        numberOfPartsRepaired++;

        // if all the parts are repaired, then the door lifts up and plays the door opening sounmd
        if (numberOfPartsRepaired >= totalNumberOfParts)
        {
            door.GetComponent<Rigidbody>().isKinematic = false;
            door.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 2f, 0f);
            isDoorGoingUp = true;
            scr_AudioManager.instance.enginge.setParameterByName("EngineState", 1f);
            scr_AudioManager.instance.doorMovement.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            scr_AudioManager.instance.doorMovement.start();
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(scr_AudioManager.instance.doorMovement, door);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(scr_AudioManager.instance.enginge, enginge);

            scr_AudioManager.instance.mainDialogue.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            scr_AudioManager.instance.mainDialogue.start();
            scr_AudioManager.instance.mainDialogue.setParameterByName("dialogueState", 3f);
        }

        // if not all the parts are repaired and the door is lifting, then the door moves down and plays the door closing sound
        else if (numberOfPartsRepaired < totalNumberOfParts && isDoorGoingUp == true)
        {
            door.GetComponent<Rigidbody>().isKinematic = false;
            door.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, -2f, 0f);
            isDoorGoingUp = false;
            scr_AudioManager.instance.enginge.setParameterByName("EngineState", 0f);
            scr_AudioManager.instance.doorMovement.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            scr_AudioManager.instance.doorMovement.start();
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(scr_AudioManager.instance.doorMovement, door);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(scr_AudioManager.instance.enginge, enginge);
        }
    }

    public void PartBroken()
    {
        // decreases amount of repaired parts
        numberOfPartsRepaired--;

                // if not all the parts are repaired and the door is opening, then the door lowers  and plays the door closing sound
        if (numberOfPartsRepaired < totalNumberOfParts && isDoorGoingUp == true)
        {
            door.GetComponent<Rigidbody>().isKinematic = false;
            door.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, -2f, 0f);
            isDoorGoingUp = false;
            scr_AudioManager.instance.doorMovement.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            scr_AudioManager.instance.doorMovement.start();
            scr_AudioManager.instance.enginge.setParameterByName("EngineState", 0f);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(scr_AudioManager.instance.doorMovement, door);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(scr_AudioManager.instance.enginge, enginge);
        }
    }
}
