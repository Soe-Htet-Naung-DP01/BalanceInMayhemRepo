using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class EnemySpawner : MonoBehaviour
{
    public Transform[] eSpawnPoints;
    public GameObject enemyPrerfab;
    float timeBtwnEachSpawn;
    public float spawnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        timeBtwnEachSpawn = spawnCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsMasterClient == false || PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            return;
        }
        else
        {
            if (timeBtwnEachSpawn <= 0)
            {
                Vector3 spawnPos = eSpawnPoints[Random.Range(0, eSpawnPoints.Length)].position;
                PhotonNetwork.Instantiate(enemyPrerfab.name, spawnPos, Quaternion.identity);
                timeBtwnEachSpawn = spawnCooldown;
            }
            else
            {
                timeBtwnEachSpawn -= Time.deltaTime;
            }
        }
        
        
    }
}
