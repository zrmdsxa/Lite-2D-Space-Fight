using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AllyBulletScript : NetworkBehaviour
{

    //Rigidbody m_rb;
    public float lifeTime = 2.0f;

    float damageAP = 0;
    float damageSP = 0;
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

    public void setDamage(float ap,float sp){
        damageAP = ap;
        damageSP = sp;
        //Debug.Log("Damage set:"+ap+"/"+sp);
    }

    public float GetAP(){
        return damageAP;
    }
    public float GetSP(){
        return damageSP;
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Enemies"){
            
        }
    }
}
