using UnityEngine;

public class EnemyRadarDetector : MonoBehaviour
{
    [SerializeField]
    float DetectionRange = 3;

    [SerializeField]
    LayerMask WhatIsVisible;

    Transform _player;

    PlayerMovement _playerMovement;

    public bool isDetectingPlayer;
    private bool playerHasBeenDetected;

    //public static Action DetectedThePlayer;
    //public static Action ContinuePatrolling;
    private EnemyPatroling _patroling;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.white;
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerMovement = _player.GetComponent<PlayerMovement>();

        _patroling = GetComponent<EnemyPatroling>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInRange() && (!playerHasBeenDetected))
        {
            if (IsNotBlocked())
            {
                //DetectedThePlayer?.Invoke();
                _patroling.DetectedThePlayer(_player);
                playerHasBeenDetected = true;

                //_playerMovement.GotCaught();
            }
        }
        if (!IsInRange() || !IsNotBlocked())
        {
            //ContinuePatrolling?.Invoke();
            if (_patroling.pauseBeforeFlip >= 3 || _patroling.pauseBeforeFlip == 0)
            {
                _patroling.ContinuePatrolling();
            }
        }

    }



    private bool IsNotBlocked()
    {
        if (IsBlocked(_player))
        {
            isDetectingPlayer = false;
        }
        else
        {
            isDetectingPlayer = true;
        }

        return isDetectingPlayer;
    }

    private bool IsBlocked(Transform player)
    {
        Vector2 v2 = player.position - transform.position;
        var hit = Physics2D.Raycast(transform.position, v2, DetectionRange, WhatIsVisible);
        return hit.transform != player;
    }

    private bool IsInRange()
    {

        if (Vector2.Distance(_player.position, transform.position) < DetectionRange)
        {
            isDetectingPlayer = true;
        }
        else
        {
            isDetectingPlayer = false;
        }

        return isDetectingPlayer;
    }
}
