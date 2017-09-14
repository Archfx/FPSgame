using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraFollow : NetworkBehaviour
{

    // Use this for initialization
    public Transform target;
    public float smoothSpeed=0.125f;
    void LateUpdate()

    {
        if (!isLocalPlayer)
        {
            return;
        }
        transform.position = target.position;
    }
}
