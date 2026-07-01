using UnityEngine;
using UnityEngine.UI;

public class scr_ExitVentSegment : MonoBehaviour
{
    public Vector3 ventEndLocation;
    public Quaternion ventRotationLocation;
    public GameObject player, fadeToBlack;
    public bool isEnteringVent, isExitingVent;
    public float fadeToBlackValue;
    public Material fadeToBlackMat;

    public void OnTriggerEnter(Collider other)
    {
        // if the player enters the trigger box then the player is entering the vent
        if (other.CompareTag("Player"))
        {
            fadeToBlack.SetActive(true);
            isEnteringVent = true;
        }
    }

    public void Awake()
    {
        // sets game object values
        player = GameObject.Find("VR Player");
        fadeToBlack = GameObject.Find("TunnelingNonAnimation");
    }
    public void Start()
    {
        // sets fade to black material
        fadeToBlackMat = fadeToBlack.GetComponent<MeshRenderer>().material;
    }

    public void Update()
    {
        // if the player is entering the vent, then fade to black and then fade to normal;
        if (isEnteringVent == true)
        {
            fadeToBlackValue += Time.fixedDeltaTime;
            fadeToBlackMat.color = new Color(0f, 0f, 0f, fadeToBlackValue);

            if (fadeToBlackValue >= 1.5f)
            {
                isEnteringVent = false;
                isExitingVent = true;
              //  player.GetComponent<Rigidbody>().linearVelocity = new Vector3(0f, 0f, 0f);
                player.GetComponent<Transform>().position = ventEndLocation;
                player.GetComponent<Transform>().rotation = ventRotationLocation;
                fadeToBlackMat.color = new Color(0f, 0f, 0f, fadeToBlackValue);
            }
        }
        else if (isExitingVent == true)
        {
            fadeToBlackValue -= Time.fixedDeltaTime;
            fadeToBlackMat.color = new Color(0f, 0f, 0f, fadeToBlackValue);

            if (fadeToBlackValue <= 0f)
            {
                isExitingVent = false;
                fadeToBlackMat.color = new Color(0f, 0f, 0f, fadeToBlackValue);
                scr_AirVentRoom.instance.isInAirVent = true;
                scr_AudioManager.instance.mainDialogue.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                scr_AudioManager.instance.mainDialogue.start();
                scr_AudioManager.instance.mainDialogue.setParameterByName("dialogueState", 6f);
            }
        }
    }
}
