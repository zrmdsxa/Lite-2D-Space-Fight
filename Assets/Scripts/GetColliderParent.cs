using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetColliderParent : MonoBehaviour
{

    public ShipScript m_shipScript;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + ":collided with:" + other.name);
        Debug.Log(tag + ":" + other.tag);
        if (tag == "Allies")
        {

        }
        else if (tag == "Enemies")
        {
            if (other.tag == "AllyBullets")
            {
                Debug.Log("hit");
				
                m_shipScript.TakeDamage(other.GetComponent<AllyBulletScript>().GetAP(), 0);

                Destroy(other.gameObject);
            }
        }
    }


}
