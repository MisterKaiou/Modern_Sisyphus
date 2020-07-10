using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{


    //Attributes
    private Vector2 screenBounds;
    private SpawnerObstacle scSpawnerObstacle;

    public Transform player;

    private BoxCollider2D bc2d;

    // Start is called before the first frame update
    void Start()
    {
        //get camera bounds size
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        //get script component from Spawner gameobject
        scSpawnerObstacle = GameObject.Find("Spawner").GetComponent<SpawnerObstacle>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        //bc2d = this.GetComponent<BoxCollider2D>();
        //bc2d.isTrigger = true;          
    }

    // Update is called once per frame
    void Update()
    {        
        /*if (screenBounds.x > transform.position.x)
        {
            scSpawnerObstacle.deployedObstacle = false;
            Destroy(gameObject);
        }*/
    }

    //When the obstacle is out of screen it is destroyed
    void OnBecameInvisible()
    {        
        scSpawnerObstacle.deployedObstacle = false;
        Destroy(gameObject);
    }
}
