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
                // Si ademas el jugador presiona la tecla Espacio, entonces lanzo la pelota hacia arriba.
                // Genero un vector con un random en la x para dar una pequeña variación al movimiento inicial.
                var upDirection = new Vector2(Random.Range(-0.25f, 0.25f), 1);
                Debug.Log(upDirection.x);
                ballRef.GetComponent<Ball>().MoveBall(upDirection);

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
            var x = (col.transform.position.x - transform.position.x) / playerCol.bounds.size.x;
            ballRef.GetComponent<Ball>().MoveBall(new Vector2(x, 1));
        }
    }

    public void ResetState()
    {
        transform.position = initialPosition;
        stickyMode = true;
    }
}