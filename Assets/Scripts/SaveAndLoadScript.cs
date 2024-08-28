using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadScript : MonoBehaviour
{

    // public GameObject[] m_SavePoints;

    public GameObject m_Text;

    private bool onCollider;

    private bool alreadySave;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && onCollider && !alreadySave)
        {
            SaveGame();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && onCollider)
        {
            PlayerPrefs.DeleteKey("SaveKey");
            Debug.Log("2 Pressed, Key is deleted!");
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && onCollider)
        {
            Debug.Log(PlayerPrefs.HasKey("SaveKey"));
        }
    }

    public void SaveGame()
    {
        Debug.Log("1 pressed, you have saved!");
        /*TODO: Add specific data to String:
        name: Swiss
        Date save -> TimeDate.today
        location of save point
        health %
        */
        PlayerPrefs.SetString("SaveKey","1");
        alreadySave = true;
    }

    // public void CreatingDataString()
    // {

    // }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision Entered");
        if(other.gameObject.CompareTag("Player"))
        {
            m_Text.SetActive(true);
            onCollider = true;
            alreadySave = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        onCollider = false;
        if(m_Text != null && m_Text.activeInHierarchy)
        {
            m_Text.SetActive(false);
            alreadySave = false;
        }
    }
}
