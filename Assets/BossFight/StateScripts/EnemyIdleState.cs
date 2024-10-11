using System.Xml;
using UnityEngine;

public class EnemyIdleState: BaseState
{
    public EnemyIdleState(Enemy entity): base(entity){}

    public override void Enter()
    {
        _entity.GetComponentInChildren<Animator>().SetBool("isIdle", true);
        // Idle state enter logic
    }

    public override void UpdateState()
    {
        // Idle State update logic
    }

    public override void Exit()
    {
        _entity.GetComponentInChildren<Animator>().SetBool("isIdle", false);
        // Idle state exit logic
    }
}