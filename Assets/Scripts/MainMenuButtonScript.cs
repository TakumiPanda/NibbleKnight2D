using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonScript : MonoBehaviour
{

    public GameObject m_MainMenuButtonScript;
    public GameObject m_Options;

    public GameObject m_ExitScreen;
    public AudioSource m_BackgroundMusicAudioSource;

    void Start()
    {
        if(PlayerPrefs.HasKey("musicVolume"))
        {
            m_BackgroundMusicAudioSource.volume = PlayerPrefs.GetFloat("musicVolume");
        }
    }

    public void StartGame()
    {
        Debug.Log("Game Starting");
        SceneManager.LoadScene("TutorialScene");
    }

    public void Options()
    {
        Debug.Log("Options Pressed");
        PlayerPrefs.SetFloat("prevMusicVolume", m_BackgroundMusicAudioSource.volume);
        m_MainMenuButtonScript.SetActive(false);
        m_Options.SetActive(true);
    }

    public void Credits()
    {
        Debug.Log("Credits Pressed");
        SceneManager.LoadScene("CreditsScene");
    }

    public void Exit()
    {
        Debug.Log("Exit Pressed");
        m_MainMenuButtonScript.SetActive(false);
        m_ExitScreen.SetActive(true);
        Application.Quit();
    }
}
