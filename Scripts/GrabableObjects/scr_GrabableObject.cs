using Unity.VisualScripting;
using UnityEngine;

public class scr_GrabableObject : MonoBehaviour
{
    public bool canPlayerPickUp, isPickedUp;

    public void OnCollisionEnter3D()
    {
        canPlayerPickUp = true;
    }

    public void Update()
    {
        if (canPlayerPickUp == true & isPickedUp == false)
        {

        }
    }
}
