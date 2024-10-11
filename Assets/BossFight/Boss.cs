using System;
using System.Collections.Generic;
using UnityEngine;

//     BOSS STATES +++

//     Idle
//     Walkling
//     RangeAttackMode
//     JumpAttackMode
//     MeeleAttackMode

public class Boss : Enemy
{
    private StateManager _normalStateManager;
    private StateManager _combatStateManager;
    private bool _inCombatMode;

    private bool _isWalking = false; //TEST CODE!!!!

    private void Start()
    {
        _inCombatMode = false;
        _normalStateManager = new StateManager(this, false); // Normal mode
        _combatStateManager = new StateManager(this, true); // Combat mode

        // Pre-create states for normal mode
        _normalStateManager.PrepareStates(new Dictionary<Type, IState>
        {
            { typeof(EnemyIdleState), new EnemyIdleState(this) },
            { typeof(EnemyWalkingState), new EnemyWalkingState(this) },
        });

        // Pre-create states for combat mode
        _combatStateManager.PrepareStates(new Dictionary<Type, IState>
        {
            { typeof(BossWalkingState), new BossWalkingState(this) },
            { typeof(BossRangeAttackState), new BossRangeAttackState(this) },
            { typeof(BossPunchAttackState), new BossPunchAttackState(this) },
            { typeof(BossJumpAttackState), new BossJumpAttackState(this) },
            { typeof(BossPostCombatState), new BossPostCombatState(this) }
        });

        // Start with IdleState in normal mode
        _normalStateManager.ChangeState<EnemyIdleState>();

        InvokeRepeating("FlipIsWalking", 2f, 2f); //TEST CODE!!!!
    }

    private void Update()
    {
        //Debug.Log("Updating");
        if (_inCombatMode)
        {
            _combatStateManager.UpdateStates();

            // if (/* condition for ranged attack */)
            // {
            //     _combatStateManager.ChangeState<BossRangeAttackState>();
            // }
        }
        else
        {
            _normalStateManager.UpdateStates();

            if(_isWalking) _normalStateManager.ChangeState<EnemyWalkingState>(); //TEST CODE!!!!
            else _normalStateManager.ChangeState<EnemyIdleState>(); //TEST CODE!!!!
        }
    }

    //TEST FUNCTION!!!!
    private void FlipIsWalking()
    {
        _isWalking = !_isWalking;
    }

    public void SwitchToCombatMode()
    {
        _inCombatMode = true;
        // Optionally reset combat state manager
        _combatStateManager.ChangeState<BossWalkingState>();
    }

    public void SwitchToNormalMode()
    {
        _inCombatMode = false;
        // Optionally reset normal state manager
        _normalStateManager.ChangeState<EnemyIdleState>();
    }
}
