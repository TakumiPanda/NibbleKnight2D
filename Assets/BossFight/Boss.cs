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
    [SerializeField] BoxCollider2D _boxFightZone;
    
    private bool _inCombatMode;

    private new void Start()
    {
        base.Start();
        _inCombatMode = false;

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
    }

    private new void Update()
    {
        base.Update();
        // if(_moveCoroutine == null)
        // {
        //     StartCoroutine(Move());
        // }

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

            if(_enumEnemyState == EnumEnemyState.Patrol) _stateManager.ChangeState<EnemyWalkingState>(); 
            else if(_enumEnemyState == EnumEnemyState.Idle) _stateManager.ChangeState<EnemyIdleState>();
        }
    }
}
