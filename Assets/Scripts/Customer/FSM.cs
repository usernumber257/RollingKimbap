using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public State curState;

    public FSM(State state)
    {
        curState = state;
    }

    public void ChangeState(State nextState)
    {
        if (nextState == curState)
            return;

        if (curState != null)
            curState.OnStateExit();

        curState = nextState;
        curState.OnStateEnter();
    }

    public void UpdateState()
    {
        curState.OnStateUpdate();
    }
}
