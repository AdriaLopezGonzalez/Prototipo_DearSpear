using UnityEngine;

public class TribeRescueDetector : MonoBehaviour
{
    private Collider2D _collider;

    private bool theyLeaving;

    //private Transform tribe1;
    //private Transform tribe2;
    //private Transform tribe3;
    private Transform bush;
    private Transform[] tribesPeople;
    private Animator[] tribesAnim;

    public float tribeSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        theyLeaving = false;

        //tribe1 = transform.GetChild(0);
        //tribe2 = transform.GetChild(1);
        //tribe3 = transform.GetChild(2);
        tribesPeople = new Transform[transform.childCount - 1];
        tribesAnim = new Animator[transform.childCount - 1];

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            tribesPeople[i] = transform.GetChild(i);
            tribesAnim[i] = tribesPeople[i].GetComponent<Animator>();
        }

        bush = transform.GetChild(transform.childCount - 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _collider.enabled = false;
            theyLeaving = true;
            foreach (Animator _an in tribesAnim)
            {
                _an.SetBool("Rescued", true);
            }
        }
    }

    private void Update()
    {
        if (theyLeaving)
        {
            GetAway();
        }
    }

    private void GetAway()
    {
        foreach (Transform tribe in tribesPeople)
        {
            if (tribe != null)
            {

                tribe.position -= new Vector3(tribeSpeed * Time.deltaTime, 0, 0);

                if (tribe.position.x > bush.position.x - 0.25 && tribe.position.x < bush.position.x + 0.25)
                {
                    GameObject.Destroy(tribe.gameObject);
                }

            }
        }
    }
}
