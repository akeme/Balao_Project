using UnityEngine;
using System.Collections;

public class CutSceneControl : MonoBehaviour {
	
	public bool[] CheckIn = new bool[]{false,false,false,false,false};
	public GameObject[] past;

	private float[] timer = new float[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0};
	
	GameObject bln_dial;

	PlayerMove plr_mov;
	ComunicationAndStatusPlayer plr_cmt;
	CamControl cam;

	// Use this for initialization
	void Start () {
		plr_mov = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMove> ();
		plr_cmt = GameObject.FindGameObjectWithTag ("Player").GetComponent<ComunicationAndStatusPlayer> ();
		cam = GameObject.Find ("Main Camera").GetComponent<CamControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (plr_mov.rigidbody2D.velocity.y == 0) {
			plr_mov.side[1] = true;

			if (CheckIn[0]){
				plr_mov.side[0] = false;
				plr_mov.side[1] = false;
				timer[0] = Mathf.Lerp (timer[0],1f,0.05f);
				if (timer[0] > 0.999f){
					timer[0] = 1f;
				}
				if (timer[0] == 1f){
					cam.follow = true;
					timer[1] = Mathf.Lerp (timer[1],1f,0.1f);
					if (timer[1] > 0.999f){
						timer[1] = 1f;
					}
					if (timer[1] ==  1f){
						cam.damped = 3;
						plr_mov.side[1] = true;
						//CheckIn[0] = false;
					}
				}
			}

			if (CheckIn[1]){
				plr_mov.side[0] = false;
				plr_mov.side[1] = false;
				timer[2] = Mathf.Lerp (timer[2],1f,0.05f);
				if (timer[2] > 0.999f){
					timer[2] = 1f;
				}
				if (timer[2] == 1f){
					plr_cmt.Comunicate = true;
				}
				if (plr_cmt.Comunicate == true){
					timer[3] = Mathf.Lerp (timer[3],1f,0.1f);
					if (timer[3] > 0.999f){
						timer[3] = 1f;
					}
					if (timer[3] == 1f){
						plr_cmt.Comunicate = false;
					}
				}
				bln_dial = GameObject.Find("Ballon Dial");
				if ((bln_dial == null) && (timer[3] == 1f)){
					timer[4] = Mathf.Lerp (timer[4],1f,0.1f);
					if (timer[4] > 0.999f){
						timer[4] = 1f;
					}
					if (timer[4] == 1f){
						plr_mov.side[1] =  true;
					}
				}
			}

			if (CheckIn[2]){
				plr_mov.side[0] = false;
				plr_mov.side[1] = false;
				Destroy(past[0]);
				timer[5] = Mathf.Lerp (timer[5],1f,0.05f);
				if (timer[5] > 0.999f){
					timer[5] = 1f;
				}
				if (timer[5] == 1f){
					plr_cmt.Comunicate = true;
				}
				if (plr_cmt.Comunicate == true){
					timer[6] = Mathf.Lerp (timer[6],1f,0.1f);
					if (timer[6] > 0.999f){
						timer[6] = 1f;
					}
					if (timer[6] == 1f){
						plr_cmt.Comunicate = false;
					}
				}
				bln_dial = GameObject.Find("Ballon Dial");
				if ((bln_dial == null) && (timer[6] == 1f)){
					timer[7] = Mathf.Lerp (timer[7],1f,0.1f);
					if (timer[7] > 0.999f){
						timer[7] = 1f;
					}
					if (timer[7] == 1f){
						plr_mov.side[1] =  true;
					}
				}
			}

			if (CheckIn[3]){
				plr_mov.side[0] = false;
				plr_mov.side[1] = false;
				Destroy(past[1]);

				timer[8] = Mathf.Lerp (timer[8],1f,0.05f);
				if (timer[8] > 0.999f){
					timer[8] = 1f;
				}
				if (timer[8] == 1f){
					plr_cmt.Comunicate = true;
				}
				if (plr_cmt.Comunicate == true){
					timer[9] = Mathf.Lerp (timer[9],1f,0.1f);
					if (timer[9] > 0.999f){
						timer[9] = 1f;
					}
					if (timer[9] == 1f){
						plr_cmt.Comunicate = false;
					}
				}
				bln_dial = GameObject.Find("Ballon Dial");
				if ((bln_dial == null) && (timer[9] == 1f)){
					timer[10] = Mathf.Lerp (timer[10],1f,0.1f);
					if (timer[10] > 0.999f){
						timer[10] = 1f;
					}
					if (timer[10] == 1f){
						Application.LoadLevel("omg");
					}
				}
			}
		}
	}
}
