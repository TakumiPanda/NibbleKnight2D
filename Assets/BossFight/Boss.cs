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
    private StateManager _stateManager;
    private bool _inCombatMode;

    private bool _isWalking = false; //TEST CODE!!!!

    private void Start()
    {
        _inCombatMode = false;
        _stateManager = new StateManager(this);

        _stateManager.PrepareStates(new Dictionary<Type, IState>
        {
            { typeof(EnemyIdleState), new EnemyIdleState(this) },
            { typeof(EnemyWalkingState), new EnemyWalkingState(this) },
            { typeof(BossRangeAttackState), new BossRangeAttackState(this) },
            { typeof(BossPunchAttackState), new BossPunchAttackState(this) },
            { typeof(BossJumpAttackState), new BossJumpAttackState(this) },
            { typeof(BossPostCombatState), new BossPostCombatState(this) }
        });

        // Start with IdleState in normal mode
        _stateManager.ChangeState<EnemyIdleState>();

        InvokeRepeating("FlipIsWalking", 2f, 2f); //TEST CODE!!!!
    }

    private void Update()
    {
        //Debug.Log("Updating");
        if (_inCombatMode)
        {
            _stateManager.UpdateStates();

            // if (/* condition for ranged attack */)
            // {
            //     _stateManager.ChangeState<BossRangeAttackState>();
            // }
        }
        else
        {
            _stateManager.UpdateStates();

            if(_isWalking) _stateManager.ChangeState<EnemyWalkingState>(); //TEST CODE!!!!
            else _stateManager.ChangeState<EnemyIdleState>(); //TEST CODE!!!!
        }
    }

    //TEST FUNCTION!!!!
    private void FlipIsWalking()
    {
        _isWalking = !_isWalking;
    }
}
