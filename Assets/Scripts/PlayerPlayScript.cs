using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerPlayScript : NetworkBehaviour
{
    public GameObject z7interceptor;

	public GameObject gunbullet;

	public static PlayerPlayScript myPlayer;

    void Awake()
    {

    }


    // Use this for initialization
    void Start()
    {
        Debug.Log(connectionToClient);
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
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);

        }
        //Debug.Log("player :" + connectionToClient);
    }

    void HideUI()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void SpawnZ7Interceptor()
    {


        if (isLocalPlayer)
        {
            HideUI();
            CmdSpawnZ7Interceptor();
        }


    }

    [Command]

    public void CmdSpawnZ7Interceptor()
    {
        Debug.Log("spawn z7");
        GameObject go = Instantiate(z7interceptor);
        go.GetComponent<FireGun>().isPlayer = true;
        NetworkServer.SpawnWithClientAuthority(go, gameObject);

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
