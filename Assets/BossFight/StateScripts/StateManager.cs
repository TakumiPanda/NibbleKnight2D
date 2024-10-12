using System;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{
    private IState _currentState;
    private Dictionary<Type, IState> _stateInstances = new();

    public StateManager(Enemy entity)
    {
        // Constructor logic if needed
    }

    public void PrepareStates(Dictionary<Type, IState> states)
    {
        foreach (var state in states)
        {
            _stateInstances[state.Key] = state.Value;
        }
    }

    public void ChangeState<T>() where T : IState
    {
        _currentState?.Exit();

        if (_stateInstances.TryGetValue(typeof(T), out IState newState))
        {
            _currentState = newState;
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
