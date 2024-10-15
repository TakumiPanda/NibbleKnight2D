using UnityEngine;

public class BossStunState: BossCombatState
{
    public BossStunState(Boss boss): base(boss) {}

    public override void Enter()
    {
        Debug.Log("Enters Stunned");
        _stateDuration = 1.5f;
        _boss.GetComponentInChildren<Animator>().SetBool("isStunned", true);
    }

    public override void UpdateState()
    {
        // Stun State update logic
        _stateDuration -= Time.deltaTime;
        if(_stateDuration <= 0)
        {
            _stateDuration = 1.5f;
            _boss.CombatState = EnumBossCombatState.FallbackState;
        }
    }

    public override void Exit()
    {
        _entity.GetComponentInChildren<Animator>().SetBool("isStunned", false);
    }
}