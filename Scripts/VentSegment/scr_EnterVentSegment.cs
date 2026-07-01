using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Comfort;


public class scr_EnterVentSegment : MonoBehaviour
{
    public Vector3 ventStartLocation;
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
           fadeToBlackMat.color = new Color(0f,0f,0f, fadeToBlackValue);
           fadeToBlack.GetComponent<MeshRenderer>().material.color = fadeToBlackMat.color;
            if (fadeToBlackValue >= 1.5f)
            {
                isEnteringVent = false;
                isExitingVent = true;
                player.GetComponent<Transform>().position = ventStartLocation;
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

                scr_AudioManager.instance.music.setParameterByName("musicState", 1f);
                scr_AudioManager.instance.environmentSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
        }
    }
}
