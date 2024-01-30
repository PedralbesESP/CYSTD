using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _lobbyListCanvas;
    [SerializeField] private GameObject _lobbyRoomCanvas;
    [SerializeField] private TextMeshProUGUI _roomId;
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

    public void SetId(string uid)
    {
        _roomId.text = "Room id: " + uid;
    }
}
