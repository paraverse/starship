using UnityEngine;
using System.Collections;

public class GlobeFetcher : MonoBehaviour {
	
	public string url = "http://dev.aasen.in:1337/github-globe/";
    
	IEnumerator Start() {
        WWW www = new WWW(url);
        yield return www;
        
		JSONObject j = new JSONObject(www.text);
	
		foreach (var jsonObject in j.list) {
			float r = gameObject.transform.localScale.x;
			
			float latitude = float.Parse(jsonObject.GetField("coordinates").GetField("latitude").ToString());
			float longitude = float.Parse(jsonObject.GetField("coordinates").GetField("longitude").ToString());			
			float magnitude = float.Parse(jsonObject.GetField("magnitude").ToString());
			
			float x = (float) (r * System.Math.Cos(latitude) * System.Math.Cos(longitude));
			float y = (float) (r * System.Math.Cos(latitude) * System.Math.Sin(longitude));
			float z = (float) (r * System.Math.Sin(latitude));
			
			Vector3 pos = gameObject.transform.position + new Vector3(x, y, z) * .5f;
			Vector3 dir = new Vector3(x, y, z).normalized;
			
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
			
			
			
			/*
			GameObject bar = GameObject.CreatePrimitive(PrimitiveType.Cube); 
			
			bar.transform.position = gameObject.transform.position + (new Vector3(x, y, z) * 0.5f);
			
			bar.transform.localScale = new Vector3(0.05f, 0.05f, magnitude * 20f);
			bar.transform.LookAt(gameObject.transform);
			*/
		}
	}
}
