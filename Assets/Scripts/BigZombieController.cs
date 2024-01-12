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
    Vector2 moveDirection = new Vector2(1, 0);
    Animator animator;
    Rigidbody2D bigZombieRB;
    private float spawnRate = 1f;
    private float timePerSpawn = 2f;
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
        moveDirection = (player.position - transform.position).normalized;
        if(distanceFromPlayer <= shootingRange && timePerSpawn < Time.time) {
            Shoot();
            timePerSpawn = Time.time + spawnRate;
        }
        if(distanceFromPlayer > lineOfSite || distanceFromPlayer < shootingRange) {
            animator.SetBool("isMoving", false);
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        bool isMoving = Vector2.Distance(transform.position, previousPosition) > 0.001f;
        if(isMoving) {
            float horizontalMovementDirection = transform.position.x - previousPosition.x;
            animator.SetFloat("Look X", horizontalMovementDirection);
        }
        animator.SetBool("isMoving", isMoving);
        previousPosition = transform.position;
    }

    void Shoot() {
        Vector2 shootDirection = moveDirection.normalized;
        Vector2 spawnPosition = bulletSpawn.transform.position;
        GameObject bulletObject = Instantiate(bullet, spawnPosition, Quaternion.identity);
        BulletController bulletController = bulletObject.GetComponent<BulletController>();
        bulletController.Launch(shootDirection, 300);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
