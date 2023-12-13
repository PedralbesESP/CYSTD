using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Networking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("localhost:3000");
        yield return www.Send();
        requestNode response = new requestNode();


        string uwu = www.downloadHandler.text;

        Debug.Log(uwu);

    }

    private void sendInformation()
    {
        StartCoroutine(cSendInformation());
    }
    IEnumerator cSendInformation()
    {
        WWWForm form = new WWWForm();
        form.AddField("playerName", " ");
        UnityWebRequest www = UnityWebRequest.Post("http://roominvaders.ddns.net/insert.php", form);
        yield return www.SendWebRequest();
    }
}

[System.Serializable]
public class requestNode
{
    public string requestText;
}
