using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyCapshipScript : NetworkBehaviour
{

    public ShipScript m_shipScript;

    public GameObject[] spawnableShips;

    public int maxShips = 50;

    int currentNumShips = 0;
    public float respawnTime = 30.0f;
    float currentRespawnTime = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            //Debug.Log(m_shipScript.getAP()); working
            if (m_shipScript.getAP() > 0.0f)
            {
                //Debug.Log(currentRespawnTime); working
                if (currentRespawnTime > 0.0f)
                {
                    currentRespawnTime -= Time.deltaTime;
                }
                else
                {
                    if (currentNumShips < maxShips)
                    {
                        currentRespawnTime = respawnTime;
                        currentNumShips++;
                        SpawnShip();
                    }
                }
            }
        }
    }

    void SpawnShip()
    {
        PlayerPlayScript.myPlayer.CmdSpawnEnemyFighterTurn();
    }

    public void DestroyedShip(){

    }
}
