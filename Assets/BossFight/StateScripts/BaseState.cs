public abstract class BaseState: IState
{
    protected Enemy _entity;

    public BaseState(Enemy entity)
    {
        this._entity = entity;    
    }

    public abstract void Enter();
    public abstract void UpdateState();
    public abstract void Exit();
}