using UnityEngine;

public class CrateCollisionBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("BossNPC"))
        {
            Boss boss = other.gameObject.GetComponent<Boss>();
            if(boss.IsInCombat && boss.CombatState != EnumBossCombatState.Stun)
            {
                boss.CombatState = EnumBossCombatState.Stun;
            } 
        }
        Destroy(gameObject, 0.5f);
    }
}
