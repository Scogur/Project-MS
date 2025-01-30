using UnityEngine;

public class ToNextLocation : MonoBehaviour
{
    Collider oc;

    void Start(){
        Debug.Log("script started");
    }
    public void OnTriggerEnter(Collider other) {
        oc = other;
        Debug.Log("Entered collision with: " + other.gameObject.name);
    }
}
