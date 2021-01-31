using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Teleport : NetworkBehaviour
{
    public Vector3 pos = new Vector3(5000, 0, 0);
    public int fromIndex = 0;
    public int toIndex = 1;

    public MapManager mapManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(mapManager == null)
                mapManager = FindObjectOfType<MapManager>();

                ChangeMap(other.gameObject);
        }
    }

    private void ChangeMap(GameObject other)
    {
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);

        other.transform.SetParent(mapManager.maps[toIndex]);

        mapManager.maps[toIndex].gameObject.SetActive(true);
        mapManager.maps[fromIndex].gameObject.SetActive(false);

        other.GetComponent<CharacterController>().enabled = false;
        other.transform.position = pos;
        Camera.main.transform.position = pos;
        other.GetComponent<CharacterController>().enabled = true;

        other.GetComponent<PlayerController>().SetSyncIndexMap(toIndex);
    }


}
