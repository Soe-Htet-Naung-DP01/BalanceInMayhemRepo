using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float dashSpeed;
    public float dashTime;
    float resetSpeed;

    public float minX, maxX, minY, maxY;

    Animator anim;
    PhotonView view;
    Health healthScript;

    LineRenderer line;
    private void Start()
    {
        resetSpeed = speed;
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        healthScript = FindObjectOfType<Health>();
        line = FindObjectOfType<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (view.IsMine)
        {
            //movement
            //PC Controls
            Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveDistance = movementInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveDistance;

            //If player goes out of bound
            Wrap();

            //dash
            //PC Controls
            if (Input.GetKeyDown(KeyCode.Space) && movementInput != Vector2.zero)
            {
                StartCoroutine(Dash());
            }

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

    IEnumerator Dash()
    {
        speed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed = resetSpeed;
    }

    void Wrap()
    {
        if (transform.position.x < minX)
        {
            //Put a particle effect here
            transform.position = new Vector2(maxX, transform.position.y);
        }
        if (transform.position.x > maxX)
        {
            //Put a particle effect here
            transform.position = new Vector2(minX, transform.position.y);
        }
        if (transform.position.y < minY)
        {
            //Put a particle effect here
            transform.position = new Vector2(transform.position.x, maxY);
        }
        if (transform.position.y > maxY)
        {
            //Put a particle effect here
            transform.position = new Vector2(transform.position.x, minY);
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
