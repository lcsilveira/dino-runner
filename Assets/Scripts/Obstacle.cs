using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float obstacleSpeed = 1f;
    private Vector3 leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero);
    }

    private void Update()
    {
        float scenarioSpeed = GameManager.Instance.gameSpeed / transform.localScale.x;
        transform.position -= Vector3.right * scenarioSpeed * obstacleSpeed * Time.deltaTime;
        if (transform.position.x <= leftEdge.x - 2f)
        {
            Destroy(gameObject);
        }
    }
}
