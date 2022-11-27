using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public float X;
    public float Y;
    public float Z;
    void OnTriggerEnter(Collider hit)
    {

        if (hit.gameObject.tag == "Player")
        {
            hit.gameObject.transform.position = new Vector3(X, Y, Z);
        }
    }
}

