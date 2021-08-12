using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MMONetworkManager : NetworkManager
{
    public GameObject[] players;

    // public Transform[] posToSpawn;

    private static int posIndex = 0;

    //private static GameObject firstPlayer;


    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<CreateMMOCharacterMessage>(OnCreateCharacter);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        // you can send the message here, or wherever else you want
        CreateMMOCharacterMessage characterMessage = new CreateMMOCharacterMessage();

        conn.Send(characterMessage);
    }

    void OnCreateCharacter(NetworkConnection conn, CreateMMOCharacterMessage message)
    {
        // playerPrefab is the one assigned in the inspector in Network
        // Manager but you can use different prefabs per race for example
        GameObject gameobject = Instantiate(players[posIndex]);
        //if (posIndex == 0)
        //{
        //    firstPlayer = gameobject;
        //} else if (posIndex == 1)
        //{
        //    gameobject.GetComponent<PlayerChangeColor>().Opponent = firstPlayer;
        //    firstPlayer.GetComponent<PlayerChangeColor>().Opponent = gameobject;
        //}
        //// gameobject.transform.position = posToSpawn[posIndex].position;

        posIndex++;


        // call this to use this gameobject as the primary controller
        NetworkServer.AddPlayerForConnection(conn, gameobject);
    }
}



public class CreateMMOCharacterMessage : MessageBase
{

}