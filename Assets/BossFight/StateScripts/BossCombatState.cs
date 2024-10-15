public abstract class BossCombatState: BaseState
{
    protected Boss _boss;
    
    public BossCombatState(Boss boss): base(boss)
    {
        this._boss = boss;
    }
}