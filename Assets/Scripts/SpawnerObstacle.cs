using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnerObstacle : MonoBehaviour
{
    //Attributes
    public GameObject obstacle1, obstacle2; 
    public float obstacleHeigh;
    public float rateSpawn;
    public int maxObstacle;    
    private float currentRateSpawn;    

    public int startRange = 1; //to be used to select enemy randomly
    public int endRange = 2; //to be used to select enemy randomly   

    public float spawnTime; //to control interval between obstacle creation      

    private float posX; 

    public bool deployedObstacle = false;

    

    private Vector2 screenBounds;

    



    // Start is called before the first frame update
    void Start()
    {    
       
        InvokeRepeating("AddObstacle", rateSpawn, spawnTime); 
               
    }

    // Update is called once per frame
    void Update()
    {
        //variable to store distance between player and spawned object     
        //posX = transform.position.x - player.position.x;       
        //get camera bounds size
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        transform.position = new Vector2(screenBounds.x + 2f, obstacleHeigh);        
        
    }   

    void AddObstacle()
    {        
                
        //choose a spawn point to add the enemy randomly
        var spawnPoint = new Vector2(transform.position.x, obstacleHeigh);
        var obstacleNumber = Random.Range(startRange, endRange + 1); //+1 to consider last number of the range in the random selection

        if (deployedObstacle == false)  
        {     
            if (obstacleNumber == 1)
            {
                Instantiate(obstacle1, spawnPoint, Quaternion.identity);
                
            } 
            else
            {
                Instantiate(obstacle2, spawnPoint, Quaternion.identity);
            } 

            deployedObstacle = true;
        }        
               
    }
}
