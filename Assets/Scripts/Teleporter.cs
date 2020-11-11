using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private void Start()
    {
        Pointer[] pointers = FindObjectsOfType<Pointer>();

        foreach (Pointer pointer in pointers)
        {
            pointer.onTeleportRequested.AddListener(OnTeleportRequest);
        }
    }

    void OnTeleportRequest(Vector3 _position)
    {
        transform.position = _position;
    }
}
