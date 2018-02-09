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
    int gameFinished;

    
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

    GameObject m_canvas;

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
            gameFinished = 0;
            Debug.Log("GameManager:is server");
            allyCapshipSS = GameObject.Find("AllyCapShip").GetComponent<ShipScript>();
            Debug.Log(allyCapshipSS);
            enemyCapshipSS = GameObject.Find("EnemyCapShip").GetComponent<ShipScript>();
            enemyCapshipECS = GameObject.Find("EnemyCapShip").GetComponent<EnemyCapshipScript>();

            allyCapShipHP = allyCapshipSS.getAP();
            Debug.Log("ally capship hp:" + allyCapshipSS.getAP());
            enemyCapShipHP = enemyCapshipSS.getAP();

            allyCapShipHPMax = allyCapshipSS.m_maxAP;
            enemyCapShipHPMax = enemyCapshipSS.m_maxAP;

            
        }
        m_canvas = transform.GetChild(0).gameObject;
    }


    void Update()
    {
        if (isServer)
        {
            if (gameFinished == 0){
                allyCapShipHP = allyCapshipSS.getAP();
                
                if (allyCapShipHP <= 0){
                    gameFinished = 1;
                }
            }
            if (gameFinished == 0){
                enemyCapShipHP = enemyCapshipSS.getAP();
                Debug.Log(enemyCapShipHP);
                if (enemyCapShipHP <= 0){
                    gameFinished = 1;
                }
            }
            if (gameFinished == 0)
            {
                Vector3 spawn = allyCapshipSS.transform.position;
                spawn.z = 0;
                allyCapShipPosition = spawn;

                Vector3 spawn2 = enemyCapshipSS.transform.position;
                spawn2.z = 0;
                enemyCapShipPosition = spawn2;
            }
        }

        if (gameFinished == 1){
            m_canvas.SetActive(true);
        }
        //Debug.Log(allyCapShipHP);
    }

    public float GetAllyCapShipHP()
    {
        return allyCapShipHP;
    }
    public float GetEnemyCapShipHP()
    {

        return enemyCapShipHP;
    }

    public float GetAllyCapShipHPMax()
    {

        return allyCapShipHPMax;
    }
    public float GetEnemyCapShipHPMax()
    {

        return enemyCapShipHPMax;
    }

    public Vector3 GetAllyCapShipPosition()
    {
        return allyCapShipPosition;
    }

    public Vector3 GetEnemyCapShipPosition()
    {
        return enemyCapShipPosition;
    }

    public void EnemyShipDestroyed()
    {
        enemyCapshipECS.DestroyedShip();
    }

    public int getGameFinished(){
        return gameFinished;
    }

    
}
