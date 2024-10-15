using System;
using System.Collections.Generic;
using UnityEngine;

public enum EnumBossCombatState
{
    FallbackState,
    Stun,
    HissyFit,
    PunchAttack,
    RangeAttack,
    JumpAttack
}

public class Boss : Enemy
{
    [SerializeField] BoxCollider2D _boxFightZone;
    public bool IsInCombat {get; set;}
    public EnumBossCombatState CombatState { get; set; }
    private StateManager _combatStateManager;

    private new void Start()
    {
        base.Start();
        _combatStateManager = new(this);
        _combatStateManager.PrepareStates(new Dictionary<Type, IState>
        {
            { typeof(BossFallbackState), new BossFallbackState(this) },
            { typeof(BossStunState), new BossStunState(this) },
            { typeof(BossHissyFitState), new BossHissyFitState(this) },
            { typeof(BossRangeAttackState), new BossRangeAttackState(this) },
            { typeof(BossPunchAttackState), new BossPunchAttackState(this) },
            { typeof(BossJumpAttackState), new BossJumpAttackState(this) },
            { typeof(BossPostCombatState), new BossPostCombatState(this) }
        });

        IsInCombat = false;
        CombatState = EnumBossCombatState.FallbackState; 
        _combatStateManager.ChangeState<BossFallbackState>();
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
            if (IsInCombat)
            {
                HandleCombat();
                _combatStateManager.UpdateStates();
            }
            
            base.Update(); // Must be placed after HandleCombat()
        }
    }

    private void HandleCombat()
    {
        Debug.Log(CombatState);

        switch(CombatState)
        {
            case EnumBossCombatState.FallbackState:
                _combatStateManager.ChangeState<BossFallbackState>();
                CurrEnemyState = EnumEnemyState.Walk;
                break;
            case EnumBossCombatState.Stun: 
                CurrEnemyState = EnumEnemyState.FallbackState;
                _combatStateManager.ChangeState<BossStunState>();
                break;
            case EnumBossCombatState.HissyFit:
                CurrEnemyState = EnumEnemyState.FallbackState;
                _combatStateManager.ChangeState<BossHissyFitState>();
                break;
            case EnumBossCombatState.PunchAttack: 
                _combatStateManager.ChangeState<BossPunchAttackState>();
                break;
            case EnumBossCombatState.RangeAttack: 
                _combatStateManager.ChangeState<BossRangeAttackState>();
                break;
            case EnumBossCombatState.JumpAttack: 
                _combatStateManager.ChangeState<BossJumpAttackState>();
                break;
            default: 
                Debug.LogError("Invalid Combat State");
                break;
        }
    }

    private new void OnCollisionEnter2D(Collision2D other)
    {
        // Damage boss when collides with player in stun mode
        if(other.gameObject.CompareTag("Player"))
        {
            if(CombatState == EnumBossCombatState.Stun)
            {
                DamageEnemy(1);
                CombatState = EnumBossCombatState.HissyFit;
            }
            else
            { 
                // Player damage code is buggy, this won't work properly until the player damage is handled properly
                other.gameObject.GetComponent<SwissHealthScript>().SwissDamaged(0.25f);
            }
        }    
    }

    public void ActivateTrashDrop()
    {
        SendMessageUpwards("DropTrash", SendMessageOptions.RequireReceiver);
    }
}
