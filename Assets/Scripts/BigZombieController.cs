using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigZombieController : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public GameObject bullet;
    public GameObject bulletSpawn;
    private Transform player;
    private Vector2 previousPosition;
    Animator animator;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if(distanceFromPlayer <= shootingRange) {
            Debug.Log("Shoot");
            Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        }
        if(distanceFromPlayer > lineOfSite || distanceFromPlayer < shootingRange) {
            animator.SetBool("isMoving", false);
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        bool isMoving = Vector2.Distance(transform.position, previousPosition) > 0.001f;
        if(isMoving) {
            float movementDirection = transform.position.x - previousPosition.x;
            animator.SetFloat("Look X", movementDirection);
        }
        animator.SetBool("isMoving", isMoving);
        previousPosition = transform.position;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
