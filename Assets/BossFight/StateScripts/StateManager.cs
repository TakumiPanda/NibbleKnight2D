using System;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{
    private IState _oldState, _currentState;
    private Dictionary<Type, IState> _stateInstances = new();

    public StateManager(Enemy entity)
    {
        //_currentState = new EnemyIdleState(entity);
        // Constructor logic if needed
    }

    public void PrepareStates(Dictionary<Type, IState> states)
    {
        foreach (var state in states)
        {
            _stateInstances[state.Key] = state.Value;
        }
        _currentState = _stateInstances[typeof(EnemyIdleState)];
    }

    public void ChangeState<T>() where T : IState
    {
        _currentState?.Exit();
        if (_stateInstances.TryGetValue(typeof(T), out IState newState))
        {
            _oldState = _currentState;
            _currentState = newState;
            
            if(_oldState?.GetType() == _currentState?.GetType()) return;
            
            _currentState.Enter();
        }
        else
        {
            Debug.LogError("State not found: " + typeof(T));
        }
    }

    public void UpdateStates()
    {
        _currentState?.UpdateState();
    }
}
