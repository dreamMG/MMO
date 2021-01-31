using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Mirror.Discovery;

public class Login : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public Button Subimit;

    public NetworkManager manager;

    readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    public NetworkDiscovery networkDiscovery;

    public Text listServers;

    private void Start()
    {
    }

    public void FindServerButton() { 
            discoveredServers.Clear();
            networkDiscovery.StartDiscovery();
        }
    public void StartHostButton() {

            discoveredServers.Clear();
            NetworkManager.singleton.StartHost();
            networkDiscovery.AdvertiseServer();
    }

    public void StartServerButton()
    {
        discoveredServers.Clear();
        NetworkManager.singleton.StartServer();
        networkDiscovery.AdvertiseServer();
    }

    private void Update()
    {
        //listServers.text = $"Discovered Servers [{discoveredServers.Count}]:";

        foreach (ServerResponse info in discoveredServers.Values)
            listServers.text = listServers.text + " " + info.EndPoint.Address.ToString();
    }

    void Connect(ServerResponse info)
    {
        NetworkManager.singleton.StartClient(info.uri);
    }

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Subimit.onClick.AddListener(Submit);
    }

    public void Submit()
    {
        StartCoroutine(LoginCLIENT());
    }

    public void StartHostServer()
    {
        StartCoroutine(LoginHOST());
    }

    //Create acc
    IEnumerator LoginHOST()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        //Try get code PHP to create game
        UnityWebRequest www = UnityWebRequest.Post("http://10.0.0.6/Sqlconnect/login.php", form);

        yield return www.SendWebRequest();
        //If create start game
        if(www.downloadHandler.text[0] == '0')
        {
            SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);

            DBManager.InitDBManager(nameField.text,
            int.Parse(www.downloadHandler.text.Split('\t')[1]),
            int.Parse(www.downloadHandler.text.Split('\t')[2]),
            int.Parse(www.downloadHandler.text.Split('\t')[3]),
            int.Parse(www.downloadHandler.text.Split('\t')[4]),
            int.Parse(www.downloadHandler.text.Split('\t')[5]),
            int.Parse(www.downloadHandler.text.Split('\t')[6])
            );

            if (!NetworkClient.active)
            {
                manager.StartHost();

                    if (NetworkClient.isConnected && !ClientScene.ready)
                    {
                        ClientScene.Ready(NetworkClient.connection);
                    }
            }
        } else
        {
            Debug.Log("You cannot login " + www.downloadHandler.text);
        }
    }
    IEnumerator LoginCLIENT()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        //Try get code PHP to create game
        UnityWebRequest www = UnityWebRequest.Post("http://10.0.0.6/Sqlconnect/login.php", form);

        yield return www.SendWebRequest();
        //If create start game
        if (www.downloadHandler.text[0] == '0')
        {
            SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);

            DBManager.InitDBManager(nameField.text,
            int.Parse(www.downloadHandler.text.Split('\t')[1]),
            int.Parse(www.downloadHandler.text.Split('\t')[2]),
            int.Parse(www.downloadHandler.text.Split('\t')[3]),
            int.Parse(www.downloadHandler.text.Split('\t')[4]),
            int.Parse(www.downloadHandler.text.Split('\t')[5]),
            int.Parse(www.downloadHandler.text.Split('\t')[6])
            );

            if (!NetworkClient.active)
            {
                manager.StartClient();

                if (NetworkClient.isConnected && !ClientScene.ready)
                {
                    ClientScene.Ready(NetworkClient.connection);
                }
            }
        }
        else
        {
            Debug.Log("You cannot login " + www.downloadHandler.text);
        }
    }

    public string GetOnlineName()
    {
        return nameField.text;
    }

    private void OnApplicationPause(bool pause)
    {
        manager.StopClient();
        manager.StopHost();
    }
}
