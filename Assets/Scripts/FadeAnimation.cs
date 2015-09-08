using UnityEngine;
using System.Collections;

public class FadeAnimation : MonoBehaviour {
	
	float alpha = 1;
	float r = 1;
	float g = 1;
	float b = 1;
	ComunicationAndStatusNPC npc_acpt;
	// Use this for initialization
	void Start () {
		npc_acpt = GameObject.FindGameObjectWithTag("NPC").GetComponent<ComunicationAndStatusNPC>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector2 (1 * transform.parent.localScale.x, 1);
		alpha -= 0.01f;
		rigidbody2D.AddForce (new Vector2 (0, 10f));
		this.GetComponent<SpriteRenderer> ().color = new Color(r,g,b,alpha);
		if (alpha <= 0) {
			npc_acpt.acptin = false;
			Destroy(this.gameObject);
		}
	}
}
