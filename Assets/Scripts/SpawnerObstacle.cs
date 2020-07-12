using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnerObstacle : MonoBehaviour
{
    //Attributes
    public GameObject obstacle1, obstacle2; //to receive objects to be spawned
    public float obstacleHeigh; //spawned object heigh
    public float rateSpawn = 1f; //frequency of the spawned objects
    public int maxObstacle = 3;   
    private float currentRateSpawn;    

    public int startRange = 1; //to be used to select enemy randomly
    public int endRange = 2; //to be used to select enemy randomly   

    public float spawnTime = 5f; //to control interval between obstacle creation  

    public bool deployedObstacle = false;    

    private Vector2 screenBounds;

    //public List <GameObject> objectList;

    //[SerializeField]
    public GameObject obstacleCleaner;

    public int countObjects = 0;





    // Start is called before the first frame update
    void Start()
    {    
       
        //InvokeRepeating("AddObstacle", 1, 1); 
        //objectList = new List<GameObject>(maxObstacle);
               
    }

    // Update is called once per frame
    void Update()
    {         

        //get camera bounds size
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        Debug.Log("BORDAS DA CAMERA: " + screenBounds);
        
        //make spawner object walk 2 positions in front of the camera bound
        transform.position = new Vector2(screenBounds.x + 2f, obstacleHeigh);

        //obstacleCleaner.transform.position = new Vector2(screenBounds.x - 1f, obstacleHeigh);

        //CleanObjectList();  
        if (countObjects == 0)
        {
            deployedObstacle = false;
        }     

    }   

    /*void CleanObjectList()
    {

        foreach (var i in objectList)
        {
            if (obstacleCleaner.transform.position.x > i.transform.position.x)
            {
                objectList.Remove(i);
            }

            if (objectList.Count == 0)
            {
                deployedObstacle = false;
                break;
            }
        }
    }*/

    void FixedUpdate()
    {
        //check if there are no obstacles deployed to instantiate new ones
        if (deployedObstacle == false)
        {
            for (int i = 0; i < maxObstacle; i++)
            {
                AddObstacle();

            }

            deployedObstacle = true;
        }
    }

    void AddObstacle()
    {                  
        GameObject obstacle;

        //select spawnPoint randomly to instantiate obstacles
        var spawnPoint = new Vector2(transform.position.x + Random.Range(startRange, maxObstacle + endRange), obstacleHeigh); //+1 to consider last number of the range in the random selection

        //choose a spawn point to add the enemy randomly              
        int obstacleNumber = Random.Range(startRange, endRange + 1); //+1 to consider last number of the range in the random selection               
                    
        if (obstacleNumber == 1)
        {
            //spawnPoint = new Vector2(transform.position.x, obstacleHeigh);
            obstacle = Instantiate(obstacle1, spawnPoint, Quaternion.identity);

        } 
        else
        {
            //spawnPoint = new Vector2(transform.position.x + obstacleNumber, obstacleHeigh);
            obstacle = Instantiate(obstacle2, spawnPoint, Quaternion.identity);
        }   

        countObjects++;
        //objectList.Add(obstacle);

        /*foreach (var i in objectList)
        {
            
            Debug.Log(i);
        }*/           
               
    }

}
