using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Enemy_FireSpirit : MonoBehaviour
{

    PlayerController[] players;
    PlayerController nearestPlayer;
    public float speed;
    Animator anim;
    float deathTimer;
    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Distances from Each Player
        float distanceToPlayerOne = Vector2.Distance(transform.position, players[0].transform.position);
        float distanceToPlayerTwo = Vector2.Distance(transform.position, players[1].transform.position);

        //choose the nearest player
        if(distanceToPlayerOne > distanceToPlayerTwo) //if Player One is further
        {
            nearestPlayer = players[1]; //nearest player is Player Two
        }
        else
        {
            nearestPlayer = players[0]; //Otherwise, nearest player is Player One
        }

        //if nearestPlayer is decided, go to that player
        if(nearestPlayer != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);
        }

        //if gotten close to the nearest player, do the animation

        
    }
    //if came into contact with the edge of the line, die
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "TheLine")
        {
            //anim.SetBool("isDead", true);
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

}
