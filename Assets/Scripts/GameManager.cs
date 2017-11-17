using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{

    public ShipScript allyCapShipScript;
    public ShipScript enemyCapShipScript;

    //SyncVars automatically updated over network

    [SyncVar]
    float allyCapShipHP;
    [SyncVar]
    float allyCapShipHPMax;
    [SyncVar]
    float enemyCapShipHP;
    [SyncVar]
    float enemyCapShipHPMax;



    public static GameManager instance;
    //public StateManager m_stateManager; //drag state manager game object here
    /*
    public float m_introTimer = 5.0f;
    // Use this for initialization

    */
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        Debug.Log("GameManager:start");
        if (isServer)
        {
            Debug.Log("GameManager:is server");
            allyCapShipScript = GameObject.Find("AllyCapShip").GetComponent<ShipScript>();
            Debug.Log(allyCapShipScript);
            enemyCapShipScript = GameObject.Find("EnemyCapShip").GetComponent<ShipScript>();

            allyCapShipHP = allyCapShipScript.getAP();
            Debug.Log("ally capship hp:"+allyCapShipScript.getAP());
            enemyCapShipHP = enemyCapShipScript.getAP();

            allyCapShipHPMax = allyCapShipScript.m_maxAP;
            enemyCapShipHPMax = enemyCapShipScript.m_maxAP;
        }
    }

    public void ManualStart(){
        
    }



    void Update()
    {
        if (isServer)
        {
            allyCapShipHP = allyCapShipScript.getAP();
            //Debug.Log(allyCapShipScript.transform.position);
            enemyCapShipHP = enemyCapShipScript.getAP();
        }
        //Debug.Log(allyCapShipHP);
    }

    public float GetAllyCapShipHP()
    {
        return allyCapShipHP;
    }
    public float GetEnemyCapShipHP()
    {

        return allyCapShipHP;
    }

    public float GetAllyCapShipHPMax()
    {

        return allyCapShipHPMax;
    }
    public float GetEnemyCapShipHPMax()
    {

        return enemyCapShipHPMax;
    }

    /*
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

*/
}
