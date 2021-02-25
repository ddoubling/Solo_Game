using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyAI : MonoBehaviour
{

    //variables
    [SerializeField] private int Health = 5;
    private StateMachine stateMachine;
    [SerializeField] private int hit;
    [SerializeField] private int damaged;
    [SerializeField] GameObject deathPartciles;

    //get and set
    public FriendlyAI Target { get; set; }
    public SpawnEnemyAI Spawn { get; set; }

    private void Awake()
    {
        //sets variables for NavMeshAgent and Animator
        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponent<Animator>();

        //creates new state machine for gameObject
        stateMachine = new StateMachine();

        // 
        var search = new SearchForFriendly(this);
        var moveToFriendly = new MoveToNewFriendly(this, navMeshAgent, animator);
        var attackFriendly = new AttackFriendly(this, animator);
       
        // void At to reduce code
        //stateMachine checks current state, its option and predicate condition to follow
        void At(AIState current, AIState option, Func<bool> condition) => stateMachine.AddTransition( current,  option, condition);  

        //Transition order and their condition
        At(search,  moveToFriendly, condition: HasTarget());
        At(moveToFriendly, search, condition: NonActive());
        At(moveToFriendly, attackFriendly, condition: InFriendlyRange());
        At(attackFriendly, search, condition: KilledTarget());
        At(attackFriendly, moveToFriendly,condition: OutFriendlyRange());

        //conditionals for the states
        Func<bool> HasTarget() => () => Target != null;
        Func<bool> NonActive() => () => moveToFriendly.TimeNonActive > 5f;
        Func<bool> InFriendlyRange() => () => Target != null && Vector3.Distance(a: transform.position, b: Target.transform.position) <= 0.8f;
        Func<bool> OutFriendlyRange() => () => Target != null && Vector3.Distance(a: transform.position, b: Target.transform.position) > 5f;
        Func<bool> KilledTarget() => () => (Target == null || Target.NoHealth);

        //set initial state
        stateMachine.SetState(search);
    }

    private void Update()
    {
        //Update to call the motion of states
        stateMachine.Motion();


    }

    // collidor used to check for bullet collision
    void OnCollisionEnter(Collision collision)
    {
        //the collider was from a bullet then increase damage, check the count of health to damage and if =<0 then set false and destroy
        var shot = collision.collider.GetComponent<BulletFire>();
        if (shot)
        {
            damaged++;
            float count = Health - damaged;
            if (count <= 0)
            {
                if (deathPartciles != null)
                    Instantiate(deathPartciles, this.transform.position, this.transform.rotation);
                gameObject.SetActive(false);
                Destroy(gameObject, 2f);
                
                

            }
        }
    }
}
