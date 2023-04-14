using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRope : MonoBehaviour
{
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;

    private Vector2 nearestGrabPointPos;

    // Start is called before the first frame update
    void Start()
    {
        _distanceJoint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _lineRenderer.SetPosition(0, nearestGrabPointPos);
            _lineRenderer.SetPosition(1, transform.position);
            _distanceJoint.connectedAnchor = nearestGrabPointPos;
            _distanceJoint.enabled = true;
            _lineRenderer.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _distanceJoint.enabled = false;
            _lineRenderer.enabled = false;
        }
        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
    }
}
