using UnityEngine;
using System.Collections;

public class DEBUG_SetRankMeshText : MonoBehaviour {

    public TextMesh textMesh;
    public Unit unit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        textMesh.text = ((int) unit.Type).ToString();
	}
}
