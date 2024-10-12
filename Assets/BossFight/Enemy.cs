using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform[] _patrolPoints;
    protected int _currPatrolPointIndex = -1;
    
    protected StateManager _stateManager;

    [SerializeField] private EnemyDataSO _enemyData;
    [SerializeField] protected EnumEnemyState _enumEnemyState;

    private Coroutine patrolCoroutine;

    protected void Start()
    {
        _stateManager = new(this);
        _enumEnemyState = EnumEnemyState.Idle;
        foreach(Transform p in _patrolPoints)
        {
            p.SetParent(null);
        }    
        if(_patrolPoints.Length > 0) _currPatrolPointIndex = 0;
    }
    // protected virtual void Awake()
    // {
    //     stateManager = new();
    //     IdleState idleState = new(this);
    //     stateManager.ChangeState(idleState);
    // }

    private IEnumerator Patrol()
    {
        _enumEnemyState = EnumEnemyState.Patrol;
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();

        int targetPatrolPointIndex = (_currPatrolPointIndex + 1) % _patrolPoints.Length;
        Vector2 targetPatrolPos = _patrolPoints[targetPatrolPointIndex].position; 

        float dist;
        
        while(true)
        {
            dist = Vector2.Distance(transform.position, _patrolPoints[targetPatrolPointIndex].position);
            Vector2 dir = (targetPatrolPos - rb2d.position).normalized;

            rb2d.MovePosition(rb2d.position + _enemyData.MaxSpeed * Time.fixedDeltaTime * dir);
            
            if(dist <= 0.1f)
            {
                rb2d.position = targetPatrolPos;
                break;
            }

            yield return new WaitForFixedUpdate();
        }
        
        _enumEnemyState = EnumEnemyState.Idle;
        yield return new WaitForSeconds(1.5f);

        _currPatrolPointIndex = targetPatrolPointIndex;
        patrolCoroutine = null;
    }

    protected void Update()
    {
        if(patrolCoroutine == null && _enumEnemyState == EnumEnemyState.Idle)
        {
            patrolCoroutine = StartCoroutine(Patrol());
        }    
    }

    protected virtual void UpdateStateManager()
    {
        _stateManager.UpdateStates();
    }
}