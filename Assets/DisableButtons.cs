using Kandooz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableButtons : MonoBehaviour {

	public IntField location;
	public BoxCollider button;
	public int value=5;
	
	// Use this for initialization
	void Awake () {
		//button = this.gameObject.GetComponent<BoxCollider>();
		DisableButton();
	}
	
	public void Start()
	{

	}
	public void DisableButton()
	{

		if (location.Value == value )
		{
			button.enabled = true;

		}
		else
		{
			button.enabled = false;
			Debug.Log("Disabling" + this.gameObject.name);

		}
	}
}
