using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setImage : MonoBehaviour {


	public Sprite menCourt;
	public Sprite womenCourt;
	private Image courtImage;
	// Use this for initialization
	void Start () {
		courtImage = this.gameObject.GetComponent<Image>();

	}

	

	public void setMenCourt()
	{
		courtImage.sprite = menCourt;
	}
	public void setWomenCourt()
	{
		courtImage.sprite = womenCourt;
	}
}
