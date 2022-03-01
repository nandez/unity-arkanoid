using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameStateManager : MonoBehaviour
{
    public List<GameObject> hearts = new List<GameObject>();
    public Text scoreElement;

    public GameObject ballRef;
    public GameObject playerRef;

    public GameObject PanelOverlay;
    public TMPro.TMP_Text PanelMessage;

    public static int PlayerLives { get; private set; }
    private int playerScore = 0;

    public void Start()
    {
        // Asigno un handler para manejar el evento emitido
        // por la pelota al colisionar con el borde inferior.
        ballRef.GetComponent<Ball>().OnDeadZoneCollision += OnDeadZoneCollition;

        PlayerLives = 3;
        Time.timeScale = 1;
    }



    public void OnDeadZoneCollition()
    {
        // Resto 1 a las vidas del judador y actualizo el HUD.
        PlayerLives--;
        UpdatePlayerLivesDisplay();

        if (PlayerLives <= 0)
        {
            PanelMessage.SetText("GAME OVER!");
            PanelOverlay.SetActive(true);
            StartCoroutine(NavigateToMainMenu());
        }

        // Resetea la posición del jugador y de la pelota para comenzar otra ronda.
        ballRef.GetComponent<Ball>().ResetState();
        playerRef.GetComponent<PlayerMovement>().ResetState();
    }

    public void OnBlockDestroyed(int points)
    {
        playerScore += points;
        scoreElement.text = string.Format("SCORE{0}{1:D6}", Environment.NewLine, playerScore);

        // Obtengo todos los objetos con el tag TILE.
        var tileObjects = GameObject.FindGameObjectsWithTag("Tile");

        // Filtro aquellos bloques que puedan romperse..
        var tiles = tileObjects.Count(t => t.GetComponent<Block>()?.invulnerable == false);

        // Verifico si no quedan bloques para romper..
        if(tiles - 1 <= 0)
        {
            // Pauso el juego para mostrar la pantalla de highscores
            Time.timeScale = 0;

            PanelMessage.SetText("LEVEL COMPLETED!");
            PanelOverlay.SetActive(true);
            StartCoroutine(NavigateToMainMenu());
        }
    }

    private void UpdatePlayerLivesDisplay()
    {
        // Itero sobre los corazones para Activar / Desactivar
        // en base a la cantidad de vidas del jugador..
        for (int i = 0; i < hearts.Count; i++)
            hearts[i].SetActive(i + 1 <= PlayerLives);
    }

    protected IEnumerator NavigateToMainMenu()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("MenuScene");
    }
}