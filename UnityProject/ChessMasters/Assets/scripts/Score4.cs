using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Score4 : MonoBehaviour {

	//the game's current speed
	float timeScale;

	public Text scoreText;
	public float score;
	public float trueScore;

	// Use this for initialization
	void Start () {
		score = 15000; //Change later to adjust for level transitions
		trueScore = 15000;
		scoreText = GetComponent<Text>();
		Time.timeScale = 3.0f;
		timeScale = Time.timeScale;
	}

	// Update is called once per frame
	void Update () {
		
		//Gradually speed things up!
		if (timeScale < 4) {
			timeScale += Time.deltaTime * 0.04f;
		}

		Time.timeScale = timeScale;

		score += (timeScale * Time.deltaTime) * 100f;
		trueScore = Mathf.Ceil(score);

		UpdateScore ();

		if (Input.GetKey (KeyCode.N)) {
			SceneManager.LoadScene(0);
		}
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + trueScore.ToString();
		//print (scoreText.text);
	}
}
