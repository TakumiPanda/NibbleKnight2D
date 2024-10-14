using System;
using System.Collections.Generic;
using UnityEngine;

//     BOSS STATES +++

//     Idle
//     Walkling
//     RangeAttackMode
//     JumpAttackMode
//     MeeleAttackMode

public enum BossCombatState
{
    Off,
    HissyFit,
    PunchAttack,
    RangeAttack,
    JumpAttack
}

public class Boss : Enemy
{
    [SerializeField] BoxCollider2D _boxFightZone;
    public BossCombatState CombatState { get; set; }

    private new void Start()
    {
        base.Start();

        _stateManager.PrepareStates(new Dictionary<Type, IState>
        {
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
        if(_currHealth <= 0) 
        {
            SendMessageUpwards("EndBossFight");
            gameObject.SetActive(false);
        }
        else
        {
            if (CurrEnemyState == EnumEnemyState.Combat)
            {
                _stateManager.ChangeState<EnemyWalkingState>();
                _stateManager.UpdateStates();
            }
            else
            {
                base.Update(); // behave like a normal enemy when not in combat mode
            }
        }
    }
}
