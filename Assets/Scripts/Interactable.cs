using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractableType {
        Enemy,
        Item,
        Building,
    }
    
    public Actor myActor {get; private set;}
    public Building myBuilding {get; private set;}

    public InteractableType interactionType;

    void Awake(){
        if (interactionType == InteractableType.Enemy){
            myActor = GetComponent<Actor>();
        }
        else if (interactionType == InteractableType.Building){
            myBuilding = GetComponent<Building>();
        }
    }

    public void InteractWithItem(){
        Destroy(gameObject);
    }

    public void InteractWithBuilding(){
        
    }
}
