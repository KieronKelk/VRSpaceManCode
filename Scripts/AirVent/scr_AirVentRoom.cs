using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_AirVentRoom : MonoBehaviour
{
    public static scr_AirVentRoom instance;
    public bool isInAirVent, isVented, isDoneDialogue;
    public float basetimeLeftTillVent, timeLeftTillVent;
    public TMP_Text timeLeftText;

    public float basetimeLeftInDark, timeLeftInDark;
    public Material fadeToBlackMat;
    public GameObject player, fadeToBlack;

    public void Awake()
    {
        //set script as an instance and finds needed objects
        instance = this;
        player = GameObject.Find("VR Player");
        fadeToBlack = GameObject.Find("TunnelingNonAnimation");
    }

    public void Start()
    {
        // resets the timer value's and the fade to black material
        timeLeftInDark = basetimeLeftInDark;
        timeLeftTillVent = basetimeLeftTillVent;
        timeLeftText.text = timeLeftTillVent.ToString();
        fadeToBlackMat = fadeToBlack.GetComponent<MeshRenderer>().material;
    }

    public void Update()
    {   
        // when the player is in the air vent room, then count down a timer
        if (isInAirVent == true && isVented == false)
        {
            timeLeftTillVent -= Time.deltaTime;
            float timeDisplay = Mathf.Round(timeLeftTillVent);
            timeLeftText.text = timeDisplay.ToString();

            // once the timer is finished, set the value's for the end game scene, reset timer and plays game over music
            if (timeLeftTillVent <= 0f)
            {
                isInAirVent = false;
                isVented = true;

                timeLeftTillVent = basetimeLeftTillVent;
                scr_AudioManager.instance.music.setParameterByName("musicState", 2f);
            }

            // when timer is 10s or less then play the final dialogue lines 
            else if (timeLeftTillVent <= 10f && isDoneDialogue == false)
            {
                isDoneDialogue = true;
                scr_AudioManager.instance.mainDialogue.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                scr_AudioManager.instance.mainDialogue.start();
                scr_AudioManager.instance.mainDialogue.setParameterByName("dialogueState", 7f);
            }
        }
        // once the player is vented, the screen cuts to black and all environmental sounds end for a set time
        else if (isVented == true)
        {
            fadeToBlack.SetActive(true);
            fadeToBlackMat.color = new Color(0f,0f,0f, 1f);
            scr_AudioManager.instance.mainDialogue.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            scr_AudioManager.instance.enginge.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            timeLeftInDark -= Time.deltaTime;

            // when the player is finished done in the dark then resets to the main menu
            if (timeLeftInDark <= 0f)
            {
                player.GetComponent<Transform>().position = scr_UIManager.instance.menuStartLocation;
                fadeToBlackMat.color = new Color(0f, 0f, 0f, 0f);
                isVented = false;
                timeLeftInDark = basetimeLeftInDark;
                scr_AudioManager.instance.music.setParameterByName("musicState", 0f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
