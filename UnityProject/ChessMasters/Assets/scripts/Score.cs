using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {

	//the game's current speed
	float timeScale = 1.0f;

	public Text scoreText;
	public float score;
	public float trueScore;

	// Use this for initialization
	void Start () {
		score = 1; //Change later to adjust for level transitions
		trueScore = 1;
		scoreText = GetComponent<Text>();
		Time.timeScale = timeScale;
	}
	
	// Update is called once per frame
	void Update () {
		
		//Gradually speed things up!
		if (timeScale < 4) {
			timeScale += Time.deltaTime * 0.02f;
		}

		Time.timeScale = timeScale;

		score += (timeScale * Time.deltaTime) * 100f;
		trueScore = Mathf.Ceil(score);

		if (trueScore >= 5000 && SceneManager.GetActiveScene ().buildIndex == 2) {
			SceneManager.LoadScene(3);
		}

		UpdateScore ();

		if (Input.GetKey (KeyCode.N)) {
			score = 5000;
			trueScore = 5000;
		}
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + trueScore.ToString();
		//print (scoreText.text);
	}
}
