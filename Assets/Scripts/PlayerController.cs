using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour
{
    CustomControls input;

    public CinemachineCamera camera;

    NavMeshAgent agent;
    Animator animator;

    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    float lookRotationSpeed = 8f;

    [Header("Attack")]
    [SerializeField] float attackSpeed = 1.5f;
    [SerializeField] float attackDistance = 1.5f;
    [SerializeField] float attackDelay = 0.3f;
    [SerializeField] int attackDamage = 40;
    [SerializeField] ParticleSystem hitEffect;
    bool playerBusy = false;
    Interactable target;

    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        input = new CustomControls();
        AssignInputs();
    }

    void AssignInputs(){
        input.Main.Move.performed += ctx => ClickToMove();
        input.Main.Zoom.performed += ctx => Zoom();
    }

    void ClickToMove(){
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers)){
            if (hit.transform.CompareTag("Interactable")){
                target = hit.transform.GetComponent<Interactable>();
                target.GetComponent<Outline>().enabled = true;
                if (clickEffect != null){
                    Instantiate(clickEffect, hit.point +=  new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                }
            }
            else {
                if (target != null) target.GetComponent<Outline>().enabled = false;
                target = null;
                agent.destination = hit.point;
                if (clickEffect != null){
                    Instantiate(clickEffect, hit.point +=  new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                }
            }            
        }
    }

    void Update(){
        FollowTarget();
        FaceTarget();
        SetAnimations();
    }

    void FollowTarget(){
        if (target == null) return;
        if (Vector3.Distance(target.transform.position, transform.position) <= attackDistance){
            ReachDistance();
        }
        else {
            agent.SetDestination(target.transform.position);
        }
    }

    void ReachDistance(){
        agent.SetDestination(transform.position);
        if (playerBusy) return;

        playerBusy = true;
        switch (target.interactionType){
            case Interactable.InteractableType.Enemy:
                //animator.Play();

                Invoke(nameof(SendAttack), attackDelay);
                Invoke(nameof(ResetBusyState), attackSpeed);
                break;
            case Interactable.InteractableType.Item:
                break;
            case Interactable.InteractableType.Building:
                InteractWithBuilding();
                break;
        }
    }

    void InteractWithBuilding(){
        Debug.Log(target.transform.gameObject);
    }

    void SendAttack(){
        if (target == null) return;
        if (target.myActor.currentHealth <= 0) {
            target = null;
            return;
        }
        Instantiate(hitEffect, target.transform.position + new Vector3(0,1,0), Quaternion.identity);
        Debug.Log("Hit effect instantiated");
        target.GetComponent<Actor>().TakeDamage(attackDamage);
        Debug.Log("Target hit " + target.transform.position);
    }

    void ResetBusyState(){
        playerBusy = false;
        //SetAnimations();
    }

    void FaceTarget(){}
    void SetAnimations(){}

    void Zoom(){
        float zoom = input.Main.Zoom.ReadValue<float>();
        float min = 2f;
        float max = 20f;
        if (zoom > 0){
            if (camera.GetComponent<CinemachinePositionComposer>().CameraDistance < max){
                camera.GetComponent<CinemachinePositionComposer>().CameraDistance += 1f;
            }
            
        }
        else if (zoom < 0){
            if (camera.GetComponent<CinemachinePositionComposer>().CameraDistance > min){
                camera.GetComponent<CinemachinePositionComposer>().CameraDistance -= 1f;
            }
        }
    }

    void OnEnable(){
        input.Enable();
    }

    void OnDisable(){
        input.Disable();
    }
}
