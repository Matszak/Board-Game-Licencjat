using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerListObject", menuName = "PlayerMisc/currentPlayers", order = 1)]
public class PlayerListObject : ScriptableObject
{
    [FormerlySerializedAs("Players")] public List<Player> players;

}
