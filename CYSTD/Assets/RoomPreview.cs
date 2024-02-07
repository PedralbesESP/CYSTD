using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPreview : MonoBehaviour
{
    const int maxPlayers = 4;
    [SerializeField] TMPro.TextMeshProUGUI idText;
    [SerializeField] TMPro.TextMeshProUGUI playersText;

    public void SetRoomInfo(string id, string playerCount)
    {
        idText.text = "Id: " + id;
        playersText.text = playerCount + "/" + maxPlayers.ToString();
    }

}
