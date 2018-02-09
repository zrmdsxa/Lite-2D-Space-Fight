using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class FireGun : NetworkBehaviour
{
    public GameObject gunbullet;
    public float damageAP = 1f;
    public float damageSP = 0f;
    public float bulletSpeed = 1000.0f;
    public float rateOfFire = 10;
    float cooldown = 0.0f;

    public bool isPlayer = false;
    public bool isAllyAI = false;
    public bool isEnemyAI = false;
    Rigidbody m_rb;

    //PlayerPlayScript m_pps;

    public Transform[] gunpoints;


    // Use this for initialization
    public override void OnStartAuthority()
    {Debug.Log("firegun:isplayer:"+isPlayer);
        if (hasAuthority)
        {
            Debug.Log("Firegun Start()");
            m_rb = GetComponent<Rigidbody>();
            //m_pps = PlayerPlayScript.myPlayer;
            //isPlayer = true;
        }

    }

    [ClientRpc]
    public void RpcStartSetPlayer(){
        isPlayer = true;
        isAllyAI = false;
        isEnemyAI = false;
    }
    public void StartSetAllyAI(){
        isPlayer = false;
        isAllyAI = true;
        isEnemyAI = false;
    }
    public void StartSetEnemyAI(){
        isPlayer = false;
        isAllyAI = false;
        isEnemyAI = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isPlayer+" "+hasAuthority);
        if (isPlayer && hasAuthority)
        {
            if (cooldown < 1 / rateOfFire)
            {
                cooldown += Time.deltaTime;
            }

            if (Input.GetAxis("FireGun") != 0)
            {
                if (cooldown >= 1 / rateOfFire)
                {
                    cooldown = 0.0f;
                    //m_pps.CmdFireGun(t.position,transform.rotation,m_rb.velocity + transform.up * bulletSpeed);
                    
                    Fire();
                    //Debug.Log("fired");
                }
            }

            if (Input.GetAxis("DropBomb") != 0)
            {
                if (cooldown >= 1 / rateOfFire)
                {
                    cooldown = 0.0f;
                    //m_pps.CmdFireGun(t.position,transform.rotation,m_rb.velocity + transform.up * bulletSpeed);
                    
                    DropBomb();
                    //Debug.Log("fired");
                }
            }

        }

    }


    void Fire()
    {
        //Debug.Log("localfiregun");
        Vector3 velocity = m_rb.velocity + (transform.up * bulletSpeed);
        CmdFireGun(velocity);
    }

    void DropBomb()
    {
        //Debug.Log("localfiregun");
        Vector3 velocity = m_rb.velocity + (transform.forward * bulletSpeed);
        CmdFireGun(velocity);
    }

    [Command]
    public void CmdFireGun(Vector3 vel)
    {
        //Debug.Log("cmdfiregun");

        //GameObject b = Instantiate(gunbullet, pos, rot);
        //b.gameObject.GetComponent<AllyBulletScript>().setVelocity(vel);
        Debug.Log(vel);
        RpcCreateBullet(vel);

        //NetworkServer.SpawnWithClientAuthority(b,gameObject);
        //b.GetComponent<AllyBulletScript>().RpcSetVelocity(vel);

    }

    [ClientRpc]
    public void RpcCreateBullet(Vector3 vel)
    {
        //Debug.Log("rpcCreateBullet");
        foreach (Transform t in gunpoints)
        {
            GameObject b = Instantiate(gunbullet, transform.position, transform.rotation);
            b.GetComponent<Rigidbody>().velocity = vel;
            if (tag == "Ally"){
                b.GetComponent<AllyBulletScript>().setDamage(damageAP,damageSP);
            }
            else if (tag == "Enemies"){

            }
            

        }
    }



}


