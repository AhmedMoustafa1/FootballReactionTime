using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationSelector : MonoBehaviour {

	public GameObject[] formations;
	// Use this for initialization
	public void ActivateFormtion(int index)
	{
		for (int i = 0; i < formations.Length; i++)
		{
			if (i == index)
			{
				formations[i].SetActive(true);
			}
			else
			{
				formations[i].SetActive(false);
			}
		}
	}
}
