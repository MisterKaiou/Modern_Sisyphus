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
    public AudioClip audioJump;


    //suitcase controller attributes    
    public float firstCheckpoint;
    public int checkpointInterval = 10;   

    private float suitcaseFirstPosition = 0f, suitcaseLostPosition = 0f;


    void SuitcaseController(float walkingDistance)
    {       
        
        //float decreaseDistance = 0f;

        print("DISTANCIA VIAJADA: " + walkingDistance); 

        //define new suitcase checkpoint
        if (walkingDistance >= (firstCheckpoint + checkpointInterval) && getSuitcase == true)
        {
            firstCheckpoint += checkpointInterval;
            print("CHECKPOINT DE RETORNO: " + firstCheckpoint);
        }
   
      
        //generate suitcase lost position only if travel distance is le
        //if (walkingDistance < (firstCheckpoint + checkpointInterval) && suitcaseLostPosition == 0f)
        //{
        
        //define distance to lost the suitcase
        if (suitcaseLostPosition == 0f && getSuitcase == true)
        {
            
            suitcaseLostPosition = firstCheckpoint + Random.Range(checkpointInterval/2, checkpointInterval + 1);
            print("POSICAO PERDA MALETA: " + suitcaseLostPosition);

        }

        //}

        //lose suitcase when reach suitcase lose position
        if (walkingDistance >= suitcaseLostPosition && getSuitcase == true)
        {
            print("DISTANCIA PERDA MALETA: " + walkingDistance);
            //decreaseDistance = walkingDistance - suitcaseLostPosition;
            animator.SetBool("isSuitcase", false);                             
            getSuitcase = false;
            suitcaseLostPosition = 0f;

        }


        if (walkingDistance <= (firstCheckpoint + 6f) && getSuitcase == false)        
        {
            print("CHECKPOINT RETORNO: " + firstCheckpoint);
            suitcaseObject.SetActive(true);
            
        }         
                
        
      
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
       
        Jump();
        //KeepPlayerOnCameraViewZone();
    }

    void Awake()
    {
        rb2d = transform.GetComponent<Rigidbody2D>(); //store player rigidbody2d component 
        startPosition = transform.position.x; //store player start position
    }
    
    void FixedUpdate()
    {      
        
        FlipPlayerHorizontally(MovePlayer());
        SuitcaseController(CalculateTravelDistance());                       
          
    }

    public float CalculateTravelDistance()
    {           
        travelDistance = (transform.position.x - startPosition) + 1;        
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

        Debug.Log("VALOR DE XMOVE: " + xmove);       

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

    /*void KeepPlayerOnTrack()
    {      
        //prevent player from leaving track vertically
        if (transform.position.y > -1.688f || transform.position.y < -3.156f)
        {
            float yPosition = Mathf.Clamp(transform.position.y, -3.156f, -1.688f);
            transform.position = new Vector2(transform.position.x, yPosition);
        }
    }*/

    void RunAudioClip(AudioClip audioObject)
    {
        AudioSource.PlayClipAtPoint(audioObject, transform.position);   
    }

    void Jump()
    {    
        //check if groundCheck object is touching the ground and return true        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);             

        //jump only if space button is pressed and player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {               
            rb2d.AddForce(Vector2.up * jumpForce);
            animator.SetBool("isJumping", true);
            RunAudioClip(audioJump); 
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
            //suitcaseObject.transform.SetParent(this.transform);
            animator.SetBool("isSuitcase", true);
            getSuitcase = true;
            other.gameObject.SetActive(false);            
        }
        
    } 

}
