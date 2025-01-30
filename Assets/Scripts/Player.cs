using UnityEngine;

public class Player : MonoBehaviour
{
    public Actor myActor {get; private set;}

    void Awake(){
        myActor = GetComponent<Actor>();
    }
}
