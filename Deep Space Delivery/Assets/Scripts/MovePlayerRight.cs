using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerRight : ScriptableObject, IPlayerCommand
{
    public void Execute(GameObject player)
    {
        player.transform.Translate(0.05f, 0, 0);
    }
}
