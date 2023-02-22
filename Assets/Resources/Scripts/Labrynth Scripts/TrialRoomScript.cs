using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialRoomScript : MonoBehaviour
{
	public enum RoomSize
	{ onextwo, twoxtwo };
	public enum TrialType
	{ combat, plat, empty};

	public enum RoomState
	{ empty, closed, trialing, completed};

	public TrialType trialType;
	public RoomSize roomSize;
	private RoomState currRoomState;
	private Transform[] doors;

	[SerializeField]
	private GameObject startPad; // for startpad prefab
	private List<GameObject> trialGeometry = new List<GameObject>();
	private int roomLength;  //used to determine where trial geometry can be placed & enemies can be spawned

	private List<GameObject> enemyList = new List<GameObject>();

	private GameObject playerRef;

	private void Start()
	{
		//room initial setup
		doors = transform.GetChild(0).transform.GetComponentsInChildren<Transform>();
		SetDoorPresence(false);
		currRoomState = RoomState.empty;


		//if this room is an empty room, don't turn it into a combat or platforming room
		if(trialType != TrialType.empty)
		{
			//determine room type
			if (Random.Range(1, 3) == 1)
			{
				trialType = TrialType.combat;
			}
			else
			{
				trialType = TrialType.plat;
			}
		}
	}

	//____________________________________________________________________________________________________________

	public void OnTriggerEnter(Collider other)
	{
		Debug.Log(currRoomState);
		Debug.Log(trialType);
		//Debug.Log("Collided with: " + other);
		if(other.transform.tag == "Player" && currRoomState == RoomState.empty)
		{
			Debug.Log("Player found for this room");
			RoomClose();
			playerRef = other.gameObject;
		}
		Debug.Log(doors);
	}

	//Player enters room
	private void RoomClose() //called when player first enter's a room's trigger box
	{
		SetDoorPresence(true);
		currRoomState = RoomState.closed;
		Debug.Log("Room Closed");

		//Spawn Startpad in appropriate room location
		PlaceStartPad();
	}
	private void SetDoorPresence(bool doorsEnabled)
	{
		//Debug.Log("Doors are present- " + doorsEnabled);

		foreach (Transform childObject in doors)
		{
			MeshRenderer meshRenderer = childObject.GetComponent<MeshRenderer>();
			if (meshRenderer != null)
			{
				meshRenderer.enabled = doorsEnabled;
			}

			BoxCollider boxCollider = childObject.GetComponent<BoxCollider>();
			if (boxCollider != null)
			{
				boxCollider.enabled = doorsEnabled;
			}
		}
	}
	private void PlaceStartPad()
	{
		if (trialType == TrialType.combat)
		{
			//place in center of the room
			Instantiate(startPad, transform.position, new Quaternion(0, 0, 0, 0), this.transform);
		}
		if (trialType == TrialType.plat)
		{
			//place at the end of the room
			//Instantiate(startPad prefab, new Vector3(LOCATION), new Quaternion(0, 0, 0, 0), this.transform);
			Instantiate(startPad, transform.position, new Quaternion(0, 0, 0, 0), this.transform);//took this from combat rom for testing
		}
		//transform.Find("StartPad").gameObject.GetComponent<StartPadScript>().hostRoom = this;//transform.GetChild<StartPad>("StartPad") or something
		GameObject startPadReference = transform.GetChild(4).gameObject;
		startPadReference.GetComponent<StartPadScript>().hostRoom = this;
		trialGeometry.Add(transform.GetChild(4).gameObject);
	}

	//____________________________________________________________________________________________________________

	//Generate Trial Geometry and Start Trial
	public void StartTrial()//called by the startpad when player enters the startpad's trigger area
	{
		if (currRoomState == RoomState.closed) {
			if (trialType == TrialType.combat)
			{
				GenerateCombatInside();
				SpawnEnemies();
			}
			if (trialType == TrialType.plat)
			{
				GeneratePlatInside();
			}
			currRoomState = RoomState.trialing;
			Debug.Log("Trial Started");
		}
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
		if(trialType == TrialType.combat && currRoomState == RoomState.trialing && enemyList.Count == 0)
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
		SetDoorPresence(false);
	}
	private void DespawnTrialGeometry()
	{
		//go through trial geometry list and set to Active(false)
		foreach(GameObject thing in trialGeometry)
		{
			thing.SetActive(false);
		}
	}
	private void GivePlayerLoot()
	{
		//use playerRef to give loot
	}


}
