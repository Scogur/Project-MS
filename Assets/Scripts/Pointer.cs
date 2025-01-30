using UnityEngine;

public class Pointer : MonoBehaviour
{
    void Start()
    {
        float lifetime = GetComponent<ParticleSystem>().startLifetime;
        GameObject.Destroy(this.gameObject, lifetime);
    }

}
