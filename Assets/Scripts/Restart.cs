using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Restart : MonoBehaviour
{
	public void RestartSameLevel()
	{
		IsBoostedDrone.StopBoostedDrone();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
