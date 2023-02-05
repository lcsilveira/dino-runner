using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class SoundBank
{
    public string name;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume = 1f;
}

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rigBody;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    [Header("Properties")]
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float animationSpeed;

    [Header("SFX")]
    [SerializeField] SoundBank[] soundBank;

    [Header("Debug")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private float leftSpawnMargin;


    private void Start()
    {
        FixSpawnLocation();
    }

    private void FixSpawnLocation()
    {
        // 10% ahead of the left margin.
        leftSpawnMargin = (float)Camera.main.pixelWidth * 0.1f;
        spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(leftSpawnMargin, 0f));
        spawnPosition.y = transform.position.y;
        spawnPosition.z = transform.position.z;
        transform.position = spawnPosition;
    }

    private void Update()
    {
        if (GameManager.Instance.paused)
            return;

        if (isGrounded)
        {
            bool isUITouch = EventSystem.current.IsPointerOverGameObject(0);
            bool touchJump = false, touchCrouch = false;

            if (!isUITouch && Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                // If it's touching right or left side.
                if (touch.position.x > Screen.width / 2 && touch.phase == TouchPhase.Began)
                    touchJump = true;

                if (touch.position.x < Screen.width / 2 && touch.phase != TouchPhase.Ended)
                    touchCrouch = true;
            }

            if (Input.GetButton("Jump") || touchJump)
            {
                isGrounded = false;
                animator.SetBool("isGrounded", false);
                animator.SetBool("isCrouched", false);
                rigBody.velocity = Vector2.up * jumpForce;
                PlaySound("jump");
            }
            else if (Input.GetAxis("Vertical") < 0 || touchCrouch)
                animator.SetBool("isCrouched", true);
            else
                animator.SetBool("isCrouched", false);
        }
        else
        {
            rigBody.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            animator.SetBool("isCrouched", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            animator.SetBool("isDead", true);
            PlaySound("die");
            GameManager.Instance.paused = true;
            GameManager.Instance.isDead = true;
            if (GameManager.Instance.score > GameManager.Instance.highScore)
            {
                GameManager.Instance.SaveData();
                GameManager.Instance.UpdateUI();
            }
        }
    }

    public void PlaySound(string soundName)
    {
        SoundBank searchResult = soundBank.FirstOrDefault(s => s.name == soundName);
        if (searchResult != null)
            audioSource.PlayOneShot(searchResult.audioClip, searchResult.volume);
    }
}
