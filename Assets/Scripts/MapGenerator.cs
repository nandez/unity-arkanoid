using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> tilePrefabs = new List<GameObject>();

    // Referencia al
    public GameObject topBorder;

    private void Start()
    {
        Debug.Log($"Width: {Screen.width} Height: {Screen.height}");
    }


    public void GenerateMap()
    {
        
    }
}
