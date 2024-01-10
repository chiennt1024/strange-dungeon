using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBow : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void Shoot() {
        animator.Play("BowShootAnim", 0, 0f);
    }
}
