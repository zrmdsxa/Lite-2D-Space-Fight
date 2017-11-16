﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerPlayScript : NetworkBehaviour
{
    public Text speedText;
    public GameObject gameManager;

    public GameObject zlcarrier1;
    public GameObject enemycap1;
    public GameObject z7interceptor;

	public GameObject gunbullet;

	public static PlayerPlayScript myPlayer;

    public GameObject enemyTest;

    



    // Use this for initialization
    void Start()
    {
        Debug.Log("PlayerScript:start");
        //Debug.Log(connectionToClient);
        if (isServer){
            GameObject gm = Instantiate(gameManager);
            GameObject go1 = Instantiate(zlcarrier1);
            GameObject go2 = Instantiate(enemycap1);
            //GameObject gm = Instantiate(gameManager);
            NetworkServer.SpawnWithClientAuthority(gm, gameObject);
            NetworkServer.SpawnWithClientAuthority(go1, gameObject);
            NetworkServer.SpawnWithClientAuthority(go2, gameObject);

           //GameManager.instance.ManualStart();
        }
        if (isLocalPlayer)
        {
            if (myPlayer == null)
            {
                myPlayer = this;
            }
            else if (myPlayer != this)
            {
                Destroy(gameObject);
            }
            transform.GetChild(0).gameObject.SetActive(true);   //spawn canvas
            transform.GetChild(1).gameObject.SetActive(true);   //camera
            transform.GetChild(2).gameObject.SetActive(false);  //speed text
            transform.GetChild(2).gameObject.SetActive(true);   ///cap ship hp
            

        }
        //Debug.Log("player :" + connectionToClient);
    }

    void LateUpdate(){

    }

    void ShipSelected()
    {
        transform.GetChild(0).gameObject.SetActive(false);  //canvas
        //transform.GetChild(1).gameObject.SetActive(false);  //camera
        transform.GetChild(2).gameObject.SetActive(true); // speed text

    }

    public void UpdateSpeed(float speed){
        speedText.text = speed.ToString("0") + " m/s";
    }

    public void SpawnZ7Interceptor()
    {


        if (isLocalPlayer)
        {
            ShipSelected();
            CmdSpawnZ7Interceptor();
            
        }

    }

    [Command]

    public void CmdSpawnZ7Interceptor()
    {
        Debug.Log("spawn z7");
        GameObject go = Instantiate(z7interceptor);
        go.GetComponent<ShipScript>().isPlayer = true;
        go.GetComponent<FireGun>().isPlayer = true;
        //go.GetComponent<FireGun>().isPlayer = true;
        NetworkServer.SpawnWithClientAuthority(go, gameObject);

        //to be used for spawning enemies
        /*
        if (isServer){
            GameObject e = Instantiate(enemyTest);
            
            NetworkServer.SpawnWithClientAuthority(e, gameObject);
        }
        */

    }
/* 
    [Command]
    public void CmdFireGun(Vector3 pos, Quaternion rot, Vector3 vel)
    {
        Debug.Log("fire gun");
		GameObject b = Instantiate(gunbullet, pos, rot);
		b.gameObject.GetComponent<AllyBulletScript>().setVelocity(vel);


        //NetworkServer.SpawnWithClientAuthority(b,gameObject);
		//b.GetComponent<AllyBulletScript>().RpcSetVelocity(vel);
    }
*/

}
