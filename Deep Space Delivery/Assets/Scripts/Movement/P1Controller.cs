using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controller : MonoBehaviour
{
    private IPlayerCommand idle;

    [SerializeField] private float speedMultiplierDecay;
    private float speedMultiplier;


    void Start()
    {
        this.idle = ScriptableObject.CreateInstance<PlayerIdle>();
        speedMultiplier = 1;
    }

    void Update()
    {
        if(Input.GetAxis("Horizontal1") == 0 && Input.GetAxis("Vertical1") == 0)
        {
            this.gameObject.GetComponent<Animator>().speed = 1;
            this.idle.Execute(this.gameObject);
        }
        else
        {
            var directionX = Input.GetAxis("Horizontal1");
            var directionY = -Input.GetAxis("Vertical1");

            var degree = UnityEngine.Mathf.Rad2Deg * UnityEngine.Mathf.Atan2(directionY, directionX);
            var speed = Mathf.Sqrt(directionX * directionX + directionY * directionY) * speedMultiplier;
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
        if(speedMultiplier > 1)
        {
            speedMultiplier -= speedMultiplierDecay * Time.deltaTime;
            if(speedMultiplier < 1)
            {
                speedMultiplier = 1;
            }
        }
    }
    public void changeSpeed(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
