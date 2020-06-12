using Kandooz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerStartPosition : MonoBehaviour
{
	public IntField location;
	// Use this for initialization
	public float[] positionsx = { 0.967f, 3.88f, 6.88f };
	public float[] positionsy = { -5.3f, 3.88f };
	public ExerciseType currentType;
	void Start()
	{


		SetLocation();
	}


	public void SetLocation()
	{
		currentType = ExerciseManager.instance.nextExerciseType;
		switch (location.Value)
		{
			case 1:
				this.transform.localPosition = new Vector3(positionsx[0], transform.localPosition.y, positionsy[0]);
				if (currentType == ExerciseType.OneLayerEx1Transition || currentType == ExerciseType.OneLayerEx2Transition || currentType == ExerciseType.TwoLayerEx3Transition || currentType == ExerciseType.TwoLayerEx4Transition)
				{
					this.transform.localPosition = new Vector3(positionsx[0], transform.localPosition.y, positionsy[1]);

				}
				break;
			case 2:
				this.transform.localPosition = new Vector3(positionsx[1], transform.localPosition.y, positionsy[0]);
				if (currentType == ExerciseType.OneLayerEx1Transition || currentType == ExerciseType.OneLayerEx2Transition || currentType == ExerciseType.TwoLayerEx3Transition || currentType == ExerciseType.TwoLayerEx4Transition)
				{
					this.transform.localPosition = new Vector3(positionsx[1], transform.localPosition.y, positionsy[1]);

				}
				break;
			case 3:
				this.transform.localPosition = new Vector3(positionsx[2], transform.localPosition.y, positionsy[0]);
				if (currentType == ExerciseType.OneLayerEx1Transition || currentType == ExerciseType.OneLayerEx2Transition || currentType == ExerciseType.TwoLayerEx3Transition || currentType == ExerciseType.TwoLayerEx4Transition)
				{
					this.transform.localPosition = new Vector3(positionsx[2], transform.localPosition.y, positionsy[1]);

				}

				break;

			case 4:
				this.transform.localPosition = new Vector3(positionsx[0], transform.localPosition.y, positionsy[1]);
				break;
			case 5:
				this.transform.localPosition = new Vector3(positionsx[1], transform.localPosition.y, positionsy[1]);
				
				break;
			case 6:
				this.transform.localPosition = new Vector3(positionsx[2], transform.localPosition.y, positionsy[1]);
			



				break;
		}

	}
}
