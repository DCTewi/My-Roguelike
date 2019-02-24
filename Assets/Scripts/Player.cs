using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float food = 50.0f;
    public float actionCD = 0.2f;
    public float speed = 0.5f;
    public float hungerNap = 1.0f;
    public float hungerPer = 5.0f;

    public Text foodText;

    [HideInInspector] public bool faceRight = true;

    private float nextHunger = 1.0f;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        foodText = GameObject.Find("Food Text").GetComponent<Text>();
    }

    private void Update()
    {
        // Hunger Timer
        nextHunger -= Time.deltaTime; 
        if (nextHunger <= 0f)
        {
            food -= hungerPer;
            nextHunger = hungerNap;
            foodText.text = "Food: " + food;
        }
        if (food <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

    private void FixedUpdate()
    {
        // Move
        float h = 0, v = 0;
#if UNITY_STANDALONE

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

#elif UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            Vector2 movePos = Input.touches[0].deltaPosition;

            h = movePos.x > 0.5f ? 1f : movePos.x < -0.5f ? -1f : 0;
            v = movePos.y > 0.5f ? 1f : movePos.y < -0.5f ? -1f : 0;
        }

#endif
        Vector3 newPos = transform.position + new Vector3(h, v) * speed;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, newPos.x, 0.2f), Mathf.Lerp(transform.position.y, newPos.y, 0.2f));
    }
}
