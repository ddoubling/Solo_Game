using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AIState
{
    //Each motion taken
    void Motion();
    //As the call enters the motion
    void OnEnter();
    //as the call exits the motion
    void OnExit();
}
