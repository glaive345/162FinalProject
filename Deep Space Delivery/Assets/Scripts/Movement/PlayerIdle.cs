using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : ScriptableObject, IPlayerCommand
{
    public void Execute(GameObject player)
    {
        player.GetComponent<Animator>().Play("HumanoidIdle");
    }
}
