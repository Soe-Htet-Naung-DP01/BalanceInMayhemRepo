using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{

    public float speed;
    Animator anim;
    PhotonView view;
    Health healthScript;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        healthScript = FindObjectOfType<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        {
            //movement
            Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveDistance = movementInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveDistance;
            
            //animation
            if(movementInput == Vector2.zero)
            {
                anim.SetBool("isWalking", false);
            }
            else
            {
                anim.SetBool("isWalking", true);
            }

        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(view.IsMine)
        {
            if (collision.tag == "Enemy")
            {
                anim.SetBool("isTakingDmg", true);
                healthScript.TakeDmg();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(view.IsMine)
        {
            if(collision.tag == "Enemy")
            {
                anim.SetBool("isTakingDmg", false);
            }
        }
        
    }
}
