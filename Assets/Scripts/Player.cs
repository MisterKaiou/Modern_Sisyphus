using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    //player attributes
    public float speed = 4.0f;
    public bool facingRight;
    private Rigidbody2D rb2d;

    public float startPosition;
    private float travelDistance = 0f;

    public Text distanceUI;


    //animation attributes    
    private SpriteRenderer playerSprite;
    public Animator animator;
    public GameObject suitcaseObject;
    public bool getSuitcase = false;

    
    //jump attributes    
    public float groundRadius = 0.2f;
    public Transform groundCheck;
    public bool isGrounded = false;
    public LayerMask whatIsGround;
    public float jumpForce = 80.0f;


    //suitcase controller attributes    
    public float firstCheckpoint;
    public int checkpointInterval = 10;   

    private float suitcaseFirstPosition = 0f, suitcaseLostPosition = 0f;


    void SuitcaseController(float walkingDistance)
    {       
        print("Walking distance: " + walkingDistance); 

        //define new suitcase checkpoint
        if (walkingDistance >= (firstCheckpoint + checkpointInterval))
        {
            firstCheckpoint += checkpointInterval;
        }
   
      
        //generate suitcase lost position only if travel distance is le
        //if (walkingDistance < (firstCheckpoint + checkpointInterval) && suitcaseLostPosition == 0f)
        //{
        
        //define distance to lost the suitcase
        if (suitcaseLostPosition == 0f)
        {

            suitcaseLostPosition = firstCheckpoint + Random.Range(1, checkpointInterval + 1);
            print("Posicao de perda da maleta: " + suitcaseLostPosition);

        }

        //}

        //lose suitcase when reach suitcase lose position
        if (walkingDistance >= suitcaseLostPosition)
        {
            animator.SetBool("isSuitcase", false);                             
            getSuitcase = false;
            suitcaseLostPosition = 0f;

        }


        if (walkingDistance <= 5f && getSuitcase == false)
        {
            suitcaseObject.SetActive(true);
        }
           
                
        /*if (suitcaseLostPosition > 0)// && suitcaseLostPosition < (firstCheckpoint + checkpointInterval))
        {

            if (walkingDistance >= suitcaseLostPosition && walkingDistance < (firstCheckpoint + checkpointInterval))
            {

                print("Posicao de retorno da maleta: " + firstCheckpoint);
                animator.SetBool("isSuitcase", false);
                        
                //suitcaseLostPosition = 0f;
                getSuitcase = false;
            }
            else
            {
                firstCheckpoint += checkpointInterval;
                print("Proximo checkpoint: " + firstCheckpoint);             
                       
            }                
                    
        }          

   
        if (walkingDistance <= (suitcaseFirstPosition + 5f) && getSuitcase == false)
        {
                            
            suitcaseObject.SetActive(true); 
            //suitcaseObject.transform.Translate(firstCheckpoint, suitcaseObject.transform.position.y, suitcaseObject.transform.position.z);
                            
        } */
      
    }


    // Start is called before the first frame update
    void Start()
    {
        //store initial suitcase position on X axis which is 0
        firstCheckpoint = suitcaseObject.transform.position.x;
        suitcaseFirstPosition = suitcaseObject.transform.position.x;

        print("Posicao inicial da maleta: " + suitcaseFirstPosition);

        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        facingRight = true;            

    }

    void Update()
    {        
        //if (getSuitcase == true)
        //{
            //suitcaseController receives player travel distance calculated
        //    SuitcaseController(CalculateTravelDistance());            
        //}
        SuitcaseController(CalculateTravelDistance()); 
        Jump();
    }

    void Awake()
    {
        rb2d = transform.GetComponent<Rigidbody2D>(); //store player rigidbody2d component 
        startPosition = transform.position.x; //store player start position
    }
    
    void FixedUpdate()
    {
        FlipPlayerHorizontally(MovePlayer());
                   
        //}

                
        //KeepPlayerOnTrack();      
                                    
    }

    public float CalculateTravelDistance()
    {           
        travelDistance = transform.position.x - startPosition;
        //distanceUI.text = $"Distance: {(transform.position.x - startPosition):##.##}";   
        distanceUI.text = $"Distance: {(travelDistance):##.##}";

        return travelDistance;      
    }

    float MovePlayer()
    {
        //move player horizontally            
        float xmove = Input.GetAxis("Horizontal") * speed * Time.deltaTime; // Variavel Vertical
        
        animator.SetFloat("Speedx", Mathf.Abs(xmove)); //add speed to invoke player walking animation

        //move player vertically
        float ymove = Input.GetAxis("Vertical") * speed * Time.deltaTime; //Variavel vertical         
        
        transform.Translate(xmove, ymove, 0);        

        return xmove;  
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

    /*void KeepPlayerOnCameraViewZone()
    {      
        
        //prevent player from leaving track vertically
        if (transform.position.x > -8f || transform.position.y < 8f)
        {
            float xPosition = Mathf.Clamp(transform.position.x, -8f, 8f);
            transform.position = new Vector2(xPosition, transform.position.y);
        }
    }*/

    /*void KeepPlayerOnTrack()
    {      
        //prevent player from leaving track vertically
        if (transform.position.y > -1.688f || transform.position.y < -3.156f)
        {
            float yPosition = Mathf.Clamp(transform.position.y, -3.156f, -1.688f);
            transform.position = new Vector2(transform.position.x, yPosition);
        }
    }*/

    void Jump()
    {    

        //check if groundCheck object is touching the ground and return true        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);             

        //jump only if space button is pressed and player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {               
            rb2d.AddForce(Vector2.up * jumpForce);
            animator.SetBool("isJumping", true);           
        }
        else
        {        
            //disable animation on landing to the ground           
            PlayerOnLanding();          
        }   
        
    }

    public void PlayerOnLanding()
    {
        //disable animation on landing to the ground
        animator.SetBool("isJumping", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "suitcaseTag" && getSuitcase == false)
        {
            //suitcase.transform.SetParent(this.transform);
            animator.SetBool("isSuitcase", true);
            getSuitcase = true;
            other.gameObject.SetActive(false);            
        }
    } 

}
