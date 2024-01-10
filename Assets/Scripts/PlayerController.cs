using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Vector2 move;
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);
    public GameObject weaponArrowPrefab;
    public InputAction MoveAction;
    public InputAction shootAction;
    public float speed = 5.0f;
    public int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; } }
    WeaponBow weaponBow;

    void Start()
    {
        MoveAction.Enable();
        shootAction.Enable();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        bool isMovingHorizontal = !Mathf.Approximately(move.x, 0.0f);
        bool isMovingVertical = !Mathf.Approximately(move.y, 0.0f);
        if (isMovingHorizontal || isMovingVertical)
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        if (isMovingHorizontal)
        {
            animator.SetFloat("Look X", moveDirection.x);
        }
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2D.position + move * speed * Time.deltaTime;
        rigidbody2D.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
    void Shoot()
    {
        WeaponBow weaponBow = GetComponentInChildren<WeaponBow>();
        if (weaponBow != null)
        {
            weaponBow.Shoot();
            float bowRotationAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            weaponBow.transform.rotation = Quaternion.AngleAxis(bowRotationAngle, Vector3.forward);
        }
        Vector2 launchDirection = moveDirection.normalized;
        Vector2 spawnPosition = rigidbody2D.position + Vector2.down * 0.5f;
        GameObject weaponArrowObject = Instantiate(weaponArrowPrefab, spawnPosition, Quaternion.identity);
        WeaponArrow weaponArrow = weaponArrowObject.GetComponent<WeaponArrow>();
        weaponArrow.Launch(launchDirection, 300);
    }
}
