using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

	


	void OnMouseDown()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
	}
}
