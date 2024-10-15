using UnityEngine;

public class EnemyWalkingState: BaseState
{
    private int targetPatrolPointIndex = 0;
    
    //Cache variable
    private Rigidbody2D _enemyRb2d;
    private Rigidbody2D _playerRb2d;

    private bool isBoss = false;

    public EnemyWalkingState(Enemy entity): base(entity)
    {
        _enemyRb2d = _entity.GetComponent<Rigidbody2D>();
    }

    public override void Enter()
    {
        if((_entity as Boss)?.CombatState != null) 
        {
            isBoss = true;
            _playerRb2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        }
        _entity.GetComponentInChildren<Animator>().SetBool("isWalking", true);
    }

    public override void UpdateState()
    {
        if(isBoss && (_entity as Boss).IsInCombat) //Combat Mode
        {
            _entity.GetComponentInChildren<Animator>().SetBool("isWalking", true);
            MoveTowardsPlayer();
        }
        else MoveTowardsPatrolPoint(); //Regular Mode
    }

    public override void Exit()
    {
        _entity.GetComponentInChildren<Animator>().SetBool("isWalking", false);
    }

    private void MoveTowardsPatrolPoint()
    {
        if (targetPatrolPointIndex < 0 || _entity.PatrolPoints.Length == 0) return;

        Transform targetPatrolPoint = _entity.PatrolPoints[targetPatrolPointIndex];
        float dist = Vector2.Distance(_enemyRb2d.position, targetPatrolPoint.position);
        Vector2 dir = ((Vector2)targetPatrolPoint.position - _enemyRb2d.position).normalized;

        // Move the enemy
        _enemyRb2d.MovePosition(_enemyRb2d.position + _entity.EnemyData.MaxSpeed * Time.fixedDeltaTime * dir);

        if (dist <= 0.1f)
        {
            targetPatrolPointIndex = (targetPatrolPointIndex + 1) % _entity.PatrolPoints.Length;
            _entity.CurrEnemyState = EnumEnemyState.Idle;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 targetPos = new(_playerRb2d.position.x, _enemyRb2d.position.y);
        float dist = Vector2.Distance(_enemyRb2d.position, targetPos);
        if (dist <= 3f) 
        {
            return;
        }
        Vector2 dir = (targetPos - _enemyRb2d.position).normalized;

        // Move the enemy (Added a 3x speed multiplier for Combat mode)
        _enemyRb2d.MovePosition(_enemyRb2d.position + _entity.EnemyData.MaxSpeed * 3f * Time.fixedDeltaTime * dir);
    }
}