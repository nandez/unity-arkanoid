using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject gameManager;

    public float Speed = 8.0f;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * Speed;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            var x = (transform.position.x - col.transform.position.x) / col.collider.bounds.size.x;
            Vector2 dir = new Vector2(x, 1).normalized;

            GetComponent<Rigidbody2D>().velocity = Speed * dir;
        }
        else if (col.collider.CompareTag("DeadZone"))
        {
            gameManager.GetComponent<GameStateManager>().OnDeadZoneCollition();
        }
    }
}