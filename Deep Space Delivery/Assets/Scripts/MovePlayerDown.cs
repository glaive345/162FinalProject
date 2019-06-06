using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerDown : ScriptableObject, IPlayerCommand
{
    public void Execute(GameObject player)
    {
        player.transform.rotation = Quaternion.Euler(Input.GetAxis("Vertical1") * 90, 90, 270);
        player.GetComponent<Animator>().Play("HumanoidRun");
    }
}