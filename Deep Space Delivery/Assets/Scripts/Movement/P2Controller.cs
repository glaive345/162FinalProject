﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Controller : MonoBehaviour
{
    private IPlayerCommand idle;


    void Start()
    {
        this.idle = ScriptableObject.CreateInstance<PlayerIdle>();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal2") == 0 && Input.GetAxis("Vertical2") == 0)
        {
            this.gameObject.GetComponent<Animator>().speed = 1;
            this.idle.Execute(this.gameObject);
        }
        else
        {
            var directionX = Input.GetAxis("Horizontal2");
            var directionY = -Input.GetAxis("Vertical2");

            var degree = UnityEngine.Mathf.Rad2Deg * UnityEngine.Mathf.Atan2(directionY, directionX);
            var speed = Mathf.Sqrt(directionX * directionX + directionY * directionY);
            this.gameObject.transform.rotation = Quaternion.Euler(degree, 90, 270);
            this.gameObject.GetComponent<Animator>().speed = speed;
            if (gameObject.transform.GetChild(2).gameObject.activeSelf)
            {
                this.gameObject.GetComponent<Animator>().Play("HumanoidWalk");
            }
            else
            {
                this.gameObject.GetComponent<Animator>().Play("HumanoidRun");
            }
        }
    }
}
