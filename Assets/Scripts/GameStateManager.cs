using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public List<GameObject> hearts = new List<GameObject>();
    public Text scoreElement;

    private int playerLives = 3;
    private int playerScore = 0;

    public void OnDeadZoneCollition()
    {
        playerLives--;

        UpdatePlayerLivesDisplay();

        if (playerLives <= 0)
        {
            // TODO: game over...
        }
    }

    public void OnBlockDestroyed(int points)
    {
        playerScore += points;
        scoreElement.text = string.Format("{0:D6}", playerScore);
    }

    private void UpdatePlayerLivesDisplay()
    {
        // Itero sobre los corazones para Activar / Desactivar
        // en base a la cantidad de vidas del jugador..
        for (int i = 0; i < hearts.Count; i++)
            hearts[i].SetActive(i + 1 <= playerLives);
    }
}