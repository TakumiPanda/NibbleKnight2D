using UnityEngine;
public class BossHissyFitState: BossCombatState
{
    public BossHissyFitState(Boss boss): base(boss) {}

    public override void Enter()
    {
        Debug.Log("Enters Hissy Fit");
        _stateDuration = 5f;
        _entity.GetComponentInChildren<Animator>().SetBool("isAngry", true);
        _boss.ActivateTrashDrop();
    }

    public override void UpdateState()
    {
        // HissyFit State update logic
        _stateDuration -= Time.deltaTime;
        if(_stateDuration <= 0)
        {
            _stateDuration = 5f;
            _boss.CombatState = EnumBossCombatState.FallbackState;
        }
    }

    public override void Exit()
    {
        _entity.GetComponentInChildren<Animator>().SetBool("isAngry", false);
    }
}