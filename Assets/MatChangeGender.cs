using Kandooz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChangeGender : MonoBehaviour {
	public IntField gender;
	public Material maleColor;
	public Material femaleColor;
	// Use this for initialization
	void Start () {
		if (gender.Value == 1)
		{
			this.GetComponent<MeshRenderer>().material = maleColor;
		}
		if (gender.Value == 2)
		{
			this.GetComponent<MeshRenderer>().material = femaleColor;

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
