using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rigBody;
    [SerializeField] private Animator animator;

    [Header("Properties")]
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float animationSpeed;

    [Header("Debug")]
    [SerializeField] private bool isGrounded;

    private void Update()
    {
        if (isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                isGrounded = false;
                animator.SetBool("isGrounded", false);
                rigBody.velocity = Vector2.up * jumpForce;
            }
        }
        else
        {
            rigBody.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
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
            GameManager.Instance.paused = true;
            if (GameManager.Instance.score > GameManager.Instance.highScore)
            {
                GameManager.Instance.SaveData();
                GameManager.Instance.UpdateUI();
            }
        }
    }
}
