using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [Header("Properties")]
    public float gameSpeed = 5f;
    public bool paused = true;
    public uint points;
    public float secondsForScore = 1f;

    private float initialGameSpeed;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI pointsTMP;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Time.timeScale = 0;

        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }

    private void Start()
    {
        initialGameSpeed = gameSpeed;
        DontDestroyOnLoad(gameObject);
        InvokeRepeating(nameof(countScore), secondsForScore, secondsForScore);
    }

    private void Update()
    {
        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    private void countScore()
    {
        points++;

        float speedIncrease = points / 50f;
        speedIncrease = (float)Math.Floor(speedIncrease);

        gameSpeed = initialGameSpeed + speedIncrease/2;

        pointsTMP.text = points.ToString();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void TogglePause()
    {
        paused = !paused;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
