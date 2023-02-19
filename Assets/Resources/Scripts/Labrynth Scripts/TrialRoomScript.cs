using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialRoomScript : MonoBehaviour
{
	public enum RoomSize
	{ onextwo, twoxtwo };
	public enum RoomType
	{ combat, plat, none};

	public enum RoomState
	{ empty, closed, trialing, completed};

	public RoomType roomType;
	public RoomSize roomSize;
	private RoomState currRoomState;
	private GameObject doors;

	[SerializeField]
	private GameObject startPad; // for startpad prefab
	private List<GameObject> trialGeometry;
	private int roomLength;  //used to determine where trial geometry can be placed & enemies can be spawned

	private List<GameObject> enemyList;

	private GameObject playerRef;

	private void Start()
	{
		//room initial setup
		GameObject doors = transform.GetChild(0).gameObject;
		currRoomState = RoomState.empty;

		//determine room type
		if (Random.Range(1, 3) == 1)
		{
			roomType = RoomType.combat;
		}
		else
		{
			roomType = RoomType.plat;
		}
	}

	//____________________________________________________________________________________________________________

	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && currRoomState == RoomState.empty)
		{
			RoomClose();
			playerRef = other.gameObject;
		}
	}

	//Player enters room
	private void RoomClose() //called when player first enter's a room's trigger box
	{
		CloseDoors();
		currRoomState = RoomState.closed;
		Debug.Log("Room Closed");

		//Spawn Startpad in appropriate room location
		PlaceStartPad();
	}
	private void CloseDoors()
	{
		//each room has 4 exits, each made up of wall tiles.
		for (int i = 0; i < 8; i++) {
			doors.transform.GetChild(i).gameObject.SetActive(true);
		}
	}
	private void PlaceStartPad()
	{
		if (roomType == RoomType.combat)
		{
			//Instantiate(startPad prefab, new Vector3(CENTER OF ROOM), new Quaternion(0, 0, 0, 0), this.transform);
		}
		if (roomType == RoomType.plat)
		{
			//place at the end of the room
			//Instantiate(startPad prefab, new Vector3(LOCATION), new Quaternion(0, 0, 0, 0), this.transform);
		}
		trialGeometry.Add(transform.Find("StartPad").gameObject);
	}

	//____________________________________________________________________________________________________________

	//Generate Trial Geometry and Start Trial
	public void StartTrial()//called by the startpad when player enters the startpad's trigger area
	{
		if (roomType == RoomType.combat)
		{
			GenerateCombatInside();
			SpawnEnemies();
		}
		if (roomType == RoomType.plat)
		{
			GeneratePlatInside();
		}
		currRoomState = RoomState.trialing;
		Debug.Log("Trial Started");
	}
	private void GeneratePlatInside()//generate a platforming room
	{
		//******************all locations for room geometry must be places in relation to room transform**************
		//use room type and dungeonDepth to create a room layout with an appropriate difficulty
		//add everything to List TrialGeometry
	}
	
	private void GenerateCombatInside()//generate a combat room
	{
		//******************all locations for room geometry must be places in relation to room transform**************

		//use room type and dungeonDepth to create a room layout-- generating encounters happens when currRoomState is set to Trialing
		//add everything to List TrialGeometry
	}
	private void SpawnEnemies()
	{
		//spawn a bunch of enemies and add them to List<GameObject>EnemyList
		//trial is over when EnemyList.Count == 0;
	}
	public void Update()//is there a way to only call update once a combat room is started?
	{
		//checks to see if all enemies are defeated for a combat room
		if(roomType == RoomType.combat && currRoomState == RoomState.trialing && enemyList.Count == 0)
		{
			TrialCompleted();
		}
	}

//____________________________________________________________________________________________________________

	//completed trial
	private void TrialCompleted()
	{
		DespawnTrialGeometry();
		GivePlayerLoot();
		currRoomState = RoomState.completed;
		Debug.Log("Room Completed");
		OpenDoors();
	}
	private void DespawnTrialGeometry()
	{
		//go through trial geometry list and set to Active(false)
	}
	private void GivePlayerLoot()
	{
		//use playerRef to give loot
	}
	private void OpenDoors()
	{
		//each room has 4 exits, each made up of wall tiles.
		for (int i = 0; i < 8; i++)
		{
			doors.transform.GetChild(i).gameObject.SetActive(false);
		}
	}


}
