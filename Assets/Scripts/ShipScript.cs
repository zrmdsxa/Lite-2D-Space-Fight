using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//GameObject 
//Pivot
//Model
public class ShipScript : NetworkBehaviour
{



    public float m_ap = 100.0f;	//armor
    public float m_sp = 100.0f;	//shield

    public float m_minSpeed = 100.0f;
    public float m_maxSpeed = 100.0f; //speed in meters per second

    //float m_speedScale = 63.0f;	//magic number

    public float m_acceleration = 1.0f;

    public float m_thrust = 1.0f;

    public bool isPlayer = false;

    private float m_minThrust; // minspeed / maxspeed

    public float m_turnRate = 35.0f;    //turn degrees per second

    private float m_rollRate;   //rolling aesthetics
                                //turnRate * 2

    private Rigidbody m_rb;
    private Transform m_ship; //This is actually the pivot

    private Quaternion m_defaultRotation;	//default local rotation of a ship facing upwards x:-90 only


    private float m_turn;



    // Use this for initialization

    public override void OnStartAuthority()
    {
        //Debug.Log("is local: "+isLocalPlayer+" has authority: "+hasAuthority);
        //Debug.Log("owner:"+GetComponent<NetworkIdentity>().clientAuthorityOwner);
        if (hasAuthority)
        {
            
            m_rb = GetComponent<Rigidbody>();
            m_ship = transform.GetChild(0);
            m_defaultRotation = m_ship.localRotation;
            m_rollRate = m_turnRate * 2.0f;
            m_minThrust = m_minSpeed / m_maxSpeed;

            transform.GetChild(1).gameObject.SetActive(true);
            isPlayer = true;
        }

    }

    void Start()
    {
        //Debug.Log("is local: "+isLocalPlayer+" has authority: "+hasAuthority);
        //Debug.Log("owner:"+GetComponent<NetworkIdentity>().clientAuthorityOwner);
        if (hasAuthority)
        {
            
            m_rb = GetComponent<Rigidbody>();
            m_ship = transform.GetChild(0);
            m_defaultRotation = m_ship.localRotation;
            m_rollRate = m_turnRate * 2.0f;
            m_minThrust = m_minSpeed / m_maxSpeed;

            transform.GetChild(1).gameObject.SetActive(true);
            isPlayer = true;
        }

    }

   

    /*
        void FixedUpdate()
        {

        }
    */
    // Update is called once per frame

    void Update()
    {
       //Debug.Log("update has authority:"+hasAuthority+" localplayer:"+isLocalPlayer);
        if (hasAuthority)
        {
            if (isPlayer)
            {
                TurnShip();
                UpdateVelocity();
            }
        }

    }

    void UpdateVelocity()
    {
        m_thrust += Input.GetAxis("Vertical") * m_acceleration * Time.deltaTime * 0.1f;

        m_thrust = Mathf.Clamp(m_thrust, m_minThrust, 1.0f);

        m_rb.velocity = transform.up * m_maxSpeed * m_thrust;

        //Debug.Log(m_rb.velocity.magnitude);
        //m_rb.velocity = transform.up * m_maxSpeed * m_speedScale * Time.deltaTime;
    }

    void TurnShip()
    {

        m_turn += Input.GetAxis("Horizontal") * m_turnRate * Time.deltaTime;

        m_turn = Mathf.Clamp(m_turn, -m_turnRate, m_turnRate);
        //Debug.Log(m_turn);
        //Turn ship left/right
        //transform.Rotate(0, 0, Input.GetAxis("Horizontal") * m_turnRate * Time.deltaTime * -1);
        transform.Rotate(0, 0, m_turn * Time.deltaTime * -1);

        Quaternion rot = m_ship.localRotation;


        //Not turning, unroll
        if (Input.GetAxis("Horizontal") == 0.0f)

        {
            //m_ship.localRotation = Quaternion.RotateTowards(rot, m_defaultRotation, (m_rollRate/2) * Time.deltaTime);
            m_ship.localRotation = Quaternion.RotateTowards(rot, m_defaultRotation, (m_rollRate - ((m_rollRate * 0.90f) - Mathf.Abs(m_turn))) * Time.deltaTime);
        }
        //roll the ship
        else
        {
            m_ship.transform.Rotate(0, 0, Input.GetAxis("Horizontal") * m_rollRate * Time.deltaTime * -1);

        }
        //Debug.Log(m_ship.transform.localRotation.eulerAngles.y);
        //max roll 90 degrees
        //change the y even though it shows up as z in the editor
        if (m_ship.transform.localRotation.eulerAngles.y > 90 && m_ship.transform.localRotation.eulerAngles.y < 180)
        {
            m_ship.transform.localRotation = Quaternion.Euler(m_ship.transform.localRotation.eulerAngles.x, 90, m_ship.transform.localRotation.eulerAngles.z);
        }
        else if (m_ship.transform.localRotation.eulerAngles.y > 180 && m_ship.transform.localRotation.eulerAngles.y < 270)
        {
            m_ship.transform.localRotation = Quaternion.Euler(m_ship.transform.localRotation.eulerAngles.x, 270, m_ship.transform.localRotation.eulerAngles.z);
        }

        m_turn = Mathf.Lerp(m_turn, 0, Time.deltaTime);

    }

}
