/*Criado: cristtiano de souza pinto cristtianodesouza@gmail.com
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
 *O player assumira o valor de status da cor de maior valor. Exemplo: player com balao violeta+azul tem status 2
 */
using UnityEngine;
using System.Collections;

public class ComunicationAndStatusPlayer : MonoBehaviour {

	public int stats_value;
	public bool Comunicate;

	NPCAnim anima;
	//ComunicationAndStatusNPC npc_dial;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "NPC") {
			/*anima = other.GetComponent<NPCAnim>();
			if (anima != null){
				GameObject.Find("Main Camera").GetComponent<CutSceneControl>().CheckIn[4] = true;
			}
			*/
		}
	}

}
