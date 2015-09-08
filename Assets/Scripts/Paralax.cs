
// Xx - Keep Out (incompleto,manter distancia) - xX

using UnityEngine;
using System.Collections;

public class Paralax : MonoBehaviour {

	public float spd_dwn;
	PlayerMove plr_mov;

	// Use this for initialization
	void Start () {
		plr_mov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x + (plr_mov.hsp/spd_dwn) *-1,transform.position.y,transform.position.z);
	}
}
