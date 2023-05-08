using UnityEngine;

public class PlayerRope : MonoBehaviour
{
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;

    private Vector2 nearestGrabPointPos;

    private bool _isHooked;

    // Start is called before the first frame update
    void Start()
    {
        _distanceJoint.enabled = false;
    }


    private void OnEnable()
    {
        PlayerInputs.SetRope += SetRope;
        PlayerInputs.EndRope += EndRope;

        PlayerMovement.CheckHook += CheckHook;
    }

    private void OnDisable()
    {
        PlayerInputs.SetRope -= SetRope;
        PlayerInputs.EndRope -= EndRope;

        PlayerMovement.CheckHook -= CheckHook;
    }

    private void Update()
    {
        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
    }

    private void SetRope()
    {
        nearestGrabPointPos = SetPoint();
        if (nearestGrabPointPos != Vector2.zero)
        {
            _lineRenderer.SetPosition(0, nearestGrabPointPos);
            _lineRenderer.SetPosition(1, transform.position);
            _distanceJoint.connectedAnchor = nearestGrabPointPos;
            _distanceJoint.enabled = true;
            _lineRenderer.enabled = true;

            _isHooked = true;
        }
    }
    private void EndRope()
    {
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;

        _isHooked = false;
    }

    private Vector2 SetPoint()
    {
        GameObject[] pointsList = GameObject.FindGameObjectsWithTag("GrabPoint");
        GameObject nearestPoint = null;
        foreach (GameObject point in pointsList)
        {
            if (point.GetComponent<GrabPointPlayerDetector>().isDetecting)
            {
                nearestPoint = point;
            }

            // de momento solo checkea si lo está detectando cerca
            // si hubiera algun momento que se solapan areas, solo hace
            // falta hacer que coja siempre el que tiene mas cerca
        }
        if (nearestPoint != null)
        {
            Vector2 playerToPointDirection = (nearestPoint.transform.position - gameObject.transform.position).normalized;

            RaycastHit2D ray = Physics2D.Raycast((Vector2)gameObject.transform.position + playerToPointDirection, playerToPointDirection);
            // añado el modulo de la dirección a la posición de origen para que el raycast no colisione con el propio player

            if (ray.collider == nearestPoint.GetComponent<Collider2D>())
            {
                return nearestPoint.transform.position;
            }
        }
        return Vector2.zero;

    }

    private bool CheckHook()
    {
        return _isHooked;
    }
}
