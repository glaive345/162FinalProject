using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Controller : MonoBehaviour
{
    private IPlayerCommand right;
    private IPlayerCommand left;
    private IPlayerCommand up;
    private IPlayerCommand down;
    private IPlayerCommand utility;
    private bool playerInPlayzone;
    private GameObject interactingGame;


    void Start()
    {
        // true for now
        this.playerInPlayzone = false;

        this.down = ScriptableObject.CreateInstance<MovePlayerDown>();
        this.up = ScriptableObject.CreateInstance<MovePlayerUp>();
        this.right = ScriptableObject.CreateInstance<MovePlayerRight>();
        this.left = ScriptableObject.CreateInstance<MovePlayerLeft>();
        this.utility = ScriptableObject.CreateInstance<PlayerInteraction>();
    }

    void Update()
    {
        this.playzoneDetection();
        if (this.playerInPlayzone && Input.GetButtonDown("Utility2"))
        {
            this.utility.Execute(this.interactingGame);
        }

        if (Input.GetAxis("Horizontal2") > 0.01)
        {
            this.right.Execute(this.gameObject);
        }
        else if (Input.GetAxis("Horizontal2") < -0.01)
        {
            this.left.Execute(this.gameObject);
        }
        else if (Input.GetAxis("Vertical2") > 0.01)
        {
            this.up.Execute(this.gameObject);
        }
        else if (Input.GetAxis("Vertical2") < -0.01)
        {
            this.down.Execute(this.gameObject);
        }
    }

    private void playzoneDetection()
    {

        if (!this.playerInPlayzone)
        {
            // if (in some playzone)
            // {
            this.playerInPlayzone = true;
            // playzone's corresponding gameobject
            this.interactingGame = GameObject.Find("Virus Game");
            // }
        }

        // else
        // {
        //     this.playerInPlayzone = false;
        //     this.interactingGame = null;
        // }
    }
}
