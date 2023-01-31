using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [Header("Properties")]
    public float gameSpeed = 5f;
    [SerializeField] private float secondsForScore = 0.5f;
    [SerializeField] private float secondsForSpeedIncrement = 1f;
    [SerializeField] private float speedIncrement = 0.25f;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI pointsTMP;
    [SerializeField] private TextMeshProUGUI recordTMP;

    [Header("Save Manager")]
    private PlayerData playerData;
    private string savePath;
    private const string saveFile = "SavedData.json";

    [Header("Debug")]
    public bool paused = true;
    public uint highScore, score;

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
        DontDestroyOnLoad(gameObject);
        InvokeRepeating(nameof(CountScore), secondsForScore, secondsForScore);
        InvokeRepeating(nameof(IncreaseSpeed), secondsForSpeedIncrement, secondsForSpeedIncrement);

        savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + saveFile;
        LoadData();
        UpdateUI();
    }

    private void Update()
    {
        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    private void CountScore()
    {
        score++;
        UpdateUI();
    }

    private void IncreaseSpeed()
    {
        gameSpeed += speedIncrement;
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

    public void UpdateUI()
    {
        pointsTMP.text = score.ToString();
        recordTMP.text = highScore.ToString();
    }

    public void SaveData()
    {
        if (playerData == null)
            playerData = new PlayerData(score);
        else
            playerData.highScore = highScore = score;
        string json = JsonUtility.ToJson(playerData);

        StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
        writer.Close();
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            StreamReader reader = new StreamReader(savePath);
            string json = reader.ReadToEnd();

            playerData = JsonUtility.FromJson<PlayerData>(json);
            highScore = playerData.highScore;
            reader.Close();
        }
    }
}
