using UnityEngine;
using System.Collections;

public class HydraRotControl : MonoBehaviour {
	
	GameObject handL;
	GameObject handR;
	
	Vector3 lPrev;
	Vector3 rPrev;
	
	// Use this for initialization
	void Start () {
		handL = GameObject.Find("Left Hand");
		handR = GameObject.Find("Right Hand");
	}
	
	// Update is called once per frame
	void Update () {
		/*
		Material mat = (Material) gameObject.GetComponent<Material>();
		Vector3 l = handL.transform.position;
		Vector3 r = handR.transform.position;
		
		mat.color.r = (l - lPrev).magnitude;
		*/
	}
}
