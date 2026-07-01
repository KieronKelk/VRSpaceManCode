using UnityEngine;

public class scr_AudioManager : MonoBehaviour
{
    // variable declaring this script is refered to when called
    public static scr_AudioManager instance;
    // variables for FMOD audio events
    public FMOD.Studio.EventInstance environmentSound;
    public FMOD.Studio.EventInstance UIClick;
    public FMOD.Studio.EventInstance music;
    public FMOD.Studio.EventInstance keyCardScanner;
    public FMOD.Studio.EventInstance doorMovement;
    public FMOD.Studio.EventInstance playerGrab;
    public FMOD.Studio.EventInstance playerThrow;
    public FMOD.Studio.EventInstance playerWalk;
    public bool isPlayerWalking;
    public Transform playerTransform;
    public Vector3 playerLastCheckedPosition;
    public FMOD.Studio.EventInstance engineRepair;
    public FMOD.Studio.EventInstance enginge;
    public FMOD.Studio.EventInstance mainDialogue;

    // event Description for FMOD events that need 3D 
    
    public FMOD.Studio.EventDescription doorEventDescription;
    public FMOD.Studio.EventDescription engineEventDescription;
    public FMOD.Studio.EventDescription keyCardScannerEventDescription;



    public void Awake()
    {
        // setting that when talking about scr_AudioManager.instance it is refering to this specific script in the game world
        instance = this;

        // sets all events and parameters to corresponding FMOD settings
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music"); // done
        enginge = FMODUnity.RuntimeManager.CreateInstance("event:/Engine"); // done
        engineRepair = FMODUnity.RuntimeManager.CreateInstance("event:/EngineRepair"); // DONE
        playerWalk = FMODUnity.RuntimeManager.CreateInstance("event:/PlayerWalk"); // done
        playerThrow = FMODUnity.RuntimeManager.CreateInstance("event:/PlayerThrow"); // done
        playerGrab = FMODUnity.RuntimeManager.CreateInstance("event:/PlayerGrab"); // done
        doorMovement = FMODUnity.RuntimeManager.CreateInstance("event:/Door"); // ISSUE
        keyCardScanner = FMODUnity.RuntimeManager.CreateInstance("event:/KeyCardScanner"); // DONE
        UIClick = FMODUnity.RuntimeManager.CreateInstance("event:/UIClick"); // done
        environmentSound = FMODUnity.RuntimeManager.CreateInstance("event:/Environment"); // done
        mainDialogue = FMODUnity.RuntimeManager.CreateInstance("event:/MainStoryDialogue");

        // starts playing the game's music and sets the parameter to the base value of 0
        music.start();
        music.setParameterByName("musicState", 0f);
    }

    public void Update()
    {
        // gets player current position
        Vector3 playerPosRightNow = playerTransform.position;

        // if players current position isn't equal to their last checked position
        if (playerTransform.position != playerLastCheckedPosition)
        {
            // if player isn't walking, set player to walking and play walking sound
            if (isPlayerWalking == false)
            {
                isPlayerWalking = true;
                playerWalk.start();
            }

            // set players last checked position, to the players position at the start of the current check
            playerLastCheckedPosition = playerPosRightNow;
        }

        // if the players current position is equal to their last checked position
        else
        {
            // stop the player walking and the walking sound immediately
            playerWalk.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            isPlayerWalking = false;
        }
    }

    public void PlayerDied()
    {
        // sets music value
        music.setParameterByName("musicState", 2f);
    }

    public void GrabObject()
    {
        // plays grabbed sound when an object is grabbed
        playerGrab.start();
    }

    public void ThrownObject()
    {
        // plays thrown sound when an object is thrown
        playerThrow.start();
    }

}
