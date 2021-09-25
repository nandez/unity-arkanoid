using UnityEngine;

public class Block : MonoBehaviour
{
    public int points = 10;
    public int hitPoints = 1;
    public bool invulnerable = false;

    public Sprite normalForm;
    public Sprite brokenForm;

    public GameObject gameManager;

    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = normalForm;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball") && !invulnerable)
        {
            hitPoints--;

            if (hitPoints <= 2)
                GetComponent<SpriteRenderer>().sprite = brokenForm;

            // Si el bloque no tiene mas puntos, destruyo el gameObject
            if (hitPoints <= 0)
            {
                gameManager.GetComponent<GameStateManager>().OnBlockDestroyed(points);
                Destroy(gameObject);
            }
                

           
        }
    }
}