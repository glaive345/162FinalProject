using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerUp : ScriptableObject, IPlayerCommand
{
    public void Execute(GameObject player)
    {
        player.transform.Translate(0, 0.05f, 0);
    }
}