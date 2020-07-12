using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBarrierControl : MonoBehaviour
{

    //attributes  

    public Player scPlayer; //to access Player script
    public bool isBarrierActive;
    

    private float initialBarrierPosition;

    


    // Start is called before the first frame update
    void Start()
    {
        //initialBarrierPosition = transform.position.x;
        //isBarrierActive = true;        
        scPlayer = GameObject.Find("Player").GetComponent<Player>(); //to get player script component  
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isBarrierActive && scPlayer.transform.position.x <= 3f)
        {  
               
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
           
            isBarrierActive = true;
        }        
            

        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "playerTag" && IsPlayerHoldingSuitcase())
        {
            Debug.Log("PASSEI POR AKI");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            isBarrierActive = false;
        }        

    }

    private bool IsPlayerHoldingSuitcase()    
    {
        if (scPlayer.getSuitcase)
        {
            return true;
        }
        
        return false;
    }
}
