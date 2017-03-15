using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Score3 : MonoBehaviour {

	//the game's current speed
	float timeScale = 5.0f;

	public Text scoreText;
	public float score;
	public float trueScore;

	// Use this for initialization
	void Start () {
		score = 10000; //Change later to adjust for level transitions
		trueScore = 10000;
		scoreText = GetComponent<Text>();
		timeScale = 5.0f;
		Time.timeScale = timeScale;
	}

	// Update is called once per frame
	void Update () {
		//Gradually speed things up!
		if (timeScale < 10) {
			timeScale += Time.deltaTime * 0.02f;
		}

		Time.timeScale = timeScale;

		score += (timeScale * Time.deltaTime) * 100f;
		trueScore = Mathf.Ceil(score);

		UpdateScore ();

		if (trueScore >= 20000 && SceneManager.GetActiveScene ().buildIndex == 4) {
			SceneManager.LoadScene (5);
		}

		if (Input.GetKey (KeyCode.N)) {
			SceneManager.LoadScene (5);
		}
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + trueScore.ToString();
		//print (scoreText.text);
	}
}
