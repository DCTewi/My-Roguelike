using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHard : Enemy
{
    protected override void CheckIfDeath()
    {
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
