using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class HandleVisuals : MonoBehaviour {
	public static List<GameObject> inactiveObjects = new List<GameObject>();
	public static GameObject visibleObject;
	
	GameObject handL;
	GameObject handR;
	
	void Start () {
		hideGlobe ();
		hideFarm ();
		handL = GameObject.Find("Left Hand");
		handR = GameObject.Find("Right Hand");
	}
	
	void Update () {
		
		if (handL == null)
			handL = GameObject.Find("Left Hand");
		if (handR == null)
			handR = GameObject.Find("Right Hand");
		
		if (handL == null || handR == null)
			return;
		
		Vector3 l = handL.transform.position;
		Vector3 r = handR.transform.position;
		
		GameObject b1 = GameObject.Find ("Button1");
		GameObject b2 = GameObject.Find ("Button2");
		GameObject b3 = GameObject.Find ("Button3");
		GameObject b4 = GameObject.Find ("Button4");
		
		bool lCollide1 = b1.collider.bounds.Contains(l);
		bool rCollide1 = b1.collider.bounds.Contains(r);
		bool lCollide2 = b2.collider.bounds.Contains(l);
		bool rCollide2 = b2.collider.bounds.Contains(r);
		bool lCollide3 = b3.collider.bounds.Contains(l);
		bool rCollide3 = b3.collider.bounds.Contains(r);
		bool lCollide4 = b4.collider.bounds.Contains(l);
		bool rCollide4 = b4.collider.bounds.Contains(r);
		
		if(Input.GetKey(KeyCode.F) || lCollide3 || rCollide3) {
			Reactivate("FarmVisual");
			hideGlobe();
		}
		
		if(Input.GetKey(KeyCode.G) || lCollide4 || rCollide4) {
			Reactivate("GlobeVisual");
			hideFarm ();
		}		
	}
	
	private void Reactivate(string name) {
		for (int i = 0; i < inactiveObjects.Count; i++) {
			if (inactiveObjects[i].name == name) {
				inactiveObjects[i].SetActive(true);
				inactiveObjects.RemoveAt(i);
				i--;
			}
		}
	}
	
	private void hideGlobe() {
		GameObject globe = GameObject.Find ("GlobeVisual");
		if (globe != null) {
			GlobeFetcher v = (GlobeFetcher) globe.GetComponent("GlobeFetcher");
			if (v != null) {
				v.hide ();
			}
		}
	}
	
	private void hideFarm() {
		GameObject farm = GameObject.Find ("FarmVisual");
		if (farm != null) {
			FarmVisual v = (FarmVisual) farm.GetComponent("FarmVisual");
			if (v != null) {
				v.hide ();
			}
		}
	}
}
