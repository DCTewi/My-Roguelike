using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Text levelText;
    public Text finalText;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.ResetTrigger("FadeOut");
    }

    private void Start()
    {
        levelText.text = "Level " + GameManager.instance.level;
        StartCoroutine(HideLevelText());
        finalText.color = new Color(0f, 0f, 0f, 0f);
    }

    private IEnumerator HideLevelText()
    {
        yield return new WaitForSeconds(0.9f);

        levelText.color = new Color(0f, 0f, 0f, 0f);
    }

    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
        finalText.color = new Color(255f, 255f, 255f, Mathf.Lerp(0.0f, 255f, 0.8f));
    }
}
