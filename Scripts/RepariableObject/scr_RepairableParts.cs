using Unity.VisualScripting;
using UnityEngine;

public class scr_RepairableParts : MonoBehaviour
{
    public bool isRepaired;
    public MeshRenderer meshRenderer;

    public void OnTriggerEnter(Collider other)
    {
        // if the repair tool hits the object
        if (other.CompareTag("RepairTool"))
        {
            // if the object was already repaired, then it is broken and the effects play for it
            if (isRepaired == true)
            {
                isRepaired = false;
                scr_RepairableObject.instance.PartBroken();
                meshRenderer.material = scr_RepairableObject.instance.repairedPartBroken;
                scr_AudioManager.instance.engineRepair.start();
                scr_AudioManager.instance.engineRepair.setParameterByName("RepairState", 1f);
            }
            // else, the object is repaired and the effects play for it
            else
            {
                isRepaired = true;
                scr_RepairableObject.instance.PartRepaired();
                meshRenderer.material = scr_RepairableObject.instance.repairedPartFixed;
                scr_AudioManager.instance.engineRepair.start();
                scr_AudioManager.instance.engineRepair.setParameterByName("RepairState", 0f);
            }
        }
    }
}
