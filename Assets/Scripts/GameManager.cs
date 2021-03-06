﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float level = 1;
    public Fader fader;
    public static GameManager instance;

    private TileGenerator tileGenerator;

    private void Awake()
    {
        // To Keep GameManager Unique
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            tileGenerator = GetComponent<TileGenerator>();
            tileGenerator.GenerateTiles();
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void CallBackOnceSceneInit()
    {
        SceneManager.sceneLoaded += OnSceneInit;
    }

    private static void OnSceneInit(Scene arg0, LoadSceneMode arg1)
    {
        instance.level++;
        instance.InitScene();
    }

    private void InitScene()
    {
        tileGenerator.GenerateTiles();
    }

    [ContextMenu("Next Stage")]
    public void NextStage()
    {
        SceneManager.LoadScene(Scenes.Main);
    }

    public void GameOver()
    {
        fader.FadeOut();
        StartCoroutine(GameOverCoro());
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;

        instance.level = 0;
        NextStage();
    }

    public IEnumerator GameOverCoro()
    {
        yield return new WaitForSeconds(0.8f);
        Time.timeScale = 0f;
    }
}
