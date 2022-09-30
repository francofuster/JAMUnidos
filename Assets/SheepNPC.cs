using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepNPC : MonoBehaviour
{
    [Range(0,5)]
    public float speed = 1f;

    public int TimeToTurn = 5;

    public Animator anim;

    private Rigidbody rb;

    private int timeInterval;

    private int activeState;

    private bool canCheckMove = true;

    private bool canMove = false;

    private float dirX;
    private float dirY;

    // Start is called before the first frame update


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if(canCheckMove)
        {
            
            activeState = Random.Range(0,3);
            switch(activeState)
            {
                case 0 ://Move Front
                    canCheckMove = false;
                    dirX = 0f;
                    dirY = -1;
                    StartCoroutine(Walk());
                    break;

                case 1 ://Move Back
                    canCheckMove = false;
                    dirX = 0f;
                    dirY = 1;
                    StartCoroutine(Walk());
                    break;

                case 2 ://Move Left
                    canCheckMove = false;
                    dirX = -1f;
                    dirY = 0;
                    StartCoroutine(Walk());
                    break;

                case 3 ://Move Right
                    canCheckMove = false;
                    dirX = 1f;
                    dirY = 0;
                    StartCoroutine(Walk());
                    break;

            }
        }

        if(canMove)
        {
            Vector3 moveDir = new Vector3(dirX, 0f, dirY);
            transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
        }        
        else
        {
            transform.Translate(Vector3.zero);
            rb.velocity = Vector3.zero;
        }
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        StopCoroutine(Walk());
        // canMove = false;
        StartCoroutine(SetIdle());
    }

    // private void OnTriggerEnter(Collider other) //when collision is detected. the sheep will stop moving and set idle state
    // {
    //     // StopCoroutine(Walk());
    //     StartCoroutine(SetIdle());
    // }

    private IEnumerator Walk()
    {
        StopCoroutine(SetIdle());
        anim.SetBool("isMoving", true);
        anim.SetFloat("DirectionX", dirX);
        anim.SetFloat("DirectionY", dirY);
        
        canMove = true;

        yield return new WaitForSeconds(4);

        
        StartCoroutine(SetIdle());
    }

    private IEnumerator SetIdle()
    {
        canMove = false;
        
        anim.SetBool("isMoving", false);
        
        yield return new WaitForSecondsRealtime(TimeToTurn);
        canCheckMove = true;
    }
}
