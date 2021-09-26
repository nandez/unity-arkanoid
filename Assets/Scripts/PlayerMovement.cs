using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Velocidad de movimiento de la paleta.
    /// </summary>
    public float speed = 20.0f;

    /// <summary>
    /// Referencia a la pelota.
    /// </summary>
    public GameObject ballRef;

    private Vector2 initialPosition;
    private bool stickyMode = true;

    private Collider2D playerCol;
    private Rigidbody2D playerRb;

    private void Start()
    {
        playerCol = GetComponent<Collider2D>();
        playerRb = GetComponent<Rigidbody2D>();

        // Guardo la posicion inicial para utilizar en el Reset();
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Get Horizontal Input
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Set Velocity (movement direction * speed)
        playerRb.velocity = Vector2.right * horizontal * speed;

        // Verifico si la paleta se encuentra en stickyMode y si está tocando la pelota
        if (stickyMode && playerCol.IsTouching(ballRef.GetComponent<Collider2D>()))
        {
            // Muevo la pelota junto con la paleta.
            ballRef.transform.position = new Vector3(transform.position.x, ballRef.transform.position.y, ballRef.transform.position.z);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // SI ademas el jugador presiona la tecla Espacio, entonces lanzo la pelota hacia arriba.
                ballRef.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;

                // TODO: verificar antes si no existe un powerup de stickyMode activo.. en ese caso
                // el sticky no debería setearse en false sino que este debería apagarse cuando finalice
                // el tiempo del powerup.
                stickyMode = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Si la la pelota colisiona con la barra y esta no tiene el stickymode
        // entonces calculo el vector de rebote.
        if (col.collider.name.Equals(ballRef.name) && !stickyMode)
        {
            var ballSpeed = ballRef.GetComponent<Ball>().speed;
            var ballRb = ballRef.GetComponent<Rigidbody2D>();

            var x = (col.transform.position.x - transform.position.x) / playerCol.bounds.size.x;
            ballRb.velocity = ballSpeed * new Vector2(x, 1).normalized;
        }
    }

    public void ResetState()
    {
        transform.position = initialPosition;
        stickyMode = true;
    }
}