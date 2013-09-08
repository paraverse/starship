using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandleVisuals : MonoBehaviour {
	public static List<GameObject> inactiveObjects = new List<GameObject>();

	void Start () {

	}
	
	void Update () {
		if(Input.GetKey(KeyCode.F)) {
			Reactivate("FarmVisual");
		}
		
		if(Input.GetKey(KeyCode.G)) {
			Reactivate("GlobeVisual");
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
}
