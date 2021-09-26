using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> tiles = new List<GameObject>();

    // Utilizo 2 arrays para contener las posiciones x e y
    // predefinidas donde se pueden instanciar bloques.
    private readonly float[] xTilePositions = new float[] { -6f, -4.975f, -3.95f, -2.925f, -1.9f, -0.875f, 0.15f, 1.175f, 2.2f, 3.225f };

    private readonly float[] yTilePositions = new float[] { 4f, 3.65f, 3.3f, 2.95f, 2.6f, 2.25f, 1.9f, 1.55f, 1.2f, 0.85f, 0.5f, 0.15f };

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        if (tiles?.Count > 0)
        {
            // Creo una lista para ir guardando las posiciones que se han utilizado.
            // El tipo "Tuple" permite especificar 2 parámetros como Item1 & Item2 en este caso de tipo int;
            // los mismos van a representar tanto x como y para cada bloque instanciado.
            // TODO: Se podría crear tambien una matriz en lugar de una lista.. habría que ver performance..
            var tilePlacementMap = new List<Tuple<float, float>>();

            // Determino la cantidad aleatoria de bloques que voy a generar tomando como mínimo 4 filas completas.
            // y como máximo, el total de filas x columnas.
            var tileCount = Random.Range(xTilePositions.Length * 4, xTilePositions.Length * yTilePositions.Length);

            // Voy generando los bloques hasta completar la cantidad deseada.
            while (tileCount > 0)
            {
                var x = xTilePositions[Random.Range(0, xTilePositions.Length)];
                var y = yTilePositions[Random.Range(0, yTilePositions.Length)];

                // Verifico que las posiciones generadas se encuentren disponibles.
                if (!tilePlacementMap.Any(t => t.Item1 == x && t.Item2 == y))
                {
                    // Obtengo un bloque aleatorio.
                    var tilePrefab = GetRandomTile();

                    // Instancio el bloque a partir del prefab.
                    Instantiate(tilePrefab, new Vector3(x, y, 1), Quaternion.identity);

                    // Agrego la posición a la lista para marcarla commo usada.
                    tilePlacementMap.Add(new Tuple<float, float>(x, y));

                    // Resto 1 al contador.
                    tileCount--;
                }
            }
        }
    }

    protected GameObject GetRandomTile()
    {
        // Genero un valor random entre 0 y 1
        var rnd = Random.Range(0, 1.0f);

        // Itero sobre cada template para verificar que
        // peso tiene su spawnRate.
        foreach (var currentTile in tiles)
        {
            var currentTileWeight = currentTile.GetComponent<Block>().spawnRate;

            if (rnd < currentTileWeight)
                return currentTile;

            rnd -= currentTileWeight;
        }

        // Si llego a este punto, es porque la suma de los valores de spawnRate
        // de todos los tiles no es 1.
        throw new ArgumentException("La suma de los spawnRate no es 1.");
    }
}