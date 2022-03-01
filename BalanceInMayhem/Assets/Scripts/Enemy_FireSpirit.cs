using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FireSpirit : MonoBehaviour
{

    PlayerController[] players;
    PlayerController nearestPlayer;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Distances from Each Player
        float distanceToPlayerOne = Vector2.Distance(transform.position, players[0].transform.position);
        float distanceToPlayerTwo = Vector2.Distance(transform.position, players[1].transform.position);

        //go to the nearer player
        if(distanceToPlayerOne > distanceToPlayerTwo) //if Player One is further
        {
            nearestPlayer = players[1]; //nearest player is Player Two
        }
        else
        {
            nearestPlayer = players[0]; //Otherwise, nearest player is Player One
        }

    }
}
