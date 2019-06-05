using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : ScriptableObject, IPlayerCommand
{
    public void Execute(GameObject game)
    {
        game.GetComponent<IMinigame>().Interaction();
    }
}