using UnityEngine;
using System.Collections;

public class CooldownBubble : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SelfDelete(){
         Destroy (gameObject, 0.0f); 
	}
}
