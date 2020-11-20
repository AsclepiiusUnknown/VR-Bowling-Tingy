using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    [HideInInspector]
    public bool isGrabbed = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ThrowBall(float _force, Vector3 _dir)
    {
        FindObjectOfType<PCGrabber>().ReleaseObject();
        Vector3 playerPos = GameObject.FindGameObjectWithTag("PC Player").transform.forward;
        playerPos = transform.TransformPoint(playerPos);
        rb.AddForce(_dir * _force);
        print(_force);
    }
}
