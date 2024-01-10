using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponArrow : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetRotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
    public void Launch(Vector2 direction, float force) {
        rigidbody2D.AddForce(direction * force);
        SetRotation(direction);
    }
}
