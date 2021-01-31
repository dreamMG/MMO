using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : MonoBehaviour
{
    public static MyNetworkManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.name);
        form.AddField("lvl", DBManager.lvl);
        form.AddField("CON", DBManager.CON);
        form.AddField("INE", DBManager.INE);
        form.AddField("STR", DBManager.STR);
        form.AddField("DEX", DBManager.DEX);
        form.AddField("exp", DBManager.exp);

        //Try get code PHP to create game
        UnityWebRequest www = UnityWebRequest.Post("http://10.0.0.6/Sqlconnect/savedata.php", form);

        yield return www.SendWebRequest();
        //If create start game
        if (www.downloadHandler.text == "0")
        {
            Debug.Log("Game saved");
        } else
        {
            Debug.Log("Save failed. Error #" + www.downloadHandler.text);
        }
    }
}
