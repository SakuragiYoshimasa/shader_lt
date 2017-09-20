using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour {

	public GameObject sphere;
	private List<GameObject> spheres = new List<GameObject>();
	private int w = 100;
	private int h = 100;

	void Start() {
		for(int i = 0; i < w; i++){
			for(int j = 0; j < h; j++){
				Vector3 pos = new Vector3(i * 2.0f, 0, j * 2.0f);
				spheres.Add(GameObject.Instantiate(sphere, pos, Quaternion.identity) as GameObject);
				spheres[i * h + j].transform.parent = this.transform;
			}
		}
	}

	void Update() {
		for(int i = 0; i < w; i++){
			for(int j = 0; j < h; j++){
				Vector3 pos = new Vector3(i * 2.0f, 5.0f * Mathf.Sin((float)(i * j) / 200f + Time.realtimeSinceStartup), j * 2.0f);
				spheres[i * h + j].transform.position = pos;
			}
		}
	}
}
