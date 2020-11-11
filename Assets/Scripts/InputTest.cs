using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    public void OnGrabPressed(InputEventArgs _args)
    {
        GameObject GO = GameObject.CreatePrimitive(PrimitiveType.Cube);

        GO.transform.position = Random.insideUnitSphere * 2;
    }
}
