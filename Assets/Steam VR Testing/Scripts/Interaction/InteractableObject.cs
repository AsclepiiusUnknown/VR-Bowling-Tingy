using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class InteractableObject : MonoBehaviour
{
    #region |Variables
    public Rigidbody Rigidbody { get { return rigidbody; } }
    public Collider Collider { get { return collider; } }
    public Transform AttchPoint { get { return attachPoint; } }

    public bool isGrabbable = true;
    public bool isTouchable = false;
    public bool isUseable = false;
    public SteamVR_Input_Sources allowedSource = SteamVR_Input_Sources.Any;

    [Space]

    public Transform attachPoint;

    [Space]

    public InteractionEvent onGrabbed = new InteractionEvent();
    public InteractionEvent onReleased = new InteractionEvent();

    public InteractionEvent onTouched = new InteractionEvent();
    public InteractionEvent onUntouched = new InteractionEvent();

    public InteractionEvent onUsed = new InteractionEvent();
    public InteractionEvent onUnused = new InteractionEvent();

    private new Collider collider;
    private new Rigidbody rigidbody;

    private InteractionEventArgs GenerateArgs(VrController _controller)
    {
        return new InteractionEventArgs(_controller, rigidbody, collider);
    }
    #endregion


    private void Start()
    {
        collider = gameObject.GetComponent<Collider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider>();
            Debug.LogErrorFormat("Object: {0} does not have a collider, adding a BoxCollider...", name);
        }

        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    #region ||Grab
    public void OnObjectGrabbed(VrController _controller)
    {
        if (isGrabbable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
        {
            onGrabbed.Invoke(GenerateArgs(_controller));
        }
    }

    public void OnObjectReleased(VrController _controller)
    {
        if (isGrabbable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
        {
            onReleased.Invoke(GenerateArgs(_controller));
        }
    }
    #endregion

    #region ||Touch
    public void OnObjectTouch(VrController _controller)
    {
        if (isTouchable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
        {
            onTouched.Invoke(GenerateArgs(_controller));
        }
    }

    public void OnObjectUntouched(VrController _controller)
    {
        if (isTouchable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
        {
            onUntouched.Invoke(GenerateArgs(_controller));
        }
    }
    #endregion

    #region ||Use
    public void OnObjectUsed(VrController _controller)
    {
        if (isUseable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
        {
            onUsed.Invoke(GenerateArgs(_controller));
        }
    }

    public void OnObjectUnused(VrController _controller)
    {
        if (isGrabbable && (_controller.InputSource == allowedSource || allowedSource == SteamVR_Input_Sources.Any))
        {
            onUnused.Invoke(GenerateArgs(_controller));
        }
    }
    #endregion
}
