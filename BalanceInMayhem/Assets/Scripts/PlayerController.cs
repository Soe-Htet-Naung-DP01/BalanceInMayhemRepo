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

    LineRenderer line;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        healthScript = FindObjectOfType<Health>();
        line = FindObjectOfType<LineRenderer>();
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

            line.SetPosition(0, transform.position);
        }
        else
        {
            line.SetPosition(1, transform.position);
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
