using UnityEngine;

public class scr_MainDialogue : MonoBehaviour
{
    public bool isDialogueTriggered;
    public float dialogueStateNum;

    public void OnTriggerEnter(Collider other)
    {
        // if the player triggers this and hasn't before, then stop all main dialogue playing and, play the new main dialogue
        if (other.CompareTag("Player") && isDialogueTriggered == false)
        {
            isDialogueTriggered = true;
            scr_AudioManager.instance.mainDialogue.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            scr_AudioManager.instance.mainDialogue.start();
            scr_AudioManager.instance.mainDialogue.setParameterByName("dialogueState", dialogueStateNum);
        }
    }
}
