using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void SetRotation(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
    public void Launch(Vector2 direction, float force) {
        bulletRB.AddForce(direction * force);
        animator.Play("PurpleBullet", 0, 0f);
        SetRotation(direction);
        Destroy(gameObject, 2);
    }
}
