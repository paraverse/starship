using UnityEngine;
using System.Collections;

public class HydraRotControl : MonoBehaviour {
	
	GameObject handL;
	GameObject handR;
	
	Vector3 lPrev;
	Vector3 rPrev;
	
	Vector3 rotSpeed;
	
	bool lDown = false;
	bool rDown = false;
	
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
		
		bool lCollide = gameObject.collider.bounds.Contains(handL.transform.position);
		bool rCollide = gameObject.collider.bounds.Contains(handR.transform.position);
		
		SixenseInput.Controller lControl = SixenseInput.GetController(SixenseHands.LEFT);
		SixenseInput.Controller rControl = SixenseInput.GetController(SixenseHands.RIGHT);
		
		lDown = (lCollide || lDown) && lControl.GetButton(SixenseButtons.TRIGGER);
		rDown = (rCollide || rDown) && rControl.GetButton(SixenseButtons.TRIGGER);
		
		if (lDown && rDown) {
			Vector3 middle = (l + r) * .5f;
			Vector3 axis = middle - gameObject.transform.position;
			Vector3 angle = Vector3.Cross((rPrev - lPrev), (r-l)) * 200 / (middle.magnitude);
			//axis.Normalize();
			//axis.Scale(new Vector3(angle, angle, angle));
			rotSpeed = angle;
		} else if (lDown) {
			Vector3 v1 = (l - lPrev);
			Vector3 v2 = (l - gameObject.transform.position);
			lDown = true;
			rDown = false;
			rotSpeed = Vector3.Cross(v2, v1) / v2.magnitude;
		} else if (rDown) {
			Vector3 v1 = (r - rPrev);
			Vector3 v2 = (r - gameObject.transform.position);
			rDown = true;
			lDown = false;
			rotSpeed = Vector3.Cross(v2, v1) / v2.magnitude;
		}
		
		gameObject.renderer.material.color = new Color(1, 1 - rotSpeed.magnitude / 5,
			1 - rotSpeed.magnitude / 5, gameObject.renderer.material.color.a);
		
		lPrev = l;
		rPrev = r;
		
		//gameObject.transform.Rotate(rotSpeed);
		gameObject.transform.RotateAround(rotSpeed, rotSpeed.magnitude / 10);
		rotSpeed = rotSpeed * .7f;
		//rotSpeed = new Vector3(0, 1, 0);
	}
}
