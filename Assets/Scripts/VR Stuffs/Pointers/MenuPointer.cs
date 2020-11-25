using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuPointer : MonoBehaviour
{
    public float defaultLength = 5;
    public GameObject dot;
    public VRInputModule inputModule;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLine();
    }

    void UpdateLine()
    {
        //Use default or distance
        PointerEventData data = inputModule.GetData();
        float targetLength = (data.pointerCurrentRaycast.distance == 0) ? defaultLength : data.pointerCurrentRaycast.distance;

        //Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        //Default
        Vector3 endPos = transform.position + (transform.forward * targetLength);

        //Or based on hit
        if (hit.collider != null)
            endPos = hit.point;

        //Set Position of the dot
        dot.transform.position = endPos;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPos);
    }

    private RaycastHit CreateRaycast(float _length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, 5);

        return hit;
    }
}
