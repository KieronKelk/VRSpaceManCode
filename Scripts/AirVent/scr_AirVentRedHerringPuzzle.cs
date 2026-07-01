using UnityEngine;



public class scr_AirVentRedHerringPuzzle : MonoBehaviour
{
    public static scr_AirVentRedHerringPuzzle instance;
    public float numberOfScrewsLeft;
    public GameObject glassPanel, hammer;

    public void Awake()
    {
       // makes hammer not interactable at start of game
        hammer.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = false;
        instance = this;
    }

    public void screwDestroyed()
    {
        // once all screws are destroyed, the hammer becomes interactable and the glass drops
        numberOfScrewsLeft--;
        if (numberOfScrewsLeft <= 0)
        {
            glassPanel.GetComponent<Rigidbody>().isKinematic = false;
            hammer.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = true;
        }
    }
}
