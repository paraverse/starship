using UnityEngine;
using System.Collections;

public class HydraSizeControl : MonoBehaviour {
	
	GameObject handL;
	GameObject handR;
	
	Vector3 lPrev;
	Vector3 rPrev;
	
	float scaleSpeed;
	float totalScale;
	
	bool lDown = false;
	bool rDown = false;
	
	// Use this for initialization
	void Start () {
		handL = GameObject.Find("Left Hand");
		handR = GameObject.Find("Right Hand");
		scaleSpeed = 0;
		totalScale = gameObject.collider.transform.localScale.magnitude / 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (handL == null)
			handL = GameObject.Find("Left Hand");
		if (handR == null)
			handR = GameObject.Find("Right Hand");
		
		if (handL == null || handR == null)
			return;
		// Material mat = (Material) gameObject.GetComponent<Material>();
		Vector3 l = handL.transform.position;
		Vector3 r = handR.transform.position;
		
		bool move = false;
		
		bool lCollide = gameObject.collider.bounds.Contains(l);
		bool rCollide = gameObject.collider.bounds.Contains(r);
		
		SixenseInput.Controller lControl = SixenseInput.GetController(SixenseHands.LEFT);
		SixenseInput.Controller rControl = SixenseInput.GetController(SixenseHands.RIGHT);
		
		if (lControl != null && rControl != null) {
		lDown = (lCollide || lDown) && lControl.GetButton(SixenseButtons.TRIGGER);
		rDown = (rCollide || rDown) && rControl.GetButton(SixenseButtons.TRIGGER);
		
		if (lDown && rDown) {
			move = true;
			//Vector3 v1 = gameObject.transform.rotation * (l - lPrev);
			//Vector3 v2 = gameObject.transform.rotation * (l - gameObject.transform.position);
			scaleSpeed = (r - l).magnitude - (rPrev - lPrev).magnitude;
		}
		
		lPrev = l;
		rPrev = r;
		}
		//gameObject.transform.Rotate(rotSpeed);
		totalScale += scaleSpeed;
		gameObject.transform.localScale = new Vector3(totalScale, totalScale, totalScale);
		scaleSpeed = scaleSpeed * .7f;
		//rotSpeed = new Vector3(0, 1, 0);
	}
}
