public abstract class BaseState: IState
{
    protected Enemy _entity;
    protected float _stateDuration;

    public BaseState(Enemy entity)
    {
        this._entity = entity;
    }

    public abstract void Enter();
    public abstract void UpdateState();
    public abstract void Exit();
}