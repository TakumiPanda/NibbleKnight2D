using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Data variables
    public Transform[] PatrolPoints;
    public EnemyDataSO EnemyData;
    
    // State Management variables
    protected StateManager _stateManager;

    // Runtime state variables
    public EnumEnemyState CurrEnemyState {get; set;}

    [SerializeField] protected int _currHealth;
    
    protected void Start()
    {
        _stateManager = new(this);
        _currHealth = EnemyData.MaxHealth;

        CurrEnemyState = EnumEnemyState.Idle;        
        _stateManager.PrepareStates(new Dictionary<Type, IState>
        {
            { typeof(EnemyFallbackState), new EnemyFallbackState(this) },
            { typeof(EnemyIdleState), new EnemyIdleState(this) },
            { typeof(EnemyWalkingState), new EnemyWalkingState(this) }
        });
        
        _stateManager.ChangeState<EnemyIdleState>();
    }

    protected void Update()
    {
        if(_currHealth <= 0) 
        {
            SendMessageUpwards("EndBossFight");
            gameObject.SetActive(false);
        }
        else
        {
            if (CurrEnemyState == EnumEnemyState.Walk)
            {
                _stateManager.ChangeState<EnemyWalkingState>();
            }
            else if (CurrEnemyState == EnumEnemyState.Idle)
            {
                _stateManager.ChangeState<EnemyIdleState>();
            }
            else
            {
                _stateManager.ChangeState<EnemyFallbackState>();
            }

            // Update State Associated Actions
            UpdateStateManager();
        }
    }

    protected virtual void UpdateStateManager()
    {
        _stateManager.UpdateStates();
    }

    protected virtual void OnCollisionEnter2D(Collision2D other) // Default for all enemies
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // Player damage code is buggy, this won't work properly until the player damage is handled properly
            other.gameObject.GetComponent<SwissHealthScript>().SwissDamaged(0.1f);
        }    
    }

    public void DamageEnemy(int damage)
    {
        _currHealth = Math.Clamp((_currHealth-=damage), 0, EnemyData.MaxHealth);
        SendMessageUpwards("UpdateHealthBar", _currHealth);
    }
}