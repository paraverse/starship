using UnityEngine;
using System.Collections;
using System.IO;

public class ParticleSpawner : MonoBehaviour, hideable {

	int currentParticle = 0;
	static int numberOfParticles = 100;
	float[][] points = new float[numberOfParticles][];
	
	private ParticleSystem.Particle[] particles = new ParticleSystem.Particle[numberOfParticles];
	
 	private bool h = false;
	
	public void hide () {
		h = true;
	}
	void Start () {
		for(int k = 0; k < numberOfParticles; k++){
			points[k] = new float[4];
			
		}
	}
	
	// Update is called once per frame
	void Update (){
		if (h) {
			HandleVisuals.inactiveObjects.Add(gameObject);
			gameObject.SetActive(false);
			h = false;
        }
	}
	
	
	void LateUpdate()
	{
		particleSystem.playbackSpeed = 1;
		
		int length = particleSystem.GetParticles(particles); 
		int i = 0;
		
		while (i < length)
		{

			points[i][2] = points[i][2] + (float).1;
			
			if(points[i][2] > Random.value * 30){
				points[i][2] = 0;	
			}
			
			float x = points[i][0];
			float y = points[i][1];
			float z = points[i][2];
			particles[i].position = new Vector3((float)(x*2),(float)(y*2),(float)(z*2));

			particles[i].size = (float)1;
			particles[i].color =  Color.white;	
			
			

			i++;
		
		}
		particleSystem.loop = false;
		particleSystem.SetParticles(particles, length);
		Debug.Log("Time: " + particleSystem.duration);
		GameObject player = GameObject.Find ("ParticleSpawner");
		Vector3 position = player.renderer.bounds.extents;
		Debug.Log(position);
		spawnParticle(Random.value*position.x, Random.value*position.z);
		
	}
	
	void spawnParticle(float x, float y){
		points[currentParticle][0] = x;
		points[currentParticle][1] = y;
		currentParticle = currentParticle + 1;
		if(currentParticle >= 100){
			currentParticle = 0;
		}
	}
}
