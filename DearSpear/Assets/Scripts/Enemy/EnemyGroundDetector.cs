using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundDetector : MonoBehaviour
{
    public bool NotGround => _notGround;
    private bool _notGround;

    [SerializeField]
    float DetectionDistance = 1.5f;

    [SerializeField]
    LayerMask WhatIsGround;

    void Update()
    {
        DetectGround();
    }

    private void DetectGround()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, DetectionDistance, WhatIsGround);

        _notGround = (hit.collider == null);
    }
}
