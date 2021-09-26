using UnityEngine;

// TODO: Ver los decorados de la clase para especificar
// que componentes son requeridos. ej: RigidBody2D
public class Ball : MonoBehaviour
{
    /// <summary>
    /// Velocidad de la pelota.
    /// </summary>
    public float speed = 7.5f;

    /// <summary>
    /// Velocidad máxima de la pelota
    /// </summary>
    public float maxSpeed = 25f;


    // Creo un delegado para emitir el evento y suscribirme desde el GameManager.
    public delegate void DeadZoneCollision();
    public event DeadZoneCollision OnDeadZoneCollision;

    private Vector2 initialPosition;
    private Rigidbody2D rb;

    private void Start()
    {
        // Guardo la posicion inicial para utilizar en el Reset();
        initialPosition = transform.position;

        // Guardo la referencia al componente RigidBody2D
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("DeadZone"))
        {
            // Si colisiona con el borde inferior, emito el evento "OnDeadZoneCollision"
            // para notificar a los suscriptores.
            OnDeadZoneCollision?.Invoke();
        }
    }

    public void MoveBall(Vector2 direction)
    {
        rb.velocity = Vector2.ClampMagnitude(speed * direction.normalized, maxSpeed);
    }

    public void ResetState()
    {
        transform.position = initialPosition;
        rb.velocity = Vector2.zero;
    }
}