using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class Pointer : MonoBehaviour
{
    [System.Serializable]
    public class TeleportEvent : UnityEvent<Vector3> { };

    public Vector3 Position { get; private set; } = Vector3.zero;

    [SerializeField]
    private SteamVR_Input_Sources source;
    public LayerMask pointerLayers;
    public float maxPointerLength = 100f;

    [Header("Rendering")]
    public GameObject tracer;
    public bool stretchTracerAlongRay = true;
    public float tracerScaleFactor = .1f;
    public GameObject cursor;
    public float cursorScaleFactor = .25f;

    [Space]
    public Color invalidLocation = Color.red;
    public Color validLocation = Color.green;

    [Space]
    public TeleportEvent onTeleportRequested;

    private VrControllerInput input;
    private bool isPointerActive = false;

    private MeshRenderer tracerRender;
    private MeshRenderer cursorRender;

    bool bail = false;

    private void Start()
    {
        if (input == null)
        {
            bail = true;
            return;
        }

        switch (source)
        {
            case SteamVR_Input_Sources.LeftHand:
                input = VrRig.instance.LeftController.Input;
                break;
            case SteamVR_Input_Sources.RightHand:
                input = VrRig.instance.RightController.Input;
                break;
            default:
                input = VrRig.instance.LeftController.Input;
                break;
        }

        #region Obsolete Approach of Above
        // if (source == SteamVR_Input_Sources.LeftHand)
        // {
        //     input = VrRig.instance.LeftController.Input;
        // }
        // else if (source == SteamVR_Input_Sources.RightHand)
        // {
        //     input = VrRig.instance.RightController.Input;
        // }
        // else
        // {
        //     input = VrRig.instance.LeftController.Input;
        // }
        #endregion

        input.onPointerPressed.AddListener(OnPointerActivate);
        input.onPointerReleased.AddListener(OnPointerUnactivate);
        input.onTeleportPressed.AddListener(OnTeleportPressed);

        tracer.transform.parent = transform;
        cursor.transform.parent = transform;

        tracer.SetActive(false);
        cursor.SetActive(false);

        tracerRender = tracer.GetComponent<MeshRenderer>();
        cursorRender = cursor.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (bail)
            return;

        transform.rotation = input.transform.rotation;
        transform.position = input.transform.position;

        if (isPointerActive)
        {
            if (Physics.Raycast(input.transform.position, input.transform.forward, out RaycastHit hit, maxPointerLength, pointerLayers))
            {
                Position = hit.point;

                Vector3 midpoint = Vector3.Lerp(transform.position, hit.point, .5f);
                tracer.transform.position = midpoint;
                tracer.transform.rotation = Quaternion.LookRotation(transform.forward);
                tracer.transform.localScale = new Vector3(tracerScaleFactor, tracerScaleFactor, hit.distance);

                cursor.transform.position = hit.point;
                cursor.transform.localScale = Vector3.one * cursorScaleFactor;

                tracerRender.material.color = validLocation;
                cursorRender.material.color = validLocation;
            }
            else
            {
                Position = Vector3.zero;

                tracer.transform.position = transform.position + transform.forward * (maxPointerLength * .5f);
                tracer.transform.rotation = Quaternion.LookRotation(transform.forward);
                tracer.transform.localScale = new Vector3(tracerScaleFactor, tracerScaleFactor, maxPointerLength);

                cursor.transform.position = transform.position + transform.forward * maxPointerLength;
                cursor.transform.localScale = Vector3.one * cursorScaleFactor;

                tracerRender.material.color = invalidLocation;
                cursorRender.material.color = invalidLocation;
            }
        }
    }

    void OnPointerActivate(InputEventArgs _args)
    {
        isPointerActive = true;
        tracer.SetActive(true);
        cursor.SetActive(true);
    }

    void OnPointerUnactivate(InputEventArgs _args)
    {
        isPointerActive = false;
        tracer.SetActive(false);
        cursor.SetActive(false);
    }

    void OnTeleportPressed(InputEventArgs _args)
    {
        if (isPointerActive && Position != Vector3.zero)
        {
            onTeleportRequested.Invoke(Position);
        }
    }
}