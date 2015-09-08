
// Esse script pode ser resumido como um simples criador de botao

using UnityEngine;
using System.Collections;

public class GUI_Controller : MonoBehaviour {

	// publics
	public GUISkin model;
	public int btn_widht;
	public int btn_heigth;
	public float btn_x;
	public float btn_y;
	public string btn_name;
	public string scene;
	public bool btn_center;

	//privates
	private bool bt_inicio;


	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		GUI.skin = model;
		if (btn_center) {
			bt_inicio = GUI.Button (new Rect(Screen.width/2 - (btn_widht/2),Screen.height/2 - (btn_heigth/2),btn_widht,btn_heigth), btn_name);
		} else {
			bt_inicio = GUI.Button (new Rect (btn_x, btn_y, btn_widht, btn_heigth), btn_name);
		}
		if (bt_inicio) {
			Application.LoadLevel(scene);
		}
	}
}
