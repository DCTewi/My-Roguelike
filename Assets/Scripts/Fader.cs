using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Text levelText;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(HideLevelText());
    }

    private IEnumerator HideLevelText()
    {
        yield return new WaitForSeconds(0.9f);

        levelText.color = new Color(0f, 0f, 0f, 0f);
    }

    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }
}
