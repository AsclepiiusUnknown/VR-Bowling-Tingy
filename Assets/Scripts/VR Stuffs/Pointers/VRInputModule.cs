using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.EventSystems;

public class VRInputModule : BaseInputModule
{
    public Camera camera;
    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Boolean clickAction;
    public Canvas canvas;

    private GameObject currentObject;
    private PointerEventData data;

    protected override void Awake()
    {
        base.Awake();

        data = new PointerEventData(eventSystem);

        if (canvas != null)
            if (VrHelper.VRIsPresent())
                canvas.worldCamera = camera;
    }

    public override void Process()
    {
        data.Reset();
        data.position = new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2);

        eventSystem.RaycastAll(data, m_RaycastResultCache);
        data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObject = data.pointerCurrentRaycast.gameObject;

        m_RaycastResultCache.Clear();

        HandlePointerExitAndEnter(data, currentObject);

        if (clickAction.GetStateDown(targetSource))
            ProcessRelease(data);
    }

    public PointerEventData GetData()
    {
        return data;
    }

    private void ProcessPress(PointerEventData _data)
    {
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);

        if (newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = currentObject;
    }

    private void ProcessRelease(PointerEventData _data)
    {
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        if(data.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        eventSystem.SetSelectedGameObject(null);

        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }
}
