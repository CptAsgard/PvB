using UnityEngine;
using System.Collections;

public class MainMenuCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.RotateBy( gameObject, iTween.Hash( "y", 0.05f, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.easeInOutSine, "space", Space.World, "speed", 1f) );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
