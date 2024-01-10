using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other) {
        PlayerController player = other.GetComponent<PlayerController>();
        if(player != null) {
            player.ChangeHealth(-1);
        }
    }
}
