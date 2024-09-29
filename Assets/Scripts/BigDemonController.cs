using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigDemonController : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    private Transform player;
    private Vector2 previousPosition;
    Animator animator;
    public AnimationClip animationClip;
    CircleCollider2D attackArea;
    public GameObject skillCharge;
    Animator skillChargeAnimator;
    bool isPerformSkill;
    Vector2 moveDirection = new Vector2(1, 0);
    public GameObject projectileSpawn;
    public GameObject projectile;
    bool isShoot;

    void Start() {
        
    }
    void Awake()
    {
        isPerformSkill = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        attackArea = GetComponent<CircleCollider2D>();
        previousPosition = transform.position;
        skillChargeAnimator = skillCharge.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        moveDirection = (player.position - transform.position).normalized;
        if (distanceFromPlayer > lineOfSite) {
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

    private IEnumerator Shoot()
    {
        Vector2 shootDirection = moveDirection.normalized;
        Vector2 spawnPosition = projectileSpawn.transform.position;
        GameObject bulletObject = Instantiate(projectile, spawnPosition, Quaternion.identity);
        BulletController bulletController = bulletObject.GetComponent<BulletController>();
        bulletController.Launch(shootDirection, 300);
        yield return new WaitForSeconds(1f);
        isPerformSkill = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }

    private void OnTriggerStay2D(Collider2D col) {
        if(isPerformSkill) {
            return;
        }
        isPerformSkill = true;
        if (isShoot) {
            isShoot = false;
            StartCoroutine(Shoot());
        }
        else {
            isShoot = true;
            Debug.Log("Perform" + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            PerformChargeSkill();
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
    }

    private void PerformChargeSkill() {
        skillChargeAnimator.SetBool("isCharge", true);
        StartCoroutine(PerformMeleeAttack());
    }

    private void PerformStopChargeSkill() {
        skillChargeAnimator.SetBool("isCharge", false);
    }

    private IEnumerator PerformMeleeAttack()
    {
        yield return new WaitForSeconds(2);
        skillChargeAnimator.SetBool("isCharge", false);
        skillChargeAnimator.SetBool("isRelease", true);
        yield return new WaitForSeconds(1f);

        skillChargeAnimator.SetBool("isRelease", false);
        yield return new WaitForSeconds(1f);
        isPerformSkill = false;
        Debug.Log("Skill release");
    }
}
