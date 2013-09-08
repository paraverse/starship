using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GlobeFetcher : MonoBehaviour {
	
	public string url = "http://dev.aasen.in:1337/github-globe/";
	
	List<GameObject> lines;
	List<Vector3> positions;
	List<float> mags;
	
	float r;
    
	IEnumerator Start() {
		lines = new List<GameObject>();
		positions = new List<Vector3>();
		mags = new List<float>();
		
        WWW www = new WWW(url);
        yield return www;
        
		JSONObject j = new JSONObject(www.text);
		
		foreach (var jsonObject in j.list) {
			r = gameObject.transform.localScale.x;
			
			float latitude = float.Parse(jsonObject.GetField("coordinates").GetField("latitude").ToString()) * ((float) System.Math.PI / 180f);
			float longitude = float.Parse(jsonObject.GetField("coordinates").GetField("longitude").ToString()) * ((float) System.Math.PI / 180f);			
			float magnitude = float.Parse(jsonObject.GetField("magnitude").ToString());
			
			mags.Add(magnitude);
			
			float x = (float) -(r * System.Math.Cos(latitude) * System.Math.Sin(longitude));
			float y = (float) (r * System.Math.Sin(latitude));
			float z = (float) (r * System.Math.Cos(latitude) * System.Math.Cos(longitude));
			
			Vector3 relativePos = new Vector3(x, y, z) * .5f;
			Vector3 pos = gameObject.transform.position + relativePos;
			Vector3 dir = relativePos.normalized;
			
			GameObject center = new GameObject();
			center.transform.position = gameObject.transform.position;
			
			LineRenderer line = (LineRenderer) center.AddComponent(typeof(LineRenderer));
			line.SetVertexCount(2);
			line.SetPosition(0, pos);
			line.SetPosition(1, pos + dir * magnitude * 10);
			line.SetWidth(.3f, .3f);
			
			line.material = new Material (Shader.Find("Particles/Additive"));
			Color color = new Color(magnitude * 2, 0, 1-magnitude);
			line.SetColors(color, color);
			
			center.transform.parent = gameObject.transform;
			
			lines.Add(center);
			positions.Add(relativePos);
		}
	}
	
	void Update() {
		if (Input.GetKey(KeyCode.Space)) {
			HandleVisuals.inactiveObjects.Add(gameObject);
			gameObject.SetActive(false);
        }
		
		for (int i = 0; i < lines.Count; i++) {
			
			Vector3 relativePos = positions[i];
			relativePos = gameObject.transform.rotation * (relativePos * gameObject.transform.localScale.x / r);
			Vector3 pos = gameObject.transform.position + relativePos;
			Vector3 dir = relativePos.normalized;
			
			LineRenderer line = (LineRenderer) lines[i].GetComponent("LineRenderer");
			line.transform.position = gameObject.transform.position;
			line.SetVertexCount(2);
			line.SetPosition(0, pos);
			line.SetPosition(1, pos + dir * mags[i] * 10);
			
		}
	}
}
