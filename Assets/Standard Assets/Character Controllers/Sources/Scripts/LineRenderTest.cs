using UnityEngine;
using System.Collections;

public class LineRenderTest : MonoBehaviour {
	
	LineRenderer line;
	
	Material mat;
	
	// Use this for initialization
	void Start () {
		line = (LineRenderer) gameObject.GetComponent(typeof(LineRenderer));
	}
	
	// Update is called once per frame
	void Update () {
		line.SetVertexCount(20);
		for (int i = 0; i < 20; i++){
			line.SetPosition(i, new Vector3((float) i, (float) (i * i) / 5, 0));
			line.SetWidth(0, i / 10);
		}
	}
}
