using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwissMovementTransformScript : MonoBehaviour
{

    public float m_speed = 10.0f;

    public GameObject m_Swiss;

    public bool onGround;

    public bool alreadyJump;

    // Start is called before the first frame update
    void Start()
    {
        onGround = true;
        alreadyJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKey(KeyCode.A))
        // {
        //     m_Swiss.transform.Translate(Vector3.left * Time.deltaTime * m_speed);
        // }
        // else if(Input.GetKey(KeyCode.D))
        // {
        //     m_Swiss.transform.Translate(Vector3.right * Time.deltaTime * m_speed);
        // }
        // else
        // {
            
        // }
        float m_horizontalInput = Input.GetAxis("Horizontal");

        m_Swiss.transform.Translate(Vector3.right * Time.deltaTime * m_speed * m_horizontalInput);

        // if(Input.GetKeyDown(KeyCode.A))
        // {
        //     m_Swiss.transform.RotateAround(m_Swiss.transform.position, transform.up, 180.0f);
        // }
        // else if(Input.GetKeyDown(KeyCode.D))
        // {
        //     m_Swiss.transform.RotateAround(m_Swiss.transform.position, transform.up, -180.0f);  
        // }

    }
}
