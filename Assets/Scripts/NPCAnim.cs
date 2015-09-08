using UnityEngine;
using System.Collections;

public class NPCAnim : MonoBehaviour {
	ComunicationAndStatusNPC npc_dial;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		npc_dial = GetComponent<ComunicationAndStatusNPC>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!npc_dial.plr) {
			anim.SetBool ("Runin", true);
		} else {
			anim.SetBool ("Runin", false);
		}
	}
}
