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
        PlayerInputs.CheckEnemyDistance += CheckIfEnemyClose;
    }

    private void OnDisable()
    {
        PlayerInputs.CheckEnemyDistance -= CheckIfEnemyClose;
    }

    private bool CheckIfEnemyClose()
    {
        //AHORA LA DIRECCIÓN NO ESTÁ FLIP, SOLO SEÑALA A LA DERECHA
        var hit = new RaycastHit2D();

        if (gameObject.transform.localPosition.x > 0)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right, detectDistance);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, Vector2.left, detectDistance);
        }

        if (hit.transform != null)
        {
            if (hit.transform.gameObject.tag == "BaseEnemy")
            {
                enemyTransform = hit.transform;
                return true;
            }
        }
        return false;
    }
}
