using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

    public AudioSource m_BackgroundMusic;


    // Start is called before the first frame update
    void Start()
    {
        m_BackgroundMusic.volume = PlayerPrefs.GetFloat("musicVolume");
        if(Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
        }
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
