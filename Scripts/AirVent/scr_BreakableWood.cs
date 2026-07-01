using UnityEngine;

public class scr_BreakableWood : MonoBehaviour
{

    public GameObject[] pieceOfWood;
    public void OnTriggerEnter (Collider other)
    {
       // breaks pieces of wood when the hammer hits it
        if (other.CompareTag("Hammer"))
        {
            for (int i = 0; i < pieceOfWood.Length; i++)
            {
                pieceOfWood[i].GetComponent<Rigidbody>().isKinematic = false;
                pieceOfWood[i].GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
