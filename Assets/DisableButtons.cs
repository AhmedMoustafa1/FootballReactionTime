using Kandooz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableButtons : MonoBehaviour {

	public IntField location;
	public BoxCollider button;
	public int value1=1;
	public int value2=2;
	public int value3=3;
	// Use this for initialization
	void Awake () {
		//button = this.gameObject.GetComponent<BoxCollider>();
		//DisableButton();
	}
	
	public void Start()
	{

	}
	public void DisableButton()
	{

		if (location.Value == value1 || location.Value== value2 || location.Value== value3)
		{
			button.enabled = false;
		}
		else
		{
			button.enabled = true;

		}
	}
}
