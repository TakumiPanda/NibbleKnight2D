using UnityEngine;

public class EnemyIdleState: BaseState
{
    public EnemyIdleState(Enemy entity): base(entity){}

    public override void Enter()
    {
        Debug.Log("Enters Idle");
        _stateDuration = 1.5f;
        _entity.GetComponentInChildren<Animator>().SetBool("isIdle", true);
    }

    public override void UpdateState()
    {
        // Idle State update logic
        _stateDuration -= Time.deltaTime;
        if(_stateDuration <= 0)
        {
            _stateDuration = 1.5f;
            _entity.CurrEnemyState = EnumEnemyState.Walk;
        }
    }

    public override void Exit()
    {
        _entity.GetComponentInChildren<Animator>().SetBool("isIdle", false);
        // Idle state exit logic
    }
}