using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGamePlayer : MonoBehaviour
{
    [SerializeField] private JumpGame game;

    private void OnCollisionEnter2D(Collision2D other)
    {
        this.game.failed = true;
    }
}
