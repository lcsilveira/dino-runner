using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] private TextMeshProUGUI buttonText;

    private void Update()
    {
        bool paused = GameManager.Instance.paused;

        if (Input.GetButtonDown("Cancel"))
            GameManager.Instance.TogglePause();

        if (paused && Input.GetButtonDown("Submit"))
            GameManager.Instance.TogglePause();

        animator.SetBool("paused", paused);

        buttonText.text = paused ? "Play" : "Pause";
    }
}
