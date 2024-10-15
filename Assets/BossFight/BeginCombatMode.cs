using UnityEngine;

public class BeginCombatMode : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            SendMessageUpwards("StartBossFight", SendMessageOptions.RequireReceiver);
        }    
    }
}
