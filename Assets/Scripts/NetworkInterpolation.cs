using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkInterpolation : NetworkBehaviour
{
    [SyncVar]
    Vector3 networkPosition = Vector3.zero;

    [SyncVar]
    Quaternion networkRotation = Quaternion.identity;

    [SyncVar]
    Vector3 networkVelocity = Vector3.zero;


    private float updateInterval = 0.0f;
    public float updatesPerSecond = 20.0f; //doesn't feel any better over 10

    private Rigidbody m_rb;

    public override void OnStartAuthority()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (hasAuthority)
        {

            updateInterval += Time.deltaTime;
            if (updateInterval > 1 / updatesPerSecond)
            {
                updateInterval = 0;
                CmdSync(transform.position, transform.rotation, m_rb.velocity);
            }
        }
        else
        {

            transform.position = Vector3.Lerp(transform.position + (networkVelocity * Time.deltaTime * 1.22f), networkPosition, 0.05f);

            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, 0.1f);
        }

    }

    [Command]
    void CmdSync(Vector3 position, Quaternion rotation, Vector3 velocity)
    {
        networkPosition = position;
        networkRotation = rotation;
        networkVelocity = velocity;
    }
}