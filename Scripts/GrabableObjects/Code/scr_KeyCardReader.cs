using UnityEngine;
using UnityEngine.UIElements;

public class scr_KeyCardReader : MonoBehaviour
{
    public bool isKeyCardScanned;
    public GameObject door, keyCardScanner;
    public Vector3 doorStopMovementPosition;

    public Material keyCardScannerMaterial;

    public void Start()
    {
        // sets keycard material to the default color of red
        keyCardScannerMaterial.color = new Color32(231,40,23,255);
    }

    public void OnTriggerEnter(Collider other)
    {
        keyCardScannerMaterial.color = new Color32(231,141,23,255); // yellow, planned delay if time

        // if the keycard is used to scan the door, the keycard scanner turns green and the door opens, playing the keycard scan and door open sfx
        if (other.CompareTag("KeyCard"))
        {
            keyCardScannerMaterial.color = new Color32(40,231,23,255); // green
            isKeyCardScanned = true;
            door.GetComponent<Rigidbody>().isKinematic = false;
            door.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 2f, 0f);
            scr_AudioManager.instance.keyCardScanner.start();
            scr_AudioManager.instance.keyCardScanner.setParameterByName("KeycardScannerState", 1f);
            scr_AudioManager.instance.doorMovement.start();
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(scr_AudioManager.instance.doorMovement, door);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(scr_AudioManager.instance.keyCardScanner, keyCardScanner);
        }

        // if something other than the keycard is scanned, the scanner turns red and plays a rejection sound
        else
        {
            keyCardScannerMaterial.color = new Color32(231,40,23,255); // red
            scr_AudioManager.instance.keyCardScanner.start();
            scr_AudioManager.instance.keyCardScanner.setParameterByName("KeycardScannerState", 0f);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(scr_AudioManager.instance.keyCardScanner, keyCardScanner);
        }
    }

    public void DoorScan()
    {
        
    }

    public void Update()
    {
       // sets door maximum movement when opening, once it's reached the door's position is set and can't be moved higher and the door movement sound is stopped
       if (door.GetComponent<Transform>().position.y >= doorStopMovementPosition.y && isKeyCardScanned == true && door.GetComponent<Rigidbody>().isKinematic == false)
        {
            Debug.Log("DoorStopMovement");
            door.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0f, 0f);
            door.GetComponent<Rigidbody>().isKinematic = true;
            door.GetComponent<Transform>().position = doorStopMovementPosition;
            scr_AudioManager.instance.doorMovement.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
}
