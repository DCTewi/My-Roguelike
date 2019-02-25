using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public static float food = 50.0f;
    public float actionCD = 0.6f;
    public float speed = 0.5f;
    public float hungerNap = 1.0f;
    public float hungerPer = 3.0f;

    public Text foodText;

    [HideInInspector] public bool isFaceRight = true;

    private float nextHunger = 1.0f;
    private float actionColdingDown = 0.6f;
    private float nextStep = 0.2f;

    private Animator anim;
    private CircleCollider2D atkRange;
    private SoundEffecter sound;
    private SoundEffecter stepSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        atkRange = GetComponent<CircleCollider2D>();
        sound = GetComponent<SoundEffecter>();
    }

    private void Start()
    {
        foodText = GameObject.Find("Food Text").GetComponent<Text>();
        stepSound = transform.Find("Footstep Sound").gameObject.GetComponent<SoundEffecter>();
    }

    private void Update()
    {
        // Hunger Timer
        nextHunger -= Time.deltaTime; 
        if (nextHunger <= 0f)
        {
            food -= hungerPer;
            nextHunger = hungerNap;
            UpdateFood();
        }
        if (food <= 0)
        {
            sound.Play("Die");
            GameManager.instance.GameOver();
        }

        //Attack
        if (actionColdingDown > 0)
        {
            actionColdingDown -= Time.deltaTime;
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine("Attack");

            actionColdingDown = actionCD;
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
        if ((isFaceRight && h < 0) || (!isFaceRight && h > 0)) Flip();
        if (nextStep > 0f)
        {
            nextStep -= Time.deltaTime;
        }
        else if (h != 0f || v != 0f)
        {
            stepSound.Play("Footstep" + Random.Range(0, 2), true);
            nextStep = 0.2f;
        }
        Vector3 newPos = transform.position + new Vector3(h, v) * speed;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, newPos.x, 0.2f), Mathf.Lerp(transform.position.y, newPos.y, 0.2f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.Enemy)
        {
            collision.SendMessage("Hurt");
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        isFaceRight = !isFaceRight;
    }

    private void UpdateFood()
    {
        foodText.text = "Food: " + food;
    }

    public void Heal(float deltaFood)
    {
        if (deltaFood > 10.0f)
        {
            sound.Play("Soda" + Random.Range(0, 2));
        }
        else
        {
            sound.Play("Fruit" + Random.Range(0, 2));
        }
        food += deltaFood;
        UpdateFood();
    }

    public void Hurt(float damage)
    {
        anim.SetTrigger("Damage");
        sound.Play("Damage" + Random.Range(0, 2));
        food -= damage;
        UpdateFood();
    }

    private IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.1f);
        atkRange.enabled = true;
        sound.Play("Chop" + Random.Range(0, 2));
        
        yield return new WaitForSeconds(0.3f);
        atkRange.enabled = false;
    }
}
