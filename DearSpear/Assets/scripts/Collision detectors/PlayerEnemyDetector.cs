using UnityEngine;

public class PlayerEnemyDetector : MonoBehaviour
{
    public float _detectDistance => detectDistance;
    private float detectDistance = 1f;

    private Transform enemyTransform;
    public Transform _enemyTransform
    {
        get { return enemyTransform; }
    }

    private void OnEnable()
    {
        PlayerInput.CheckEnemyDistance += CheckIfEnemyClose;
    }

    private void OnDisable()
    {
        PlayerInput.CheckEnemyDistance -= CheckIfEnemyClose;
    }

    private bool CheckIfEnemyClose()
    {
        //AHORA LA DIRECCI�N NO EST� FLIP, SOLO SE�ALA A LA DERECHA
        var hit = Physics2D.Raycast(transform.position, Vector2.right, detectDistance);
        if (hit.transform != null)
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                enemyTransform = hit.transform;
                return true;
            }
        }
        return false;
    }
}
