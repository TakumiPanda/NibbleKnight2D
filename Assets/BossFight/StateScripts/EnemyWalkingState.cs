using UnityEngine;

public class EnemyWalkingState: BaseState
{
    public EnemyWalkingState(Enemy entity): base(entity){}

    public override void Enter()
    {
        _entity.GetComponentInChildren<Animator>().SetBool("isWalking", true);
    }

    public override void UpdateState()
    {
        // Walking State update logic
    }

    public override void Exit()
    {
        _entity.GetComponentInChildren<Animator>().SetBool("isWalking", false);
    }
}