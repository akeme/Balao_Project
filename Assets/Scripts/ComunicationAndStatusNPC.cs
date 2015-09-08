/*Criado: cristtiano de souza pinto cristtianodesouza@gmail.com e Pedro Henrique haroukino@hotmail.com
	* 31/08/2015
		* Gerencia do dialogo e status do personagem com os NPCS.
		* Gerenciar o valor de status que o player possui.
		* O valor de status e um inteiro de 1 a 6. Os valores seguem as seguintes representaçoes:
		* violet: 1
		* blue: 2
		* green: 3
		* yellow:4
		* orange:5
		* red:6.
		*O NPC seguira a regra de assumir o valor de status da cor de maior valor. Exemplo: NPC com balao da cor azul tera status 2. Npc com balao da cor verde e amarelo tera status =3.
		*/
using UnityEngine;
using System.Collections;

public class ComunicationAndStatusNPC : MonoBehaviour {
	// Publics
	public int stats_value;// Valo De Status Do NPC
	public Sprite ballon_sprite; // Simbolo De Balao Do NPC
	public Sprite[] judge; // Sprites <Diferença> <Igualdade> <Negaçao> <Aceitaçao>
	public Sprite ballon_pensamento; // Balao De Pensamento
	public Transform symbol_position; // Posiçao De Criaçao Dos Simbolos
	public Transform speak; // Posiçao Da <Negaçao> ou <Aceitaçao>
	public Transform plr_pos; // Posiçao Do Jogador No Momento Do Dialogo
	public GameObject thinking; // Objeto Criado Para Mostrar Que O Jogador Pode Interagir Com o NPC
	public bool turn;
	public bool acptin;
	public bool interaction;
	public bool plr;

	// Privates {Sprite,bool,float, <Scripts>}
	// Sprites
	Sprite lst_jdg; // Sprite (Igual) ou (Diferente)
	Sprite dread_jdg; // Sprite (Aceitaçao) ou (Negaçao)

	// Bools
	bool can_go;
	bool comunication; // Verifica Se o Jogador Esta No Campo De Interaçao
	bool start_dial; // O Dialogo Ter Inicio
	bool acpt; // Aceitaçao
	bool[] create = new bool[] {false,false,false,false,false}; // bools Usadas Para Bloco De Criaçao De Objetos

	// Floats
	float tempo_criaçao; // Tempo Da Criaçao Do Balao De Dialogo
	float tempo_intervalo = 1.5f; // Auto Explicatorio...

	// Scripts
	SpriteRenderer plr_spr;
	ComunicationAndStatusPlayer plr_bln;
	PlayerMove plr_mov;
	//NPCAnim anima;
	//CutSceneControl cntrl;

	// Use this for initialization
	void Start () {
		// Inicializando Valor Das Variaveis Publicas Somente Para Comunicaçao (assim evitando erros)
		comunication = false;
		start_dial = false;

		// Importanto Componentes (<Scripts>,<SpriteRenderer>)
		plr_spr = GameObject.FindGameObjectWithTag("plr_bln").GetComponent<SpriteRenderer>();
		plr_bln = GameObject.FindGameObjectWithTag("Player").GetComponent<ComunicationAndStatusPlayer>();
		plr_mov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
		//cntrl = GameObject.Find ("Main Camera").GetComponent<CutSceneControl> ();
		//anima = GetComponent<NPCAnim>();
	}
	
