using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    protected virtual float Feed()
    {
        return 6.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.Player)
        {
            collision.gameObject.SendMessage("Heal", Feed());
            Destroy(gameObject);
        }
    }
}
