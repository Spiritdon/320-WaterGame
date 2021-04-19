using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shredder : MonoBehaviour
{


	private void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.gameObject.CompareTag("Player"))
		{
			//Reload the current scene
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
