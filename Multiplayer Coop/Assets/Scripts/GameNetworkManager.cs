﻿using System.Collections.Generic;
using System.Linq;
using Barebones.MasterServer;
using UnityEngine;
using UnityEngine.Networking;

public class GameNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();
        Instantiate(Resources.Load<GameObject>("Prefabs/Field"));
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        Destroy(FindObjectOfType<LevelGenerator>().gameObject);
        foreach (Interactible i in FindObjectsOfType<Interactible>())
        {
            Destroy(i.gameObject);
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
       SpawnPlayer(conn, "Player", "carrot");
       //SpawnPlayer(conn, "Tester", "carrot");
    }

    /// <summary>
    ///     Spawns a character for connected client, and assigns it to connection
    /// </summary>
    /// <returns></returns>
    public static PlayerController SpawnPlayer(NetworkConnection connection, string playerName, 
        string weaponSprite, Transform position = null)
    {
        // Create an instance
        var player = Instantiate(Resources.Load<PlayerController>("Prefabs/Player"));
        //var player = Instantiate(Resources.Load<PlayerController>("Prefabs/Tester"));
        if (position == null)
        {
            // Nove to random position, of no position was given
            player.MoveToRandomSpawnPoint();
        }
        else
        {
            player.transform.position = position.position;
        }

        NetworkServer.AddPlayerForConnection(connection, player.gameObject, 0);

        return player;
    }

    public void DisconnectAllPlayers()
    {
        foreach (var player in FindObjectsOfType<PlayerController>())
        {
            player.connectionToClient.Disconnect();
        }
    }
}