using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VrHelper
{
    private static List<XRDisplaySubsystem> displays = new List<XRDisplaySubsystem>();

    public static void SetEnabled(bool _active)
    {
        displays.Clear();
        SubsystemManager.GetInstances(displays);

        foreach (XRDisplaySubsystem system in displays)
        {
            if (_active)
            {
                system.Start();
            }
            else
            {
                system.Stop();
            }
        }
    }

    public static bool IsEnabled()
    {
        displays.Clear();
        SubsystemManager.GetInstances(displays);

        foreach (XRDisplaySubsystem system in displays)
        {
            if (system.running)
            {
                return true;
            }
        }

        return false;
    }
}
