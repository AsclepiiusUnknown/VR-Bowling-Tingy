using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InteractionEvent : UnityEvent<InteractionEventArgs> { }

[System.Serializable]
public class InteractionEventArgs
{
    public VrController controller;
    public Rigidbody rb;
    public Collider collider;

    public InteractionEventArgs(VrController _controller, Rigidbody _rb, Collider _col)
    {
        collider = _col;
        controller = _controller;
        rb = _rb;
    }
}
