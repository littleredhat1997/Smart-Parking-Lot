using UnityEngine;

public class DestroyDelay : MonoBehaviour
{

    void Start()
    {
        Destroy(this.gameObject, 2.0f);
    }
}
