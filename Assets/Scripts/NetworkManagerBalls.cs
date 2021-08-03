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

        player.SetDispName($"Player {Random.Range(10000, 99999)}");

        Color randColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f));
        
        player.SetDispColor(randColor);
        

    }
}
