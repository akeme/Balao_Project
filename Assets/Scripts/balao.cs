using UnityEngine;
using System.Collections;

public class balao : MonoBehaviour {
	public Sprite[] ballons_sprite;
	public bool plr;
	ComunicationAndStatusPlayer plr_bln;
	// Use this for initialization
	void Start () {
		plr_bln = GameObject.FindGameObjectWithTag("Player").GetComponent<ComunicationAndStatusPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		rigidbody2D.AddForce (new Vector2 (0, 150f));
		if (plr){
			GetComponent<SpriteRenderer>().sprite = ballons_sprite[plr_bln.stats_value];
		}
	}
}
