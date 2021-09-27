using UnityEngine;

public class Block : MonoBehaviour
{
    public int points = 10;
    public int hitPoints = 1;
    public bool invulnerable = false;

    [Range(0, 1)]
    public float spawnRate;

    public Sprite normalForm;
    public Sprite brokenForm;

    public AudioClip blockHitSfx;
    public AudioClip blockDestroySfx;

    private bool hasCollide = false;

    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = normalForm;
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.CompareTag("Ball") && !hasCollide && !invulnerable)
        {
            // Seteo la varaible hasCollide para evitar multiples colisiones con la pelota..
            hasCollide = true;
            hitPoints--;

            if (hitPoints <= 2)
                GetComponent<SpriteRenderer>().sprite = brokenForm;

            // Si el bloque no tiene mas puntos, destruyo el gameObject
            if (hitPoints <= 0)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>().OnBlockDestroyed(points);
                GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>().PlayOneShot(blockDestroySfx, 0.35f);
                Destroy(gameObject);
            }
            else
            {
                GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>().PlayOneShot(blockHitSfx, 0.5f);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (hasCollide)
            hasCollide = false;
    }
}