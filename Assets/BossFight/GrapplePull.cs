using UnityEngine;

public partial class GrappleHook // Grapple Pull Object
{
    private void TryDropItem()
    {
        if(Input.GetKeyDown(KeyCode.E) && newOb?.GetComponent<Rigidbody2D>() != null)
        {                
            newOb.GetComponent<Rigidbody2D>().gravityScale = 10f;
            stopGrapple();  
        }
    }
}
