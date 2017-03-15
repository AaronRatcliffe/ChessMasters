using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Score2 : MonoBehaviour {

	//the game's current speed
	float timeScale = 3.0f;

	public Text scoreText;
	public float score;
	public float trueScore;

	// Use this for initialization
	void Start () {
		score = 5000; //Change later to adjust for level transitions
		trueScore = 5000;
		scoreText = GetComponent<Text>();
		timeScale = 3.0f;
		Time.timeScale = timeScale;
	}

	// Update is called once per frame
	void Update () {
		//Gradually speed things up!
		if (timeScale < 4) {
			timeScale += Time.deltaTime * 0.03f;
		}

		Time.timeScale = timeScale;

		score += (timeScale * Time.deltaTime) * 100f;
		trueScore = Mathf.Ceil(score);

		if (Input.GetKey (KeyCode.N)) {
			score = 15000;
			trueScore = 15000;
		}

		UpdateScore ();

		if (trueScore >= 15000 && SceneManager.GetActiveScene ().buildIndex == 3) {
			SceneManager.LoadScene(5);
		}
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + trueScore.ToString();
		//print (scoreText.text);
	}
}
