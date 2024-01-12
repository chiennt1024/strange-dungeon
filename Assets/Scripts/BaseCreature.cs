using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCreature : MonoBehaviour
{
    public float speed;
    protected Rigidbody2D rb2d;
    protected Vector2 previousPosition;
    Animator animator;
    public Transform targetTransform;

    protected void Awake() {
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
    }
    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
        bool isMoving = Vector2.Distance(transform.position, previousPosition) > 0.091f;
        if(isMoving) {
            float horizontalMoveDirection = transform.position.x - previousPosition.x;
            animator.SetFloat("Look X", horizontalMoveDirection);
        }
        animator.SetBool("isMoving", isMoving);
        previousPosition = transform.position;
    }
    protected abstract void Attack();
}
