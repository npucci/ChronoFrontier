using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {
	public Color color;

	// Use this for initialization
	void Start () {
		Mesh mesh = GetComponent < MeshFilter > ().mesh;
		Vector3 [] vertices = mesh.vertices;
		Color32 [] colors = new Color32 [ vertices.Length ];
		for ( int i = 0 ; i  < colors.Length ; i++ ) {
			colors [ i ] = color;
		}
		mesh.colors32 = colors;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
