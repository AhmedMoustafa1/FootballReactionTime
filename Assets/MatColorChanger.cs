using Kandooz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatColorChanger : MonoBehaviour {
	public Material _currentMat;
	public IntField gender;
	public Color maleColor;
	public Color femaleColor;
	// Use this for initialization
	void Start () {
		if (gender.Value == 1)
		{
			_currentMat.SetColor("_Color", maleColor);
		}
		if (gender.Value == 2)
		{
			_currentMat.SetColor("_Color", femaleColor);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
