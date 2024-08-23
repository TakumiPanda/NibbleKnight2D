using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public bool launch;
    public float launch_Velocity = 20f;
    public Transform destination;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!launch) {
                if (Vector2.Distance(player.transform.position, transform.position) > 0.7f)
                {
                    player.transform.position = destination.transform.position;
                    // player.velocity = new Vector2(player.velocity.x ,launch_Velocity);
                    // have the player launch out of the pipe at a high speed
                }
            } else {
                if(Input.GetKeyDown(KeyCode.E)) {
                    if (Vector2.Distance(player.transform.position, transform.position) > 0.7f)
                    {
                        player.transform.position = destination.transform.position;
                        
                    }
                }
            }
        }
    }
}
