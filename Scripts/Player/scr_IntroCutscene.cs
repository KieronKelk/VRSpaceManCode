using Unity.VisualScripting;
using UnityEngine;

public class scr_IntroCutscene : MonoBehaviour
{
    public static scr_IntroCutscene instance;
    public FMOD.Studio.EventInstance cutsceneDialogue;
    public Animator intoCutsceneAnimatior;

    public void Awake()
    {
        // creates instance of script and FMOD event
        instance = this;
        cutsceneDialogue = FMODUnity.RuntimeManager.CreateInstance("event:/IntroCutsceneDialogue");
    }

    public void Start()
    {
       // CutsceneStart();
    }

    public void GameStart()
    {
        // trigger delay for animation to start
        Invoke("CutsceneStart", 1.5f);
    }

    public void CutsceneStart()
    {
        intoCutsceneAnimatior.enabled = true;
        // start trigger to play animation
        intoCutsceneAnimatior.SetTrigger("intoCutscene");
        // play audio for animation
        cutsceneDialogue.start();

        // trigger delay
        Invoke("CutsceneEnd", 15f);
    }

    public void CutsceneEnd()
    {
        // stops audio for animation
        cutsceneDialogue.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        // stops cutscene
        intoCutsceneAnimatior.SetTrigger("cutsceneDone");

        // starts main dialogue conversation
        scr_AudioManager.instance.mainDialogue.start();
        scr_AudioManager.instance.mainDialogue.setParameterByName("dialogueState", 0f);

        // reset animator to being disabled
        intoCutsceneAnimatior.enabled = false;

        intoCutsceneAnimatior.ResetTrigger("intoCutscene");
        intoCutsceneAnimatior.ResetTrigger("cutsceneDone");
    }
}
