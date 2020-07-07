using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //attributes
    public float speed = 4.0f;
    //public GameObject cam;
    private SpriteRenderer playerSprite;
    public Animator animator;
    public bool facingRight;

    public float jumpForce = 300.0f;
    public float groundRadius = 0.2f;
    public Transform groundCheck;
    public bool isGrounded = false;
    public LayerMask whatIsGround;





    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        facingRight = true;
        //anim.SetTrigger("Stopped"); //chama animação        

    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FlipPlayerHorizontally(MovePlayer());
        KeepPlayerOnTrack();
        Jump();
                        
    }

    float MovePlayer()
    {
        //move player horizontally            
        float xmove = Input.GetAxis("Horizontal") * speed * Time.deltaTime; // Variavel Vertical
        
        animator.SetFloat("Speedx", Mathf.Abs(xmove)); //add speed to invoke player walking animation


        //move player vertically
        float ymove = Input.GetAxis("Vertical") * speed * Time.deltaTime; //Variavel vertical
        
        if (ymove > 0f || ymove < 0f)
        {
        
            animator.SetFloat("Speedx", Mathf.Abs(ymove));
        }
        


        //apply movement to the player
        transform.Translate(xmove, ymove, 0);

        

        return xmove;        
        
        /*if(Input.GetKeyDown(KeyCode.A))
        {   
            mySpriteRenderer.flipX = false;
            while (Input.GetKeyDown(KeyCode.A))
            {                         
                anim.SetTrigger("Walking"); //chama animação
            }            
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            mySpriteRenderer.flipX = true;
            anim.SetTrigger("Walking"); //chama animação
        }*/



        /*if (horizontal > 0)
        {
            anim.SetTrigger("Walking"); //chama animação
            mySpriteRenderer.flipX = false;            
        }
        else if (horizontal < 0)
        {            
            mySpriteRenderer.flipX = true;
            anim.SetTrigger("Walking");
        }
        else
            anim.SetTrigger("Stopped");*/
        

        

        //if (vertical > 0) anim.SetTrigger("Walking"); //chama animação
        //else anim.SetTrigger("Stopped");
        
    }

    void FlipPlayerHorizontally(float xPosition)
    {
        if ((xPosition > 0 && !facingRight) || (xPosition < 0 && facingRight))        
        {
            facingRight = !facingRight;            
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1; 
            transform.localScale = playerScale;
        }
    }


    void KeepPlayerOnTrack()
    {      
        //prevent player from leaving track vertically
        if (transform.position.y > -1.688f || transform.position.y < -3.156f)
        {
            float yPosition = Mathf.Clamp(transform.position.y, -3.156f, -1.688f);
            transform.position = new Vector2(transform.position.x, yPosition);
        }
    }

    void Jump()
    {
       
        
        //attributes       
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (Input.GetKeyDown("space"))
        {
            
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = (Vector2.up * jumpForce);
            //rb.AddForce(Vector3.up * jumpForce); 
        }
        
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        //if (jump && isGrounded)
        //{
            //GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce);
        //}

    }

}
