using UnityEngine;

public class scr_DontDestroyOnLoad : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
