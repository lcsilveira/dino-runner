using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Properties")]
    public float gameSpeed = 5f;
    [SerializeField] private float secondsForScore = 0.5f;
    [SerializeField] private float secondsForSpeedIncrement = 1f;
    [SerializeField] private float speedIncrement = 0.25f;

    private float initialGameSpeed;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private TextMeshProUGUI highScoreTMP;
    [SerializeField] public Animator animatorUI;
    [SerializeField] private TextMeshProUGUI playButtonText;
    [SerializeField] private GameObject menuObj;

    [Header("Save Manager")]
    private PlayerData playerData;
    private string savePath;
    private const string saveFile = "SavedData.json";

    [Header("Debug")]
    public bool paused = true;
    public bool isDead = false;
    public uint highScore, score;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Time.timeScale = 0;

        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);

        initialGameSpeed = gameSpeed;
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
        UpdateMenu();
    }

    private void UpdateMenu()
    {
        if (Input.GetButtonDown("Cancel") || (paused && Input.GetButtonDown("Submit")))
            TogglePause();
        animatorUI.SetBool("paused", paused);

        if (isDead)
            playButtonText.text = "Play again";
        else
            playButtonText.text = paused ? "Play" : "Pause";
    }

    private void CountScore()
    {
        score++;
        UpdateUI();
        if (score % 100 == 0)
            FindObjectOfType<Player>().PlaySound("point");
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

        menuObj.SetActive(paused);

        if (isDead)
        {
            isDead = false;
            gameSpeed = initialGameSpeed;
            score = 0;
            SceneManager.LoadScene(0);
        }
    }

    public void UpdateUI()
    {
        scoreTMP.text = score.ToString();
        highScoreTMP.text = highScore.ToString();
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
