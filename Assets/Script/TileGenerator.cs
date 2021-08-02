using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;

    private Transform playerPosition;
    public float spawnX = 0.0f;
    public float tileLength = 5.0f;
    public float trapLength = 5f;
    public float spawnTrapX = 20f;
    public int amountOfTilesOnScreen = 50;
    public int amountOfTrapsOnScreen = 20;

    // Start is called before the first frame update
    private void Start()
    {
        try
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            tileLength = tilePrefabs[0].GetComponent<Collider2D>().bounds.size.x;
            //Debug.Log(tileLength);
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < amountOfTilesOnScreen; i++)
        {
            if (playerPosition.position.x > (spawnX - amountOfTilesOnScreen * tileLength))
            {
                SpawnTile();
            }

            if (playerPosition.position.x > (spawnTrapX - amountOfTrapsOnScreen * trapLength))
            {
                SpawnTrap();
            }
        }

        //RemoveTiles(ref tilePrefabs, 1);
    }

    private void SpawnTile(int prefabIndex = -1)
    {

        int randomIndex = Random.Range(0, tilePrefabs.Length);
        GameObject go;
        go = Instantiate(tilePrefabs[0]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector2.right * spawnX;

        spawnX += tileLength;
    }

    private void SpawnTrap(int prefabIndex = -1)
    {
        int randomIndex = Random.Range(0, tilePrefabs.Length);
        GameObject go;
        go = Instantiate(tilePrefabs[1]) as GameObject;
        go.transform.SetParent(transform);
        //go.transform.position = Vector2.right * spawnTrapX;
        go.transform.position = new Vector2(Random.Range(-5f, 5.0f), 0) * spawnTrapX;

        spawnTrapX += trapLength;
    }

}
