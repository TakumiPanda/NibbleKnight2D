using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsAutoScrollingScript : MonoBehaviour
{
    public float m_Speed = 100.0f;

    public float m_Limit = 2000.0f;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < m_Limit)
        {
            transform.Translate(Vector3.up * m_Speed * Time.deltaTime);
        }
    }
}
