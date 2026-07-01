using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class scr_UIManager : MonoBehaviour
{   
    // UI panels and player game object
    public GameObject startMenu, pauseMenu, optionsMenu;
    public GameObject player, fadeToBlack;

    // bools for game state
    public bool isGamePlayed, isGamePaused, isRecentlyPressed, isGameBeingUnpaused;

    // fade to black value's
    public bool isStartingFade, isExitingFade;
    public float fadeToBlackValue;
    public Material fadeToBlackMat;

    // positions for different game state's
    public Vector3 playerLastPosition, gameStartLocation, menuStartLocation;

    // input for pausing the game
    public InputActionProperty pauseGameAction;

    // movement settings value's
    public bool contiousTurning;
    public TMP_Text turningButtonText;
    public GameObject turning;

    // intro cutscene animator
    public Animator introAnimator;

    // script instance
    public static scr_UIManager instance;

    public void Awake()
    {
        // sets instance of the script and sets game object to not destroy on load
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {   
        // sets game value's
        fadeToBlackMat = fadeToBlack.GetComponent<MeshRenderer>().material;
        introAnimator.enabled = false;
    }

    public void Update()
    {
        // on button press then if game isn't paused, pause game, otherwise unpause game and the button hasn't been pressed recently
         if (pauseGameAction.action.IsPressed() && isRecentlyPressed == false)
         {
            isRecentlyPressed = true;

           // pauses the game
           if (isGamePaused == false)
           {
                playerLastPosition = player.GetComponent<Transform>().position;
                isGamePaused = true;
                isStartingFade = true;
           }
           // unpauses the game
            else
            {
                Time.timeScale = 1f;
                UnPause();
                  isGamePaused = false;
                isGameBeingUnpaused = true;
           }
         }
    }

    public void UnpauseButtonPress()
    {
        if (isRecentlyPressed == false && isGamePaused == true)
        {
            isRecentlyPressed = true;
            Time.timeScale = 1f;
            UnPause();
            isGamePaused = false;
            isGameBeingUnpaused = true;
        }
    }

    public void Turning()
    {   
        // disables continous turning and enabled snap turning
        if (contiousTurning == true)
        {
            contiousTurning = false;
            turningButtonText.text = "Enabelled Snap Turn";
            turning.GetComponent<ContinuousTurnProvider>().enabled = false;
            turning.GetComponent<SnapTurnProvider>().enabled = true;
        }
        // enable continous turning and disables snap turning
        else
        {
            contiousTurning = true;
            turningButtonText.text = "Enabelled Continous Turn";
            turning.GetComponent<ContinuousTurnProvider>().enabled = true;
            turning.GetComponent<SnapTurnProvider>().enabled = false;
        }
        // plays UI sound
        scr_AudioManager.instance.UIClick.start();
    }

    public void GameStart()
    {
        // plaus UI click sound
        scr_AudioManager.instance.UIClick.start();

        // stars intro fade, and starts game sounds
        isStartingFade = true;
        scr_AudioManager.instance.music.setParameterByName("musicState", 0f);
        scr_AudioManager.instance.environmentSound.start();
    }

    public void FixedUpdate()
    {
        // fades to black over time
        if (isStartingFade == true)
        {
            fadeToBlackValue += Time.fixedDeltaTime;
             Debug.Log(fadeToBlackValue);
            fadeToBlackMat.color = new Color(0f, 0f, 0f, fadeToBlackValue);
            fadeToBlack.GetComponent<MeshRenderer>().material.SetColor("_Color", fadeToBlackMat.color);

            // after a period of time, fade to black ends
            if (fadeToBlackValue >= 1.5f)
            {
                isStartingFade = false;
                isExitingFade = true;

                // if the fade isn't because of pausing or unpausing then play the intro cutscene
                if (isGamePaused == false && isGameBeingUnpaused == false)
                {
                  //    player.GetComponent<Transform>().position = gameStartLocation;
                  //    scr_AudioManager.instance.mainDialogue.start();
                  //    scr_AudioManager.instance.mainDialogue.setParameterByName("dialogueState", 0f);
                    scr_IntroCutscene.instance.GameStart();
                }
                
                // if fade to black is because of an unpause, then set the player to their last position in the level
                else if (isGamePaused == false && isGameBeingUnpaused == true)
                {
                    player.GetComponent<Transform>().position = playerLastPosition;
                    isGameBeingUnpaused = false;
                    startMenu.SetActive(true);
                }
                
                // if fade to black is because of a pause, then set the player to the menu UI location
                else
                {
                    player.GetComponent<Transform>().position = menuStartLocation;
                }

                // fade to black, set to the maximum darkness
                fadeToBlackMat.color = new Color(0f, 0f, 0f, 1f);
                fadeToBlack.GetComponent<MeshRenderer>().material.SetColor("_Color", fadeToBlackMat.color);
            }
        }
        // fades back to normal over time
        else if (isExitingFade == true)
        {
            fadeToBlackValue -= Time.fixedDeltaTime;
            fadeToBlackMat.color = new Color(0f, 0f, 0f, fadeToBlackValue);
            fadeToBlack.GetComponent<MeshRenderer>().material.SetColor("_Color", fadeToBlackMat.color);

            // if fade out of black is done, then end the fading
            if (fadeToBlackValue <= 0f)
            {
                isExitingFade = false;
                fadeToBlackMat.color = new Color(0f, 0f, 0f, 0f);
                fadeToBlack.GetComponent<MeshRenderer>().material.SetColor("_Color", fadeToBlackMat.color);

                // if the game is paused, trigger the pause
                if (isGamePaused == true)
                {
                    Pause();
                }
                isRecentlyPressed = false;
            }
        }
    }

    public void QuitGame()
    {
        // quits the game
        scr_AudioManager.instance.UIClick.start();
        Application.Quit();
    }

    public void Pause()
    {
       // pauses the game by setting the pause value's
        pauseMenu.SetActive(true);
        startMenu.SetActive(false);
        optionsMenu.SetActive(false);
        player.GetComponent<Transform>().position = menuStartLocation;
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        // sets unpause value's
        isStartingFade = true;
        isGameBeingUnpaused = true;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void OptionsMenu()
    {
        // plaus UI click sound and opens the option menu
        scr_AudioManager.instance.UIClick.start();
        startMenu.SetActive(false);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ExitOption()
    {
        // opens previous menu the player was on, closes options menu and plays UI click sound
        if (isGamePaused == false)
        {
            startMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(true);
        }

        optionsMenu.SetActive(false);
        scr_AudioManager.instance.UIClick.start();
    }

}
