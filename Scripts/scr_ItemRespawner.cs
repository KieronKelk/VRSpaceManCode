using UnityEngine;

public class scr_ItemRespawner : MonoBehaviour
{
    public GameObject respawnItem;
    public Vector3 respawnPos;
    public string tagName;

    public void OnTriggerEnter(Collider other)
    {
    // if key item enters area
        if (other.CompareTag(tagName))
        { 
        // reset key item's to spawn positon
           respawnItem.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
           respawnItem.GetComponent<Transform>().position = respawnPos;
        }
    }
}
