using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAreaChange : MonoBehaviour
{

    public AudioClip m_music;

    public AudioSource m_AudioSource;

    public bool m_changeMusic = false;
    public bool isDone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_changeMusic && !isDone)
        {
            m_AudioSource.Stop();
            m_AudioSource.clip = m_music;
            m_AudioSource.Play();
            isDone = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Trigger");
        m_changeMusic = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        isDone = false;
        m_changeMusic = false;
    }
}
