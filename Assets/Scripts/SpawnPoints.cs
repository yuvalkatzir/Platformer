using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform[] spawns;
    private int currentSpawn = 0;

    private void Start()
    {
        playerTransform.position = spawns[0].position;
    }

    public void Respawn()
    {
        playerTransform.position = spawns[currentSpawn].position;
    }

    public bool UpdateSpawn(Transform spawn)
    {
        int spawnIndex = Array.IndexOf(spawns, spawn);
        if(spawnIndex > currentSpawn)
        {
            currentSpawn = spawnIndex;
            return true;
        }

        return false;
    }
}
