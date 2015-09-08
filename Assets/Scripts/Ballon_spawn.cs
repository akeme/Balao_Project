/// <summary>
/// Spanw Ballon Dialogue
/// </summary>
using UnityEngine;
using System.Collections;

public class Ballon_spawn : MonoBehaviour {

	private float direction;
	private float resize;
	private Transform self_parent;
	private float begin_time;

	// Use this for initialization
	void Start () {
		resize = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		resize = Mathf.Lerp(resize,1,0.05f);
		if (resize >= 0.999f){
			resize = 1;
		}
		transform.localScale = new Vector2 (resize*-1,resize);
	}
}
