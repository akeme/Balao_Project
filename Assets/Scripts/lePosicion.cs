using UnityEngine;
using System.Collections;

public class lePosicion : MonoBehaviour {
	public bool[] order = new bool[] {false,false,false,false};
	public int n;
	public Transform ini_pos;
	CutSceneControl cntrl;
	// Use this for initialization
	void Start () {
		cntrl = GameObject.Find ("Main Camera").GetComponent<CutSceneControl> ();
		if (ini_pos != null) {
			transform.position = ini_pos.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			if (order [n] == true){ 
				cntrl.CheckIn[n] = true;
			}
			Destroy (this.gameObject);
		}
	}
}
