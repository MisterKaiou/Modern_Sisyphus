﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{

    //attributes
    //public GameObject player; //to receive player object
    

    // Start is called before the first frame update
    void Start()
    {       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate()
    {
        
        
        //add to the camera position the player position on x axis
        //transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        
    }

    
}
