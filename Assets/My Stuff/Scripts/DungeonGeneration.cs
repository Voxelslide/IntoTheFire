using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
  [SerializeField]
  private int dungeonSize = 4; //dungeon will have dungeonSize^2 number of rooms


  //reference to room
  public GameObject teleporterRoom;
  public GameObject room1x1;
  public GameObject room1x2;
  public GameObject room2x2;

  public List<GameObject> trialRooms = new List<GameObject>();
  public List<GameObject> teleporterRooms = new List<GameObject>();



  // Start is called before the first frame update
  void Start()
  {
    //just in case you forget to specify dungeonSize in the editor, generates a minimum of 16 rooms so there's no errors
    if(dungeonSize <= 4)
		{
      dungeonSize = 4;
		}


    GenerateDungeon();
  }

  public void GenerateDungeon()
	{

    //create trial rooms
    GenerateTrialRooms();


    //create teleporter rooms

    GenerateTeleporterRooms();


    //link teleporter rooms to trial rooms

    //link teleporter rooms to trial rooms - bruh how

  }

  private void GenerateTrialRooms()
	{
    for (int i = 0; i < dungeonSize; i++)
    {
      for (int j = 0; j < dungeonSize; j++)
      {
        int roomType = Random.Range(1, 3);
        //rotation rotates a room 90 degrees about the y axis if it is has an odd i
        //0.071 is the rotation needed in the x and z parameters for a 90 degree rotation in a quaternion
        float rotation = 0.7071f * (i % 2);

        if (roomType == 1)
        {
          Instantiate(room1x2, new Vector3(i * 150f, 0f, j * 150f), new Quaternion(0, rotation, 0, rotation), this.transform);
        }
        if (roomType == 2)
        {
          Instantiate(room2x2, new Vector3(i * 150f, 0f, j * 150f), new Quaternion(0, 0, 0, 0), this.transform);
        }

        trialRooms.Add(transform.GetChild(i * dungeonSize + j).gameObject);
      }
    }
  }

  private void GenerateTeleporterRooms()
  {
    //create number of teleporter rooms equal to rows-1 * cols-1
    for (int i = 0; i < dungeonSize - 1; i++)
    {
      for (int j = 0; j < dungeonSize - 1; j++)
      {
        Instantiate(teleporterRoom, new Vector3(i * 150f + 100f, 100f, j * 150f + 100f), new Quaternion(0, 0, 0, 0), this.transform);
        teleporterRooms.Add(transform.GetChild(i * (dungeonSize -1) + j).gameObject);
      }
    }
  }
}
