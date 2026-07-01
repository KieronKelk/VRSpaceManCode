using UnityEngine;

public class scr_AirVentScrews : MonoBehaviour
{
    public GameObject screw;
    public void OnTriggerEnter (Collider other)
    {
        // when hit with the repair tool, the screw is destroyed
        if (other.CompareTag("RepairTool"))
        {
            scr_AirVentRedHerringPuzzle.instance.screwDestroyed();
            Destroy(screw);
        }
    }
}
