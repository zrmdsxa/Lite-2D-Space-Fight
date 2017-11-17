using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UpdateUICapShipHP : NetworkBehaviour
{

    public Image allyCapHP;
    public Image enemyCapHP;
    GameObject allyCapShip;
    GameObject enemyCapShip;

    float allyMaxHP;
    float enemyMaxHP;


    // Use this for initialization
    void Start()
    {
        //allyCapShip = GameObject.Find("AllyCapShip");
        //enemyCapShip = GameObject.Find("EnemyCapShip");
        //Debug.Log("updateuicapship start");
        StartCoroutine(LateStart(0.1f));
        //InvokeRepeating("UpdateHP",0.1f,0.1f);
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        getMaxHP();
    }

    void getMaxHP()
    {
        Debug.Log("updateuicapshiphp/getmaxhp()");
        allyMaxHP = GameManager.instance.GetAllyCapShipHPMax();
        enemyMaxHP = GameManager.instance.GetEnemyCapShipHPMax();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        allyCapHP.fillAmount = GameManager.instance.GetAllyCapShipHP() / allyMaxHP;
		enemyCapHP.fillAmount = GameManager.instance.GetEnemyCapShipHP() / enemyMaxHP;
        //Debug.Log(GameManager.instance.GetAllyCapShipHP());
        //Debug.Log(allyMaxHP);
        //xDebug.Log(GameManager.instance.GetAllyCapShipHP() / allyMaxHP);

    }
}
