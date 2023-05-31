using System;
using UnityEngine;

public class SpearLauncher : MonoBehaviour
{
    private Vector2 spearStartPosition;
    private Vector2 lastAimedPosition;
    private Vector2 aimPosition;

    private Vector2 direction;

    public LayerMask layerMask;

    [SerializeField]
    private GameObject spear;
    [SerializeField]
    private float launchForce;
    [SerializeField]
    private Transform launchPoint;

    public bool spearActive = true;

    private bool isAiming = false;

    private PlayerInputs _playerInput;

    private void OnEnable()
    {
        PlayerInputs.LaunchSpear += LaunchSpear;
        spearCollisionDetector.SpearGrabbed += SpearGrabbed;
        Spear.SpearGrabbed += SpearGrabbed;
        LevelManager.SpearGrab += SpearGrabbed;

        PlayerInputs.ErasePoints += ErasePoints;
        PlayerInputs.IsAiming += IsAiming;
        PlayerInputs.CheckPlayerHasSpear += CheckPlayerHasSpear;
    }

    private void OnDisable()
    {
        PlayerInputs.LaunchSpear -= LaunchSpear;
        spearCollisionDetector.SpearGrabbed -= SpearGrabbed;
        Spear.SpearGrabbed -= SpearGrabbed;
        LevelManager.SpearGrab -= SpearGrabbed;

        PlayerInputs.ErasePoints -= ErasePoints;
        PlayerInputs.IsAiming -= IsAiming;
        PlayerInputs.CheckPlayerHasSpear -= CheckPlayerHasSpear;
    }

    [SerializeField]
    private GameObject point;
    private GameObject[] points;
    [SerializeField]
    private int numberOfPoints;
    [SerializeField]
    private float spaceBetweenPoints;

    private void Awake()
    {
        _playerInput = GetComponentInParent<PlayerInputs>();

        points = new GameObject[numberOfPoints];
        for (int index = 0; index < numberOfPoints; index++)
        {
            points[index] = Instantiate(point, launchPoint.position, Quaternion.identity);
            points[index].SetActive(false);
        }
    }

    void Update()
    {
        GetSpearDirection();
        if (spearActive && isAiming)
        {
            DrawPoints();
        }
    }

    public void DrawPoints()
    {
        for (int index = 0; index < numberOfPoints; index++)
        {
            points[index].transform.position = PointPosition(index * spaceBetweenPoints);
            points[index].SetActive(true);
        }
    }

    public void ErasePoints()
    {
        isAiming = false;
        for (int index = 0; index < numberOfPoints; index++)
        {
            if (points[index] != null)
            {
                points[index].transform.position = PointPosition(index * spaceBetweenPoints);
                points[index].SetActive(false);
            }
        }
    }

    private void IsAiming()
    {
        isAiming = true;
    }

    private void GetSpearDirection()
    {

        spearStartPosition = transform.position;

        if(_playerInput.AimSpearPosition != (Vector2)transform.position)
            lastAimedPosition = _playerInput.AimSpearPosition;

        aimPosition = _playerInput.AimSpearPosition;
        if (aimPosition == (Vector2)transform.position)
            aimPosition = lastAimedPosition;

        direction = aimPosition - spearStartPosition;
        transform.right = direction;
    }

    public void LaunchSpear()
    {
        if (spearActive && CheckDistanceFromCollision())
        {
            GameObject newSpear = Instantiate(spear, launchPoint.position, launchPoint.rotation);
            newSpear.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
            spearActive = false;
        }
    }

    private bool CheckDistanceFromCollision()
    {
        var hit = new RaycastHit2D();
        hit = Physics2D.Raycast(spearStartPosition + (direction.normalized * 0.9f), direction.normalized, 100f, layerMask);
        return ((hit.distance > 2f) || (hit.distance == 0 && direction.normalized.y > 0));
    }

    private Vector2 PointPosition(float t)
    {
        Vector2 pointPosition = (Vector2)launchPoint.position + (direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return pointPosition;
    }

    public void SpearGrabbed()
    {
        if (!spearActive)
        {
            spearActive = true;
            Destroy(GameObject.FindGameObjectWithTag("Spear"));
            Destroy(GameObject.FindGameObjectWithTag("SpearInGround"));
        }
    }

    private bool CheckPlayerHasSpear()
    {
        return spearActive;
    }
}