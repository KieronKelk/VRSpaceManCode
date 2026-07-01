using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_GameManager : MonoBehaviour
{
    public void Awake()
    {
        // don't destroy this game object and opens the game scene
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("VRScene");
    }
}