	// Update is called once per frame
	void Update () {
		// Interaçao Do Jogador Com o NPC Apartir Dos Botoes <UpArrow> e <W> (Duvidas Checar Para Que Cada Variavel Serve Em Suas Criaçoes)
		if (!plr_mov.cinematic) {
			interaction = (Input.GetKeyDown (KeyCode.W)) || (Input.GetKeyDown (KeyCode.UpArrow));
		} else {
			interaction = plr_bln.Comunicate;
		}
		if ((!start_dial) && (comunication) && (interaction)) {
			if (!acpt){
				// Caso Exista Algum Objeto Com a Tag ("Icon") Sera Deletado -- Inicio...
				GameObject[] ballon_icon =  GameObject.FindGameObjectsWithTag("Icon");
				foreach (GameObject icon in ballon_icon){
					Destroy (icon);
				}
				// -- Fim!

				// Definimos o Tempo De Criaçao Do Balao De Dialogo e Impossibilitamos o Jogador De Andar -- Inicio...
				if (!plr_mov.cinematic) {
					plr_mov.transform.position = new Vector2 (plr_pos.position.x , plr_mov.transform.position.y);
				}
				plr_mov.can_move = false;
				plr_mov.hsp = 0;
				// -- Fim!

				// Criaçao Do Balao De Dialogo (esse pedaço de codg. e usado varias vezes,vou explicar somente nesse o que cada coisa faz) -- Inicio...
				tempo_criaçao = Time.time;
				GameObject obj_0 = new GameObject(); // Cria Um GameObject Novo
				obj_0.AddComponent<SpriteRenderer>(); // Adiciona o Componente <SpriteRenderer>
				obj_0.AddComponent<Ballon_spawn>(); // Adiciona o Componente <Ballon_spawn> (que no caso e um script)
				obj_0.GetComponent<SpriteRenderer>().sprite = ballon_pensamento; // Mudamos o Sprite Do Objeto
				obj_0.transform.parent = transform; // Criamos o Objeto(obj_0 -- Vulgo Balao de Dialogo) dentro desse (NPC)
				obj_0.transform.position = new Vector2(transform.position.x,transform.position.y + 0.8f); // Definimos Sua Posiçao
				obj_0.transform.localScale = new Vector2 (0,0); // Definimos Sua Escala (Vulgo Tamanho)
				obj_0.tag = "Icon"; // Damos A Tag "Icon" Para o Objeto
				obj_0.name = "Ballon Dial"; // Nomeamos o Obj
				start_dial = true; // Enfim Começamos o Dialogo
				// -- Fim!
			} else {
				if (!acptin){
					// Criaçao Do Objeto
					GameObject obj_4 = new GameObject();
					obj_4.AddComponent<SpriteRenderer>();
					obj_4.AddComponent<FadeAnimation>();
					obj_4.AddComponent<Rigidbody2D>();
					obj_4.GetComponent<SpriteRenderer>().sprite = dread_jdg;
					obj_4.transform.parent = transform;
					obj_4.transform.position = speak.transform.position;
					if (transform.localScale.x > 0){
						obj_4.transform.localScale = new Vector2 (1f,1f);
					} else {
						obj_4.transform.localScale = new Vector2 (-1f,1f);
					}
					obj_4.tag = "Icon";
					obj_4.name = "NAAAAAAAAAAAAAAH";
					acptin = true;
				}
			}
		}

		// Se Começamos o Dialogo
		if (start_dial) {
			// Criaçao Do Balao Do NPC -- Inicio...
			if(tempo_intervalo < Time.time - tempo_criaçao &&!(create[0])){
				GameObject obj_1 = new GameObject();
				obj_1.AddComponent<SpriteRenderer>();
				obj_1.GetComponent<SpriteRenderer>().sprite = ballon_sprite;
				obj_1.transform.parent = transform;
				obj_1.transform.position = new Vector2 (symbol_position.transform.position.x + (0.7f * transform.localScale.x),symbol_position.transform.position.y);
				if (transform.localScale.x > 0){ // Aqui Fazemos o Balao Nao Ficar Virado Para Direçao Errada
					obj_1.transform.localScale = new Vector2 (0.6f,0.8f);
				} else {
					obj_1.transform.localScale = new Vector2 (-0.6f,0.8f);
				}
				obj_1.tag = "Icon";
				obj_1.name = "Ballon Icon Npc";
				create[0] = true; // Aqui Asseguramos Que Nao Criara Mais de 1 (Olhe As Condiçoes De Criaçao Para Entender...)
			}
			// -- Fim!

			// Criaçao Do Balao Do Jogador -- Inicio...
			if(tempo_intervalo * 2 < Time.time - tempo_criaçao &&!(create[1])){
				GameObject obj_2 = new GameObject();
				obj_2.AddComponent<SpriteRenderer>();
				obj_2.GetComponent<SpriteRenderer>().sprite = plr_spr.sprite;
				obj_2.transform.parent = transform;
				obj_2.transform.position = new Vector2 (symbol_position.transform.position.x - (0.9f * transform.localScale.x),symbol_position.transform.position.y);
				if (transform.localScale.x > 0){
					obj_2.transform.localScale = new Vector2 (0.6f,0.8f);
				} else {
					obj_2.transform.localScale = new Vector2 (-0.6f,0.8f);
				}
				obj_2.tag = "Icon";
				obj_2.name = "Ballon Icon Player";
				create[1] = true;
			}
			// -- Fim!

			// Criaçao Do Simbolo De <Igualdade> ou <Diferença> -- Inicio...
			if(tempo_intervalo * 3 < Time.time - tempo_criaçao &&!(create[2])){
				// Definindo Qual Objeto Sera Criado
				if (stats_value - plr_bln.stats_value <= 1){ // Verificamos Se o Status Do Jogador <Menos> Status Do NPC e ((<Igual> ou <Menor>) Que ('1'))
					acpt = true;
					if (stats_value - plr_bln.stats_value == 1){ // Se For (Igual) a '1',Balao Do NPC e o Proximo Da Sequencia
						lst_jdg = judge[0];
					} else if (stats_value - plr_bln.stats_value == 0){ // Se For (Igual) a '0',Jogador e NPC Tem o Mesmo Balao
						lst_jdg = judge[1];
					} else { // (Caso Contrario) e <Menor> Que '0',Logo Nao Sao Iguais,Porem Ainda Se Aceitam
						lst_jdg = judge[0];
					}
				} else { // (Caso Contrario) e <Maior> Que '1',Entao (Nao Sao <Iguais>) e o Balao Do NPC Nao e o Proximo
					lst_jdg = judge[0];
					acpt = false;
				}

				// Criaçao Do Objeto
				GameObject obj_3 = new GameObject();
				obj_3.AddComponent<SpriteRenderer>();
				obj_3.GetComponent<SpriteRenderer>().sprite = lst_jdg;
				obj_3.transform.parent = transform;
				obj_3.transform.position = new Vector2 (symbol_position.transform.position.x - (0.1f * transform.localScale.x), symbol_position.transform.position.y);
				if (transform.localScale.x > 0){
					obj_3.transform.localScale = new Vector2 (1f,1f);
				} else {
					obj_3.transform.localScale = new Vector2 (-1f,1f);
				}
				obj_3.tag = "Icon";
				obj_3.name = "You Betrayed The LAW";
				create[2] = true;
			}
			// -- Fim!

			// Criaçao Da <Aceitaçao> ou <Negaçao> -- Inicio...
			if(tempo_intervalo * 4 < Time.time - tempo_criaçao &&!(create[3])){
				// Definindo Qual Objeto Sera Criado
				if (acpt){
					dread_jdg = judge[3];
				} else {
					dread_jdg = judge[2];
				}
				if (stats_value - plr_bln.stats_value == 1){ // Se For (Igual) a '1',Balao Do NPC e o Proximo Da Sequencia - 
					plr_bln.stats_value++; // - Aqui Acressentamos o Valor Do Jogador,Assim Mudando Para o Proximo Balao
				}

				// Criaçao Do Objeto
				GameObject obj_4 = new GameObject();
				obj_4.AddComponent<SpriteRenderer>();
				obj_4.AddComponent<FadeAnimation>();
				obj_4.AddComponent<Rigidbody2D>();
				obj_4.GetComponent<SpriteRenderer>().sprite = dread_jdg;
				obj_4.transform.parent = transform;
				obj_4.transform.position = speak.transform.position;
				if (transform.localScale.x > 0){
					obj_4.transform.localScale = new Vector2 (1f,1f);
				} else {
					obj_4.transform.localScale = new Vector2 (-1f,1f);
				}
				obj_4.tag = "Icon";
				obj_4.name = "NAAAAAAAAAAAAAAH";
				create[3] = true;
			}
			// -- Fim!

			// Terminado o Dialogo e Possibilitando Outro -- Inicio...
			if(tempo_intervalo * 5.5f < Time.time - tempo_criaçao){
				GameObject[] ballon_icon =  GameObject.FindGameObjectsWithTag("Icon");
				foreach (GameObject icon in ballon_icon){
					Destroy (icon);
				}
				// Objeto Que Mostra Se o Jogador Ainda Pode Ou Nao Interagir Com o NPC
				if (!plr_mov.cinematic){
					Instantiate (thinking,new Vector3 (transform.position.x,transform.position.y +2.5f),Quaternion.identity);
				}
				comunication = true;
				start_dial = false;
				create [0] = false;
				create [1] = false;
				create [2] = false;
				create [3] = false;
				if (!plr_mov.cinematic){
					plr_mov.can_move = true;
				}
				start_dial = false;
			}
			// -- Fim!
		}
	}

	// Chamado Quando Outro Objeto Entra Na Area De Colisao
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") { // Se Quem Entrou Na Area De Colisao Foi o Jogador
			comunication = true;
			if (!plr_mov.cinematic) {
				Instantiate (thinking,new Vector3 (transform.position.x,transform.position.y + 2.5f),Quaternion.identity); // Objeto Que Mostra Se o Jogador Ainda Pode Ou Nao Interagir Com o NPC
			}
		}
	}

	// Chamado Enquanto Outro Objeto Esta Na Area De Colisao
	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			//virar NPC para personagem:
			if (turn){
				if (other.gameObject.transform.position.x < transform.position.x) {
					transform.localScale = new Vector2 (-1, 1);
				} else {
					transform.localScale = new Vector2 (1, 1);
				}
			}
		}		
	}

	// Chamado Quando Outro Objeto Sai Da Area De Colisao
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			GameObject[] ballon_icon =  GameObject.FindGameObjectsWithTag("Icon");
			foreach (GameObject icon in ballon_icon){
				Destroy (icon);
			}
		}
		comunication = false;
		start_dial = false;
		acptin = false;
		create [0] = false;
		create [1] = false;
		create [2] = false;
		create [3] = false;
	}
}
