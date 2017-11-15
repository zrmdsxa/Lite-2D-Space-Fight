using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class FireGun : NetworkBehaviour
{
    public GameObject gunbullet;
    public float bulletSpeed = 1000.0f;
    public float rateOfFire = 10;
    float cooldown = 0.0f;

    public bool isPlayer = false;
    Rigidbody m_rb;

    //PlayerPlayScript m_pps;

    public Transform[] gunpoints;


    // Use this for initialization
    public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            Debug.Log("Firegun Start()");
            m_rb = GetComponent<Rigidbody>();
            //m_pps = PlayerPlayScript.myPlayer;
            //isPlayer = true;
        }

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

        }

    }


    void Fire()
    {
        Vector3 velocity = m_rb.velocity + (transform.up * bulletSpeed);
        CmdFireGun(velocity);
    }

    [Command]
    public void CmdFireGun(Vector3 vel)
    {
        Debug.Log("fire gun");
        //GameObject b = Instantiate(gunbullet, pos, rot);
        //b.gameObject.GetComponent<AllyBulletScript>().setVelocity(vel);
        ;
        RpcCreateBullet(vel);

        //NetworkServer.SpawnWithClientAuthority(b,gameObject);
        //b.GetComponent<AllyBulletScript>().RpcSetVelocity(vel);

    }

    [ClientRpc]
    public void RpcCreateBullet(Vector3 vel)
    {

        foreach (Transform t in gunpoints)
        {
            GameObject b = Instantiate(gunbullet, transform.position, transform.rotation);
            b.GetComponent<Rigidbody>().velocity = vel;
        }
    }



}


