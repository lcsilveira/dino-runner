using UnityEngine;

public class AnimationPlayback : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Update()
    {
        animator.speed = GameManager.Instance.gameSpeed / 25;
    }
}
