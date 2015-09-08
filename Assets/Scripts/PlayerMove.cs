
// Esse e o script de movimentaçao do personagem (meio obvio nao acha?)

using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	// Publics

	public float mov_speed;
	public float jmp_speed;
	public float max_speed;
	public float max_height;
	public bool can_move;
	public bool cinematic;
	public bool from_axis;
	public bool flying;
	public bool[] side = new bool[] {false,false};
	public string Importante;
	public float move_x;
	public float move_y;
	public float hsp;
	public Transform drag;



	// Privates
	private bool OnGround;
	private bool MaxDistance;
	private bool canFly;
	private float max_fall = -10.0f;
	private float key_left;
	private float key_right;
	private float stp;
	private float resize;
	private float[] timer = new float[] {0,0};
	private int lerp_fix;


	//PlayerAnimation plr_anim;
	balao bln;

	// Use this for initialization
	void Start () {
		if (!cinematic) {
			can_move = true;
		}
		hsp = 0;
		move_x = 0;
		move_y = 0;
		//plr_anim = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>();
		bln = GameObject.FindGameObjectWithTag("plr_bln").GetComponent<balao>();
		resize = 0.8f;
	}

	// Update is called once per frame
	void Update () {
		
		// Checando Colisões
		OnGround = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f,-5);
		MaxDistance = Physics2D.Raycast(transform.position, -Vector2.up, max_height);

		// Flutuar (se estiver no chao,apertar barra de espaço e poder se mover)
		if (can_move) {
			TheFlight ();
		}
		// Movimentaçao Horizontal

		// Define As Condiçoes Para Andar
		if (can_move) {
			if (flying) {
				side [0] = ((Input.GetKey (KeyCode.A)) || (Input.GetKey (KeyCode.LeftArrow)) && (bln.transform.position.x - 1f < transform.position.x));
				side [1] = ((Input.GetKey (KeyCode.D)) || (Input.GetKey (KeyCode.RightArrow)) && (bln.transform.position.x + 1f > transform.position.x));
			} else {
				side [0] = (Input.GetKey (KeyCode.A)) || (Input.GetKey (KeyCode.LeftArrow));
				side [1] = (Input.GetKey (KeyCode.D)) || (Input.GetKey (KeyCode.RightArrow));
			}
		} else {
			if (!cinematic){
				side [0] = false;
				side [1] = false;
			}
		}

		// Chama a Funçao De Movimento
		TheAxis ();

		// velocidade maxima de queda
		move_y = rigidbody2D.velocity.y;
		if (move_y < max_fall) {
			rigidbody2D.velocity = new Vector2 (0, max_fall);
		}
	}

	// Funçao De Levitaçao

	void TheFlight() {
		if (OnGround) {
			canFly = true;
		} else if (!flying) {
			canFly = false;
			rigidbody2D.AddForce (new Vector2 (0,jmp_speed/2));
		}
		if (canFly) {
			if (Input.GetKey(KeyCode.Space)){
				timer[1] = Mathf.Lerp (timer[1],1f,0.4f);
				if (timer[1] >= 0.999f){
					timer[1] = 1;
				}
				if (timer[1] == 1f){
					flying = true;
				}
				drag.transform.position = new Vector2 (transform.position.x + (1.5f * transform.localScale.x),transform.position.y + 2.24f);
				resize = Mathf.Lerp(resize,2,0.05f);
				if (resize >= 1.199f){
					resize = 1.2f;
				}
				bln.transform.localScale = new Vector2 (resize * transform.localScale.x,resize + 0.3f);
				if (move_x != 0){
					timer[0] = Mathf.Lerp (timer[0],1f,0.4f);
					if (timer[0] >= 0.999f){
						timer[0] = 1;
					}
					bln.rigidbody2D.AddForce (new Vector2 (300f * transform.localScale.x, 150f));
				} else {
					timer[0] = 0f;
				}
			} else {
				timer [0] = 0;
				timer [1] = 0;
				flying = false;
				drag.transform.position = new Vector2 (transform.position.x + (-1f * transform.localScale.x),transform.position.y + 1.24f);
				resize = Mathf.Lerp(resize,0.8f,0.05f);
				if (resize <= 0.899f){
					resize = 0.8f;
				}
				bln.transform.localScale = new Vector2 (resize * transform.localScale.x,resize + 0.2f);
				bln.rigidbody2D.AddForce (new Vector2 (0, 150f));
			}
		}
		if (flying){
			if (MaxDistance){
				rigidbody2D.AddForce (new Vector2 (0,jmp_speed));
			} else {
				stp = move_y;
				stp = Mathf.Lerp(stp,0,0.15f);
				if (stp < 0.001f){
					rigidbody2D.velocity = new Vector2 (0,0);
				} else {
					rigidbody2D.velocity = new Vector2 (0,stp);
				}
			}
		}
	}

	// Funçao De Movimentaçao Horizontal

	void TheAxis(){

		// Pegando os botoes pressionados
		if (side[0]){
			key_left = -1;
			lerp_fix = -1;
		} else {
			key_left = 0;
		}
		if (side[1]){
			key_right = 1;
			lerp_fix = 1;
		} else {
			key_right = 0;
		}

		move_x = key_left + key_right;
		if (move_x != 0){
			if (move_x > 0){

				// evitando deslise
				if (hsp < 0){
					hsp = 0;
					timer[0] = 0f;
				}
				// aki poderia ser resolvido com outra coisa,mas nao vem em minha mente
				if (hsp < max_speed){
					hsp += mov_speed;
				} else {
					hsp = max_speed;
				}
			}
			if (move_x < 0){

				// evitando deslise
				if (hsp > 0){
					hsp = 0;
					timer[0] = 0f;
				}
				// aki poderia ser resolvido com outra coisa,mas nao vem em minha mente #2
				if (hsp > -max_speed){
					hsp -= mov_speed;
				} else {
					hsp = -max_speed;
				}
			}
		} else {
			hsp = Mathf.Lerp(hsp,0,0.15f);
			if (lerp_fix > 0){
				if (hsp < 0.0001f){
					hsp = 0;
					lerp_fix = 0;
				}
			}
			if (lerp_fix < 0){
				if (hsp > -0.0001f){
					hsp = 0;
					lerp_fix = 0;
				}
			}
		}

		if ((flying) && (timer[0] == 1f)) {
			transform.position = new Vector2 (transform.position.x + hsp / 15, transform.position.y);
		} else if ((OnGround) || (!canFly)){
			transform.position = new Vector2 (transform.position.x + hsp / 10, transform.position.y);
		}
	}
}
