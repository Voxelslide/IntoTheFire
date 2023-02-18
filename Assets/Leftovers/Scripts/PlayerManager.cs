using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
  private List<PlayerInput> players = new List<PlayerInput>();
  [SerializeField]
  private List<Transform> startingPoints;
  [SerializeField]
  private List<LayerMask> playerLayers;

  private PlayerInputManager playerInputManager;

  private void Awake()
  {
    playerInputManager = FindObjectOfType<PlayerInputManager>();
  }

  private void OnEnable()
  {
    playerInputManager.onPlayerJoined += AddPlayer;
  }

  private void OnDisable()
  {
    playerInputManager.onPlayerJoined -= AddPlayer;
  }

  public void AddPlayer(PlayerInput player)
  {
  }
}