using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine
{
    //Interface states
    private AIState currentStates;
    //variables
    //Dictionary holding different states as a key and list of the transition class as the value
    private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
    //list of tranision class for currentTransitions
    private List<Transition> currentTransitions = new List<Transition>();
    //list of tranision class for anyTransitions
    private List<Transition> anyTransitions = new List<Transition>();
    //list of EmptyTransitions
    private static List<Transition> EmptyTransitions = new List<Transition>(capacity: 0);

    private class Transition
    {
        //state that will be transitioned to
        public AIState Option { get; }
        //conditional to follow
        public Func<bool> Condition { get; }
        //constructor
         public Transition(AIState option, Func<bool> condition)
        {
            Option = option;
            Condition = condition;
        }
    }
    private Transition GetTransition()
    {
        //search list of transitions from anyTransitions list. If the condition is true then return that that transition
        foreach (var transition in anyTransitions)
            if (transition.Condition())
                return transition;
        // if above not met, then search list of transitions from currentTransitions list. If the condition is true then return that that transition
        foreach (var transition in currentTransitions)
            if (transition.Condition())
                return transition;
        return null;
    }

     public void AddTransition(AIState current, AIState option, Func<bool> condition)
    {
        //look up possible transitions stored in dictionary from the current state, if it doesn't exist then set it as new key and value
       
        if(transitions.TryGetValue(current.GetType(), out var _transitions) == false)
        { 
            //create new list
            _transitions = new List<Transition>();
            //add to dictionary of transitions
            transitions[current.GetType()] = _transitions;
        }
        //adding new transitions for specific state
        _transitions.Add(new Transition(option, condition));
    }

    public void AddAnyTransition(AIState state, Func<bool> condition)
    {
        anyTransitions.Add(new Transition(state, condition));
    }

 
    public void SetState(AIState state)
    {
        //returns if passed state is already the current state
        if (state == currentStates)
            return;
        //calls exit for prevoius state
        currentStates?.OnExit();
        //set passed state as current state
        currentStates = state;
        
        transitions.TryGetValue(currentStates.GetType(), out currentTransitions);
        if (currentTransitions == null)
            currentTransitions = EmptyTransitions;
        //enter passed state
        currentStates.OnEnter();

    }

   public void Motion()
    {
        //when Motion called: gets transition from anyTransition and currentTransition lists. If there is a transition then set the state. Then motion the current transition
        var transition = GetTransition();
        if (transition != null)
            SetState(transition.Option);
        currentStates?.Motion();
    }

}
