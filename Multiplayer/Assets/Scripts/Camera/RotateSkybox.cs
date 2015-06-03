using UnityEngine;
using System.Collections;

public class RotateSkybox : MonoBehaviour {

    // The speed at which the skybox rotates
    public float Speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate( Vector3.up, 1 * Speed * Time.deltaTime );   
	}
}
