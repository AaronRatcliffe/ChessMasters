using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Start_Game : MonoBehaviour {

	public void LoadByIndex(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}
}
