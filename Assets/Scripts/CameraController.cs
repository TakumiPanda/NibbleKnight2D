using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float xOffset, yOffset;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    void Update()
    {
        transform.position = new Vector3(player.position.x + lookAhead,player.transform.position.y + yOffset,transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (xOffset * player.localScale.x), Time.deltaTime * cameraSpeed);
    }
}
