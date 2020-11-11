using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VrControllerInput))]
public class InteractGrab : MonoBehaviour
{
    private VrControllerInput input;

    public InteractionEvent grabbed = new InteractionEvent();
    public InteractionEvent ungrabbed = new InteractionEvent();

    private InteractableObject collidingObject;
    private InteractableObject heldObject;


    private void Start()
    {
        input = gameObject.GetComponent<VrControllerInput>();

        input.onGrabPressed.AddListener(OnGrabPressed);
        input.onGrabReleased.AddListener(OnGrabReleased);
    }

    void OnGrabPressed(InputEventArgs _args)
    {
        if (collidingObject != null)
        {
            GrabObject();
        }
    }

    void OnGrabReleased(InputEventArgs _args)
    {
        if (heldObject != null)
        {
            UngrabObject();
        }
    }

    void SetCollidingObject(Collider _collider)
    {
        InteractableObject interactable = _collider.GetComponent<InteractableObject>();

        if (collidingObject != null || interactable == null)
        {
            return;
        }

        collidingObject = interactable;
    }

    private void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (collidingObject == other.GetComponent<InteractableObject>())
        {
            collidingObject = null;
        }
    }

    void GrabObject()
    {
        heldObject = collidingObject;
        collidingObject = null;

        FixedJoint joint = AddJoint();
        joint.connectedBody = heldObject.Rigidbody;

        if (heldObject.AttchPoint != null)
        {
            heldObject.transform.position = transform.position - (heldObject.AttchPoint.position - heldObject.transform.position);
            heldObject.transform.rotation = transform.rotation * Quaternion.Euler(heldObject.AttchPoint.localEulerAngles);
        }
        else
        {
            heldObject.transform.position = transform.position;
        }

        grabbed.Invoke(new InteractionEventArgs(input.Controller, heldObject.Rigidbody, heldObject.Collider));
        heldObject.OnObjectGrabbed(input.Controller);
    }

    void UngrabObject()
    {
        FixedJoint joint = gameObject.GetComponent<FixedJoint>();
        if (joint != null)
        {
            joint.connectedBody = null;
            Destroy(joint);

            heldObject.Rigidbody.velocity = input.Controller.Velocity;
            heldObject.Rigidbody.angularVelocity = input.Controller.AngularVelocity;
        }

        ungrabbed.Invoke(new InteractionEventArgs(input.Controller, heldObject.Rigidbody, heldObject.Collider));
        heldObject.OnObjectReleased(input.Controller);
        heldObject = null;
    }

    FixedJoint AddJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
}
