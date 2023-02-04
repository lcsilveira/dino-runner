using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Background : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Vector3 leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero);
    }

    private void Update()
    {
        //float scenarioSpeed = GameManager.Instance.gameSpeed / transform.localScale.x;
        transform.position -= Vector3.right * speed * Time.deltaTime;

        if (transform.position.x <= leftEdge.x - 2f)
            Destroy(gameObject);
    }
}
