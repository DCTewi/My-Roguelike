using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 2f;

    private Player player;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
    }

    private void Update()
    {
        CheckIfDeath();
    }

    private void FixedUpdate()
    {
        WalkToPlayer();
    }

    protected virtual void CheckIfDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Hurt()
    {
        anim.SetTrigger("Damage");
        health -= 1.0f;
    }

    private void WalkToPlayer()
    {
        // Find Player

        // Walk to Player

        // Attack If It Could
    }
}
