using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    private Vector2 respawnPos;
    Collider2D coll;

    FieldOfView fieldOfView;
    [SerializeField] GameObject enemy;

    public bool csp;

    public Animator animator;

    void Awake()
    {
        fieldOfView = enemy.GetComponent<FieldOfView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        respawnPos= transform.position;
    }

    void Update()
    {
        csp = fieldOfView.CanSeePlayer;
        if (csp)
        {
            StartCoroutine(WaitBeforeRespawn());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(WaitBeforeRespawn());
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            respawnPos = transform.position;
            coll= collision.GetComponent<Collider2D>();
            coll.enabled= false;
        }

    }

    IEnumerator WaitBeforeRespawn()
    {
        animator.SetBool("isDead", true);

        yield return new WaitForSeconds(1);

        transform.position = respawnPos;
        animator.SetBool("isDead", false);
    }
}
