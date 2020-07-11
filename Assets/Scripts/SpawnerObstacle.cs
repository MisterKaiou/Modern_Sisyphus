using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnerObstacle : MonoBehaviour
{
    //Attributes
    public GameObject obstacle1, obstacle2; //to receive objects to be spawned
    public float obstacleHeigh; //spawned object heigh
    public float rateSpawn; //frequency of the spawned objects
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
        //posX = transform.position.x - player.position.x;

        //get camera bounds size
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        //make spawner object walk 2 positions in front of the camera bound
        transform.position = new Vector2(screenBounds.x + 2f, obstacleHeigh);        
    }   

    void AddObstacle()
    {        
        Vector2 spawnPoint;

        //choose a spawn point to add the enemy randomly       
        var obstacleNumber = Random.Range(startRange, endRange + 1); //+1 to consider last number of the range in the random selection

        if (deployedObstacle == false)  
        {     
            if (obstacleNumber == 1)
            {
                spawnPoint = new Vector2(transform.position.x, obstacleHeigh);
                Instantiate(obstacle1, spawnPoint, Quaternion.identity);
                
            } 
            else
            {
                spawnPoint = new Vector2(transform.position.x + obstacleNumber, obstacleHeigh);
                Instantiate(obstacle2, spawnPoint, Quaternion.identity);
            } 

            deployedObstacle = true;
        }        
               
    }
}
