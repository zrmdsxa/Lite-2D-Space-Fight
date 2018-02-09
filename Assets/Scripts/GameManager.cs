using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{

    ShipScript allyCapshipSS;

    
    ShipScript enemyCapshipSS;
    EnemyCapshipScript enemyCapshipECS;

    //SyncVars automatically updated over network

    [SyncVar]
    float allyCapShipHP;
    [SyncVar]
    float allyCapShipHPMax;
    [SyncVar]
    float enemyCapShipHP;
    [SyncVar]
    float enemyCapShipHPMax;
    [SyncVar]
    Vector3 allyCapShipPosition;

    Vector3 enemyCapShipPosition;



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
            allyCapshipSS = GameObject.Find("AllyCapShip").GetComponent<ShipScript>();
            Debug.Log(allyCapshipSS);
            enemyCapshipSS = GameObject.Find("EnemyCapShip").GetComponent<ShipScript>();
            enemyCapshipECS = GameObject.Find("EnemyCapShip").GetComponent<EnemyCapshipScript>();

            allyCapShipHP = allyCapshipSS.getAP();
            Debug.Log("ally capship hp:"+allyCapshipSS.getAP());
            enemyCapShipHP = enemyCapshipSS.getAP();

            allyCapShipHPMax = allyCapshipSS.m_maxAP;
            enemyCapShipHPMax = enemyCapshipSS.m_maxAP;
        }
    }

    public void ManualStart(){
        
    }



    void Update()
    {
        if (isServer)
        {
            allyCapShipHP = allyCapshipSS.getAP();
            //Debug.Log(allyCapShipScript.transform.position);
            enemyCapShipHP = enemyCapshipSS.getAP();

            Vector3 spawn = allyCapshipSS.transform.position;
            spawn.z = 0;
            allyCapShipPosition = spawn;

            Vector3 spawn2 = enemyCapshipSS.transform.position;
            spawn2.z = 0;
            enemyCapShipPosition = spawn2;
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

    public Vector3 GetAllyCapShipPosition(){
        return allyCapShipPosition;
    }

    public Vector3 GetEnemyCapShipPosition(){
        return enemyCapShipPosition;
    }

    public void EnemyShipDestroyed(){
        enemyCapshipECS.DestroyedShip();
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
