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
        //TODO: check if options volume already set to 0, skip this
        if(m_changeMusic && !isDone && m_music != m_AudioSource.clip)
        {
            float currentVolume = m_AudioSource.volume;
            while(m_AudioSource.volume != 0)
            {
                float temp = m_AudioSource.volume;
                temp -= Time.deltaTime;
                m_AudioSource.volume = temp;
            }
            m_AudioSource.Stop();
            m_AudioSource.clip = m_music;
            m_AudioSource.Play();
            while(m_AudioSource.volume < currentVolume)
            {
                float temp = m_AudioSource.volume;
                temp += Time.deltaTime;
                m_AudioSource.volume = temp;
            }
            if(m_AudioSource.volume > currentVolume)
            {
                m_AudioSource.volume = currentVolume;
            }
            isDone = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger");
            m_changeMusic = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        isDone = false;
        m_changeMusic = false;
    }
}
