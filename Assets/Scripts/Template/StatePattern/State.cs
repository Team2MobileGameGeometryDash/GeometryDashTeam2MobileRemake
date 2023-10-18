using UnityEngine;

/// <summary>
/// Basic state template 
/// </summary>
/// <typeparam name="TStateIDType">The type of the state ID</typeparam>
public abstract class State<TStateIDType>
{
    public TStateIDType StateID;
    protected StatesMachine<TStateIDType> m_stateMachine;

    public State(TStateIDType stateID, StatesMachine<TStateIDType> stateMachine = null)
    {
        StateID = stateID;
        m_stateMachine = stateMachine;
    }

    public virtual void OnEnter()
    {
        //Debug.Log("OnEnter " + StateID);

    }

    public virtual void OnUpdate()
    {
        //Debug.Log("OnUpadte " + StateID);
    }

    public virtual void OnFixedUpdate()
    {
        //Debug.Log("OnUpadte " + StateID);
    }


    public virtual void OnLateUpdate()
    {

    }


    public virtual void OnExit()
    {
        //Debug.Log("OnExit " + StateID);
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }

}

