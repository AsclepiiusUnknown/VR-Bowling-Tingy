using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGrabber : MonoBehaviour
{
    public float checkRadius = .5f;
    public LayerMask grabbableLayers;

    public GameObject currentlyGrabbed;
    public bool grabbing = false;

    public Vector3 delta = Vector3.zero;
    private Vector3 lastPos = Vector3.zero;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckGrabber();
        }
        else if (Input.GetMouseButtonUp(0) && grabbing)
        {
            ReleaseObject();
        }

        if (currentlyGrabbed != null && grabbing)
        {
            currentlyGrabbed.transform.position = transform.position;
        }
    }

    void CheckGrabber()
    {
        if (Physics.OverlapSphere(transform.position, checkRadius, grabbableLayers).Length > 0)
        {
            float distance = 1000;
            Collider closest = null;

            foreach (Collider col in Physics.OverlapSphere(transform.position, checkRadius, grabbableLayers))
            {
                if (Vector3.Distance(transform.position, col.transform.position) < distance)
                    closest = col;
            }

            GrabObject(closest.gameObject);
        }
    }

    void GrabObject(GameObject _obj)
    {
        currentlyGrabbed = _obj;
        grabbing = true;
        GetOrAddComponent<Rigidbody>(_obj).useGravity = false;
        if (_obj.GetComponent<Ball>() != null)
        {
            _obj.GetComponent<Ball>().isGrabbed = true;
        }
    }

    public void ReleaseObject()
    {
        GetOrAddComponent<Rigidbody>(currentlyGrabbed).useGravity = true;

        if (currentlyGrabbed.GetComponent<Ball>() != null)
        {
            currentlyGrabbed.GetComponent<Ball>().isGrabbed = false;
        }
        grabbing = false;
        currentlyGrabbed = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    T GetOrAddComponent<T>(GameObject _obj) where T : Component
    {
        T component = _obj.GetComponent<T>();

        if (component == null)
            component = _obj.AddComponent<T>() as T;

        return component;
    }
}