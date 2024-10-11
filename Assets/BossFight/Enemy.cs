using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    
    protected StateManager stateManager;

    // protected virtual void Awake()
    // {
    //     stateManager = new();
    //     IdleState idleState = new(this);
    //     stateManager.ChangeState(idleState);
    // }

    protected virtual void UpdateStateManager()
    {
        stateManager.UpdateStates();
    }
}