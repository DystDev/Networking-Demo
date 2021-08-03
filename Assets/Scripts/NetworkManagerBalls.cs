using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerBalls : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        NetworkedPlayer player = conn.identity.GetComponent<NetworkedPlayer>();

        player.SetDispName($"Player {(int)Random.Range(10000, 99999)}");

        Debug.Log($"num of active players: {numPlayers}");
    }
}
