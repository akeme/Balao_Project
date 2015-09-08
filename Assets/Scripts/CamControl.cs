
// nesse script faremos a camera serguir objeto que marcarmos,o objeto a sermarcado esta em uma variavel publica,logo podera ser modificado no inspector


using UnityEngine;
using System.Collections;

public class CamControl : MonoBehaviour {
	public Transform target;
	public float damped;
	public bool follow;
	//public bool dial;
	float ax_y;
	float ax_x;
	Transform targeting;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (follow) {
			// aqui e onde a camera segue suavemente o objeto alvo
			ax_x = Mathf.Lerp (transform.position.x, target.position.x, Time.deltaTime * damped);
			ax_y = Mathf.Lerp (transform.position.y, target.position.y + 2, Time.deltaTime * damped);
			transform.position = new Vector3 (ax_x, ax_y, transform.position.z);
		}
	}
}
