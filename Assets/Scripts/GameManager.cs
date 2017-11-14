using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public StateManager m_stateManager; //drag state manager game object here

    public float m_introTimer = 5.0f;
    // Use this for initialization

    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (m_stateManager.m_activeState)
        {
            case GameStates.INTRO:
                //Debug.Log("intro");
                UpdateIntro();
                break;
            case GameStates.MENU:
                break;
            case GameStates.PLAY:
                UpdatePlay();
                break;
            case GameStates.WON:
                break;
            case GameStates.LOST:
                break;
            default:
                Debug.Log("unknown game state");
                break;
        }
    }

    void UpdateIntro()
    {
        if (m_introTimer <= 0.0f)
        {
            Debug.Log("Change to MENU");
            m_stateManager.ChangeState(GameStates.MENU);
        }
        else
        {
            m_introTimer -= Time.deltaTime;
        }
    }

    void UpdatePlay()
    {

    }

    void PlayGame()
    {

    }


}
