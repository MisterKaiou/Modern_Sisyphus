using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    //attributes

    //Transform player;
    public float parallaxEffect; //to configure how much effect

    private float length, startPosition; //for the sprite
    public GameObject cam;
   

    // Start is called before the first frame update
    void Start()
    {
        //locate game object player with tag playerTag and get component transform
        //player = GameObject.FindGameObjectWithTag("playerTag").transform;

        startPosition = transform.position.x; //get player initial position on X axis


        //startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;        
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundParallax();


            /*float temp = (cam.transform.position.x * (1 - parallaxEffect));
            float distance = (cam.transform.position.x * parallaxEffect); //how far we travelled
            transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
        
            if (temp > startPosition + length) startPosition += length;
            else if (temp < startPosition - length) startPosition -= length;   */   

       
    }

    void BackgroundParallax()
    {
        /*transform.Translate((player.position.x - startPosition) / parallaxEffect, 0, 0);
        startPosition = player.position.x;*/

        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect); //how far we travelled
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
        
        if (temp > (startPosition + length)) startPosition += length;
        else if (temp < (startPosition - length)) startPosition -= length;
    }
}
