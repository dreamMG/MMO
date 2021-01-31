using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MapManager : NetworkBehaviour
{
    public Dictionary<GameObject, int> playersOnMap = new Dictionary<GameObject, int>();

    public Transform[] maps;

    public void SetPlayer(GameObject player, int map)
    {
            playersOnMap.Add(player, map);
    }
}
