using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPositionFix : MonoBehaviour {

	public GameObject cameraRig;
	public float displacement = 10;
	void Start () {
		this.transform.position = new Vector3(cameraRig.transform.position.x - displacement, this.transform.position.y, this.transform.position.z);
	}
	

}
