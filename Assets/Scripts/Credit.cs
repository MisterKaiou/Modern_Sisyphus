using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Credit : MonoBehaviour
{
   public void GetBack() 
   {
	    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
   }
}
