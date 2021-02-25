using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyAI : MonoBehaviour
{
    //Variables
    [SerializeField] private int maxHolding = 20;
    [SerializeField] private int Health = 10;
    [SerializeField] private int foraged = 0;
    [SerializeField] private int damaged;
    public bool NoHealth;
    private StateMachine stateMachine;
    public bool cowar;

    //gats and sets for trees to target and the StoragePits
    public Foragables Target { get; set; }

    public StoragePit StoragePit { get; set;}

    private void Awake()
    {
        stateMachine = new StateMachine();

        //setting components and states
        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponent<Animator>();
        var enemyDetection = gameObject.AddComponent<EnemyDetection>();
        var enemyAIDetection = gameObject.AddComponent<EnemyAIDetection>();
        var search = new SearchForResource(this);
        var moveToResource = new MoveToNewResource(this, navMeshAgent, animator);
        var forage = new ForageResource(this, animator);
        var returnToPit = new ReturnToPit(this, navMeshAgent, animator);
        var resourceInPit = new ResourceInPit(this);
        var runAway = new RunAway(this, navMeshAgent, enemyDetection, animator);
        var cower = new Cower(animator);

        // // void At to reduce code
        //stateMachine checks current state, its option and predicate condition to follow
        void At(AIState current, AIState option, Func<bool> condition) => stateMachine.AddTransition( current, option, condition);
        At(search, moveToResource, condition: HasTarget()) ;
        At(moveToResource, search, condition: NonActive());
        At(moveToResource, forage, condition: InResourceLocation());
        At(forage, search, condition: NoResourceMinHolding());
        At(forage, search, condition: ResourceMoved());
        At(forage, returnToPit, condition: HoldingMax());
        At(search, returnToPit, HoldingMax());
        At(moveToResource, returnToPit, HoldingMax());
        At(returnToPit, resourceInPit, condition: BesidePit());
        At(resourceInPit, search, condition:() => foraged == 0);
        At(runAway, search, () => enemyDetection.EnemyDetected == false);
        At(cower, search, () => enemyAIDetection.EnemyDetected == false);

        //move from any state to the runAway or cower if either enemy detection is truel
        stateMachine.AddAnyTransition(runAway, () => enemyDetection.EnemyDetected);
        stateMachine.AddAnyTransition(cower, () => enemyAIDetection.EnemyDetected);
       
        //Conditions to motion into each state from another
        Func<bool> HasTarget() => () => Target != null;
        Func<bool> NonActive() => () => moveToResource.TimeNonActive > 3f;
        Func<bool> InResourceLocation() => () => Target != null && Vector3.Distance(a: transform.position, b: Target.transform.position) <= 1.3f;
        Func<bool> ResourceMoved() => () => Target != null && Vector3.Distance(a: transform.position, b: Target.transform.position) > 2f;
        Func<bool> NoResourceMinHolding() => () => (Target == null || Target.depletedResource) && !HoldingMax().Invoke();
        Func<bool> HoldingMax() => () => foraged >= maxHolding;
        Func <bool> BesidePit() => () => StoragePit != null && Vector3.Distance(a:transform.position, b:StoragePit.transform.position) < 1f;

        //set initial state
        stateMachine.SetState(search);
    }

    // Update is called once per frame
    private void Update()
    {
        //calls motion of states each update
        stateMachine.Motion();
       // Debug.Log("here");
    }
 
    public void TakeFromTarget()
    {
        // if Take returns true the add to foraged and start coroutine to see if tree need moved.
        if (Target.Take())
        {
            foraged++;
        }

    }

    public bool Take()
    {
        //used to only allow dropping resources at storage if holding
        Debug.Log("dropping resource");
        if (foraged <= 0)
            return false;
        foraged--;
        return true;
    }

    public void Hit()
    {
        //when called, increase damage counter, if count <= 0 then Set bool NoHealth to true and set gameObject to false;
        damaged++;
        float count = Health - damaged;
        if (count <= 0)
        {
            gameObject.SetActive(false);
            NoHealth = true;
            LostFriendly();

        }

    }
    void LostFriendly()
    {
        if (GameObject.FindGameObjectsWithTag("friendly").Length <= 0)
            FindObjectOfType<GameManager>().EndGame();
    }
}
