using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AllyBulletScript : NetworkBehaviour
{

    //Rigidbody m_rb;
    public float lifeTime = 2.0f;
    //float time = 0.0f;

    // Use this for initialization
    /*
    void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }
    */
    void Start(){
        Destroy(gameObject,lifeTime);
    }

    // Update is called once per frame
     /*
    void Update()
    {
        //Debug.Log("bullet authority:"+hasAuthority);  has authority
       
        time += Time.deltaTime;
        if (time >= lifeTime)
        {
            Destroy(gameObject);
        }
        
    }
    */
/*
    public void setVelocity(Vector3 vel)
    {
        m_rb.velocity = vel;
    }
    */
}
