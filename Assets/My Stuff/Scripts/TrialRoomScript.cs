using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialRoomScript : MonoBehaviour
{
	//eventually split trial rooms and teleporter rooms into different scripts?


	public enum TrialType
	{ combat, plat, none};
	
	public Vector3 start;
	public Vector3 teleporterPos;
	public TrialType trialType;

	private void Start()
	{
		//determine trial type

			if (Random.Range(1, 3) == 1)
			{
				trialType = TrialType.combat;
				GenerateCombatInside();
			}
			else
			{
				trialType = TrialType.plat;
				GeneratePlatInside();
			}
	}

	//generate a combat room
	private void GenerateCombatInside()
	{
		//use room type and dungeonDepth to create a room layout and spawn enemies with an appropriate difficulty
	}


	//generate a platforming room
	private void GeneratePlatInside()
	{
		//use room type and dungeonDepth to create a room layout with an appropriate difficulty
	}

}
