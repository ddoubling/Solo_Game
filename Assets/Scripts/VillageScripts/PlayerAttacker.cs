using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerAttacker : MonoBehaviour
{
    //variables
    [SerializeField] private int Health = 3;
    [SerializeField] private int hit;
    [SerializeField] private int damaged;
    [SerializeField] private GameObject deathPartciles;
    private StateMachine stateMachine;


    public Player Target { get; set; }

    private void Awake()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponent<Animator>();
        stateMachine = new StateMachine();

        var search = new SearchForPlayer(this);
        var moveToPlayer = new MoveToNewPlayer(this, navMeshAgent, animator);
        var attackPlayer = new AttackPlayer(this, animator);
        //At lamda to reduce code
        //stateMachine addTransition calls 
        void At(AIState current, AIState option, Func<bool> condition) => stateMachine.AddTransition(current, option, condition);
        At(search, moveToPlayer, condition: HasTarget());
        At(moveToPlayer, search, condition: NonActive());
        At(moveToPlayer, attackPlayer, condition: InFriendlyRange());
        At(attackPlayer, moveToPlayer, condition: OutFriendlyRange());

        //conditions for states to follow
        Func<bool> HasTarget() => () => Target != null;
        Func<bool> NonActive() => () => moveToPlayer.TimeNonActive > 5f;
        Func<bool> InFriendlyRange() => () => Target != null && Vector3.Distance(a: transform.position, b: Target.transform.position) <= 0.8f;
        Func<bool> OutFriendlyRange() => () => Target != null && Vector3.Distance(a: transform.position, b: Target.transform.position) > 1f;

        //set initial state
        stateMachine.SetState(search);
    }



    // Update is called once per frame
    private void Update()
    {
        stateMachine.Motion();
        //  Debug.Log("here");

    }
    void OnCollisionEnter(Collision collision)
    {
        //collider to check if shot by bullet and then reduce health, if health 0 then destroy gameObject
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


