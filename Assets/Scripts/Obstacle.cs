using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

        //Vector3 destination = transform.position - Vector3.right * scenarioSpeed * obstacleSpeed;
        //transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime / 2);

        if (transform.position.x <= leftEdge.x - 2f)
            Destroy(gameObject);
    }
}
