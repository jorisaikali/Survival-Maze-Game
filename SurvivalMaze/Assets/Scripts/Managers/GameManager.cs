using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private const string PLAYER_PREFIX = "P";
    private static Dictionary<string,PlayerVitals> players = new Dictionary<string,PlayerVitals>();

    public static void RegisterPlayer(string netID, PlayerVitals player) {
        
        string playerid = PLAYER_PREFIX + netID;

        Debug.Log("Registering player: " + playerid);
        player.transform.name = playerid;
        players.Add(playerid, player);
        
    } 

    public static void DeregisterPlayer (string playerID) {
        players.Remove(playerID);
    }

    public static PlayerVitals GetPlayer (string playerID) {
        return players[playerID];
    }
	void OnGUI () {
        GUILayout.BeginArea(new Rect(200,200,200,500));

        GUILayout.BeginVertical();

        foreach (string playerID in players.Keys)
        {
            GUILayout.Label(playerID + "--" + players[playerID].transform.name);
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
