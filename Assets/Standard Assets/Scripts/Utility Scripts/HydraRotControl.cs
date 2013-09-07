using UnityEngine;
using System.Collections;

public class HydraRotControl : MonoBehaviour {
	
	GameObject handL;
	GameObject handR;
	
	Vector3 lPrev;
	Vector3 rPrev;
	
	Vector3 rotSpeed;
	
	// Use this for initialization
	void Start () {
		handL = GameObject.Find("Left Hand");
		handR = GameObject.Find("Right Hand");
		rotSpeed = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (handL == null)
			handL = GameObject.Find("Left Hand");
		if (handR == null)
			handR = GameObject.Find("Right Hand");
		
		// Material mat = (Material) gameObject.GetComponent<Material>();
		Vector3 l = handL.transform.position;
		Vector3 r = handR.transform.position;
		
		bool move = false;
		
		if (gameObject.collider.bounds.Contains(handL.transform.position) != gameObject.collider.bounds.Contains (handR.transform.position)) {
			move = true;
			Vector3 v1 = gameObject.transform.rotation * (l - lPrev);
			Vector3 v2 = gameObject.transform.rotation * (l - gameObject.transform.position);
			rotSpeed = Vector3.Cross(v1, v2);
		}
		
		gameObject.renderer.material.color = new Color(1, 1 - rotSpeed.magnitude / 5,
			1 - rotSpeed.magnitude / 5, gameObject.renderer.material.color.a);
		
		lPrev = l;
		rPrev = r;
		
		gameObject.transform.Rotate(rotSpeed);
		rotSpeed = rotSpeed * .7f;
		//rotSpeed = new Vector3(0, 1, 0);
	}
}
