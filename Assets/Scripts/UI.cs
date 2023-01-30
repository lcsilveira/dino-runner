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
        animator.SetBool("paused", paused);

        buttonText.text = paused ? "Play" : "Pause";
    }
}
