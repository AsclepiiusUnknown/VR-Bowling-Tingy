using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(SteamVR_Behaviour_Pose), typeof(VrControllerInput), typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class VrController : MonoBehaviour
{
    public VrControllerInput Input { get { return input; } }

    public Vector3 Velocity { get { return pose.GetVelocity(); } }
    public Vector3 AngularVelocity { get { return pose.GetAngularVelocity(); } }

    public SteamVR_Input_Sources InputSource { get { return inputSource; } }
    public SteamVR_Input_Sources inputSource;

    private SteamVR_Behaviour_Pose pose;
    private VrControllerInput input;

    private new SphereCollider collider;
    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    public void Setup()
    {
        pose = gameObject.GetComponent<SteamVR_Behaviour_Pose>();
        input = gameObject.GetComponent<VrControllerInput>();

        collider = gameObject.GetComponent<SphereCollider>();
        rigidbody = gameObject.GetComponent<Rigidbody>();

        input.Setup(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
