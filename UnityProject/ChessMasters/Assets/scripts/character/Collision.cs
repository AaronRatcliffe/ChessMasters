using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D collider) {
		//Debug.Log ("Player Hit");
        //playing the roguehurt animation
        GetComponent<Animator>().SetInteger("State", 2);
		StartCoroutine(DeathWait());
    }

	private IEnumerator DeathWait()
	{
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene("Main_Menu");
	}
}