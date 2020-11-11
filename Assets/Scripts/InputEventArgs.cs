using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InputEventArgs
{
    /// <summary>
    /// The controller firing the event
    /// </summary>
    public VrController controller;

    public SteamVR_Input_Sources source;

    public Vector2 touchpadAxis;

    public InputEventArgs(VrController _controller, SteamVR_Input_Sources _source, Vector2 _touchpadAxis)
    {
        controller = _controller;
        source = _source;
        touchpadAxis = _touchpadAxis;
    }
}
