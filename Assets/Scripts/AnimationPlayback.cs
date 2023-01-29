using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayback : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Update()
    {
        animator.speed = GameManager.Instance.gameSpeed / 25;
    }
}
