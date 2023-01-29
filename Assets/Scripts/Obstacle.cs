using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Vector3 leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero);
    }

    private void Update()
    {
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
        transform.position -= Vector3.right * speed * Time.deltaTime;
        if (transform.position.x <= leftEdge.x - 2f)
        {
            Destroy(gameObject);
        }
    }
}
