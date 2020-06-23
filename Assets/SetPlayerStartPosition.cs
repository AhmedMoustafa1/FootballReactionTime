using Kandooz;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerStartPosition : MonoBehaviour
{
	public IntField location;
	public IntField formation;

	public PlayerPositions[] playerPositions;
	void Awake()
	{


		SetLocation();
	}

	public void SetLocation()
	{
		

		this.transform.position = playerPositions[formation.Value].SqaudPositions[location.Value-1];




	}
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			SetLocation();

		}
	}
	//public void SetLocation()
	//{
	//	currentType = ExerciseManager.instance.nextExerciseType;
	//	switch (location.Value)
	//	{
	//		case 1:
	//			this.transform.localPosition = new Vector3(positionsx[0], transform.localPosition.y, positionsy[0]);
	//			if (currentType == ExerciseType.OneLayerEx1Transition || currentType == ExerciseType.OneLayerEx2Transition || currentType == ExerciseType.TwoLayerEx3Transition || currentType == ExerciseType.TwoLayerEx4Transition)
	//			{
	//				this.transform.localPosition = new Vector3(positionsx[0], transform.localPosition.y, positionsy[1]);

	//			}
	//			break;
	//		case 2:
	//			this.transform.localPosition = new Vector3(positionsx[1], transform.localPosition.y, positionsy[0]);
	//			if (currentType == ExerciseType.OneLayerEx1Transition || currentType == ExerciseType.OneLayerEx2Transition || currentType == ExerciseType.TwoLayerEx3Transition || currentType == ExerciseType.TwoLayerEx4Transition)
	//			{
	//				this.transform.localPosition = new Vector3(positionsx[1], transform.localPosition.y, positionsy[1]);

	//			}
	//			break;
	//		case 3:
	//			this.transform.localPosition = new Vector3(positionsx[2], transform.localPosition.y, positionsy[0]);
	//			if (currentType == ExerciseType.OneLayerEx1Transition || currentType == ExerciseType.OneLayerEx2Transition || currentType == ExerciseType.TwoLayerEx3Transition || currentType == ExerciseType.TwoLayerEx4Transition)
	//			{
	//				this.transform.localPosition = new Vector3(positionsx[2], transform.localPosition.y, positionsy[1]);

	//			}

	//			break;

	//		case 4:
	//			this.transform.localPosition = new Vector3(positionsx[0], transform.localPosition.y, positionsy[1]);
	//			break;
	//		case 5:
	//			this.transform.localPosition = new Vector3(positionsx[1], transform.localPosition.y, positionsy[1]);
				
	//			break;
	//		case 6:
	//			this.transform.localPosition = new Vector3(positionsx[2], transform.localPosition.y, positionsy[1]);
			



	//			break;
	//	}

	//}
}
