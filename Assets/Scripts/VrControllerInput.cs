using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class VrControllerInput : MonoBehaviour
{
    [System.Serializable]
    public class InputEvent : UnityEvent<InputEventArgs> { };

    public VrController Controller { get { return controller; } }

    public SteamVR_Action_Boolean grab;
    public SteamVR_Action_Boolean pointer;
    public SteamVR_Action_Boolean use;
    public SteamVR_Action_Boolean teleport;
    public SteamVR_Action_Vector2 touchpadAxis;

    public InputEvent onGrabPressed = new InputEvent();
    public InputEvent onGrabReleased = new InputEvent();

    public InputEvent onPointerPressed = new InputEvent();
    public InputEvent onPointerReleased = new InputEvent();

    public InputEvent onUsePressed = new InputEvent();
    public InputEvent onUseReleased = new InputEvent();

    public InputEvent onTeleportPressed = new InputEvent();
    public InputEvent onTeleportReleased = new InputEvent();

    public InputEvent onTouchpadChanged = new InputEvent();

    public VrController controller;

    public void Setup(VrController _controller)
    {
        controller = _controller;

        grab.AddOnStateDownListener(OnGrabDown, controller.inputSource);
        grab.AddOnStateDownListener(OnGrabUp, controller.inputSource);
        pointer.AddOnStateDownListener(OnPointerDown, controller.inputSource);
        pointer.AddOnStateDownListener(OnPointerUp, controller.inputSource);
        use.AddOnStateDownListener(OnUseDown, controller.inputSource);
        use.AddOnStateDownListener(OnUseUp, controller.inputSource);
        teleport.AddOnStateDownListener(OnTeleportDown, controller.inputSource);
        teleport.AddOnStateDownListener(OnTeleportUp, controller.inputSource);
        touchpadAxis.AddOnChangeListener(OnTouchpadChanged, controller.inputSource);
    }

    private InputEventArgs GenerateArgs()
    {
        return new InputEventArgs(controller, controller.InputSource, touchpadAxis.axis);
    }

    #region Grab
    void OnGrabDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onGrabPressed.Invoke(GenerateArgs());
    }

    void OnGrabUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onGrabPressed.Invoke(GenerateArgs());
    }
    #endregion

    #region Pointer
    void OnPointerDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onPointerPressed.Invoke(GenerateArgs());
    }

    void OnPointerUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onPointerReleased.Invoke(GenerateArgs());
    }
    #endregion

    #region Use
    void OnUseDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onUsePressed.Invoke(GenerateArgs());
    }

    void OnUseUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onUseReleased.Invoke(GenerateArgs());
    }
    #endregion

    #region Teleport
    void OnTeleportDown(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onTeleportPressed.Invoke(GenerateArgs());
    }

    void OnTeleportUp(SteamVR_Action_Boolean _action, SteamVR_Input_Sources _source)
    {
        onTeleportReleased.Invoke(GenerateArgs());
    }
    #endregion

    #region Touchpad
    void OnTouchpadChanged(SteamVR_Action_Vector2 _action, SteamVR_Input_Sources _source, Vector2 _axis, Vector2 _delta)
    {
        onTouchpadChanged.Invoke(GenerateArgs());
    }
    #endregion
}