using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _lobbyListCanvas;
    [SerializeField] private GameObject _lobbyRoomCanvas;
    [SerializeField] private TextMeshProUGUI _roomId;
    [SerializeField] private TextMeshProUGUI _playerInRoom;
    private int playersInRoom = 0;
    public static MainMenuManager Instance;
    private void Start()
    {
        Instance = this;
    }
    public void CreateGame()
    {
        NetworkManager.Instance.CreateRoom();
        _lobbyListCanvas.SetActive(false);
        _lobbyRoomCanvas.SetActive(true);
    }
    public void JoinGame()
    {
        NetworkManager.Instance.GetRooms();
        _lobbyListCanvas.SetActive(true);
        _lobbyRoomCanvas.SetActive(false);
    }
    public void JoinRoom()
    {
        _lobbyListCanvas.SetActive(false);
        _lobbyRoomCanvas.SetActive(true);
    }

    public void SetId(string uid)
    {
        _roomId.text = "Room id: " + uid;
    }
    public void SetPlayersText(int playersNum)
    {
        _playerInRoom.text = playersInRoom + playersNum + "/4";
    }

    public void StartGame()
    {
        NetworkManager.Instance.StartGame();
    }
}
