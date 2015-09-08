
// Esse e o script de animaçao,meio obvio nao?

using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	private Animator animator;
	private float mov;
	PlayerMove plr_mov;

	// Use this for initialization
	void Start () {

		// Aqui e Onde Pegamos o Animator Do Jogador,Assim Podemos Acessar Suas Variaveis
		animator = this.GetComponent<Animator> ();

		// Importa as variaveis do script 'PlayerMove'
		plr_mov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {

		// Animaçao De Pulo

		// Se a Velocidade Vertical e 0, Entao Nao Ha Animaçao De Pulo Ou De Queda.
		if ((!plr_mov.flying) && (plr_mov.move_y == 0)) {
			animator.SetBool ("Fall", false);
			animator.SetBool ("Jump", false);
		} else {
			// Se a Velocidade Vertical For Menor Que 0,Entao o Jogador Esta Caindo,Entao Queda = true Pulo = False
			if (rigidbody2D.velocity.y < 0){
				animator.SetBool ("Fall", true);
				animator.SetBool ("Jump", false);
			}
			// Se a Velocidade Vertical For Maior Que 0,Entao o Jogador Esta Pulando,Entao Queda = False Pulo = True
			if (plr_mov.flying){
				animator.SetBool ("Jump", true);
				animator.SetBool ("Fall", false);
			}
		}

		// Animaçao De Andar e Direçao Do Personagem

		// Essa Variavel Nos Diz Se o Jogador Esta Andando e Qual Direçao Ele Anda Dependendo Do Botao Que Ele Aperta

		if (plr_mov.flying) {
			mov = Input.GetAxisRaw ("Horizontal");
		} else {
			mov = plr_mov.move_x;
		}
		// Se Nao Estiver Retornando 0, Animaçao De Andar,Caso Contrario,Esta Parado
		if (mov != 0) {
			// Aqui e Onde Sabemos Para Que Lado Esta Olhando: -1 =  Esquerda, 1 = Direita
			transform.localScale = new Vector2 (mov, transform.localScale.y);
			animator.SetInteger ("Bases", 0);
		} else {
			animator.SetInteger ("Bases", 1);
		}
	}
}