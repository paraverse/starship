using UnityEngine;
using System.Collections;

public class HandleVisuals : MonoBehaviour {
	void Start () {

	}
	
	private void StartFarm() {
		GameObject farm = GameObject.CreatePrimitive(PrimitiveType.Plane);
		farm.transform.position = gameObject.transform.position + new Vector3(0f, 0f, 20f);
		farm.transform.localScale = new Vector3(1f, 10f, 1f);
		farm.AddComponent("FarmVisual");
	}
	
	private void StartGlobe() {
		GameObject globe = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		globe.transform.position = gameObject.transform.position + new Vector3(0f, 0f, 20f);
		globe.transform.localScale = new Vector3(10f, 10f, 10f);
		globe.renderer.sharedMaterial = (Material) Resources.Load("Earth");
		globe.AddComponent("GlobeFetcher");
	}
	
	void Update () {
		if(Input.GetKey(KeyCode.F)) {
            StartFarm();
        }
		
		if(Input.GetKey(KeyCode.G)) {
            StartGlobe();
        }
	}
}
