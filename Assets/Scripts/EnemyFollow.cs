using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] bool enabled = true;
    UnityEngine.AI.NavMeshAgent agent;


    [Header("Attack")]
    [SerializeField] float attackSpeed = 1.5f;
    [SerializeField] float attackDistance = 2.5f;
    [SerializeField] float attackDelay = 0.3f;
    [SerializeField] int attackDamage = 10;
    [SerializeField] ParticleSystem hitEffect;
    bool playerBusy = false;
    [SerializeField] public GameObject player;
    Player target;

    void Awake(){
        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //target = player.transform.GetComponent<Player>();
    }

    void Start(){
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = player.transform.GetComponent<Player>();
    }

    void Update()
    {
        FollowTarget();
        FaceTarget();
        SetAnimations();
        Debug.Log(target.transform.position);
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
        //animator.Play();

        Invoke(nameof(SendAttack), attackDelay);
        Invoke(nameof(ResetBusyState), attackSpeed);
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
}
