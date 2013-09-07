using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FarmVisual : MonoBehaviour {
	
	public string farmURL = "http://dev.aasen.in:1337/server-farm/";
	
	struct Server {
		public float cpu, ram, network, disk;
	
		public Server(float cpu, float ram, float network, float disk) {
			this.cpu = cpu;
			this.ram = ram;
			this.network = network;
			this.disk = disk;
		}
	}
	
	private List<List<Server>> serversOverTime = new List<List<Server>>();
	
	private List<GameObject> cpuLines = new List<GameObject>();
	private List<GameObject> ramLines = new List<GameObject>();
	private List<GameObject> networkLines = new List<GameObject>();
	private List<GameObject> diskLines = new List<GameObject>();
	
	private int currentIteration = 0;
	private int framesSinceLastChange = 0;
	private int updateInterval = 1;
	
	IEnumerator Start () {
		WWW farmResponse = new WWW(farmURL);
        yield return farmResponse;
		
		JSONObject json = new JSONObject(farmResponse.text);
		
		for (int i = 0; i < json.list.Count; i++) {
			List<Server> servers = new List<Server>();
			
			for (int j = 0; j < json.list[i].list.Count; j++) {
				JSONObject serverJson = json.list[i].list[j];
				
				float cpu = float.Parse(serverJson.GetField("cpu").ToString());
				float ram = float.Parse(serverJson.GetField("ram").ToString());
				float network = float.Parse(serverJson.GetField("network").ToString());
				float disk = float.Parse(serverJson.GetField("disk").ToString());
				
				Server server = new Server(cpu, ram, network, disk);
				
				servers.Add(server);
			}
			
			serversOverTime.Add(servers);
		}
		
		MakeLines(serversOverTime[currentIteration]);
	}
	
	private void MakeLines(List<Server> servers) {
		float dimension = (float) System.Math.Ceiling(System.Math.Sqrt(servers.Count));
		
		List<GameObject> lines = new List<GameObject>();
		
		for (int i = 0; i < servers.Count; i++) {
			Vector3 pos = gameObject.transform.position;
			
			cpuLines.Add(MakeLine(
				pos,
				pos + new Vector3(i, 0f, 0f),
				pos + new Vector3(i, servers[i].cpu, 0f),
				new Vector2(0.2f, 0.2f),
				new Color(0.752f, 0.223f, 0.168f)));

			ramLines.Add(MakeLine(
				pos,
				pos + new Vector3(i, 0f, 1f),
				pos + new Vector3(i, servers[i].ram, 1f),
				new Vector2(0.2f, 0.2f),
				new Color(0.204f, 0.596f, 0.859f)));
			
			networkLines.Add(MakeLine(
				pos,
				pos + new Vector3(i, 0f, 2f),
				pos + new Vector3(i, servers[i].network, 2f),
				new Vector2(0.2f, 0.2f),
				new Color(0.945f, 0.769f, 0.059f)));
			
			diskLines.Add(MakeLine(
				pos,
				pos + new Vector3(i, 0f, 3f),
				pos + new Vector3(i, servers[i].disk, 3f),
				new Vector2(0.2f, 0.2f),
				new Color(0.153f, 0.682f, 0.376f)));

		}
	}
	
	private GameObject MakeLine(Vector3 pos, Vector3 vertex0, Vector3 vertex1, Vector2 dimensions, Color color) {
		GameObject center = new GameObject();
		center.transform.position = gameObject.transform.position;
			
		LineRenderer line = (LineRenderer) center.AddComponent(typeof(LineRenderer));
		line.SetVertexCount(2);
		line.SetPosition(0, vertex0);
		line.SetPosition(1, vertex1);
		line.SetWidth(dimensions.x, dimensions.y);
		
		line.material = new Material (Shader.Find("Particles/Additive"));
		line.SetColors(color, color);

		return center;
	}
	
	void Update () {
		if (serversOverTime.Count > 0) {
			if (framesSinceLastChange > updateInterval) {
				List<Server> servers = serversOverTime[currentIteration];
			
				for (int i = 0; i < servers.Count; i++) {
					LineRenderer cpuLine = (LineRenderer) cpuLines[i].GetComponent("LineRenderer");
					cpuLine.SetPosition(1, gameObject.transform.position + new Vector3(i, servers[i].cpu, 0f));
	
					LineRenderer ramLine = (LineRenderer) ramLines[i].GetComponent("LineRenderer");
					ramLine.SetPosition(1, gameObject.transform.position + new Vector3(i, servers[i].ram, 1f));
			
					LineRenderer networkLine = (LineRenderer) networkLines[i].GetComponent("LineRenderer");
					networkLine.SetPosition(1, gameObject.transform.position + new Vector3(i, servers[i].network, 2f));
				
					LineRenderer diskLine = (LineRenderer) diskLines[i].GetComponent("LineRenderer");
					diskLine.SetPosition(1, gameObject.transform.position + new Vector3(i, servers[i].disk, 3f));
				}
		
				currentIteration = (currentIteration + 1) % serversOverTime.Count;
	
				framesSinceLastChange = 0;
			}
		}
		
		framesSinceLastChange++;
		
	}
}
