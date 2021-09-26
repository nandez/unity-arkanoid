using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public List<GameObject> hearts = new List<GameObject>();
    public Text scoreElement;

    public GameObject ballRef;
    public GameObject playerRef;

    private int playerLives = 3;
    private int playerScore = 0;

    public void Start()
    {
        // Asigno un handler para manejar el evento emitido
        // por la pelota al colisionar con el borde inferior.
        ballRef.GetComponent<Ball>().OnDeadZoneCollision += OnDeadZoneCollition;
    }



    public void OnDeadZoneCollition()
    {
        // Resto 1 a las vidas del judador y actualizo el HUD.
        playerLives--;
        UpdatePlayerLivesDisplay();

        if (playerLives <= 0)
        {
            // TODO: game over...
        }

        // Resetea la posición del jugador y de la pelota para comenzar otra ronda.
        ballRef.GetComponent<Ball>().ResetState();
        playerRef.GetComponent<PlayerMovement>().ResetState();
    }

    public void OnBlockDestroyed(int points)
    {
        playerScore += points;
        scoreElement.text = string.Format("SCORE{0}{1:D6}", Environment.NewLine, playerScore);
    }

    private void UpdatePlayerLivesDisplay()
    {
        // Itero sobre los corazones para Activar / Desactivar
        // en base a la cantidad de vidas del jugador..
        for (int i = 0; i < hearts.Count; i++)
            hearts[i].SetActive(i + 1 <= playerLives);
    }
}