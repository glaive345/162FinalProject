using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGame : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject stripe0;
    [SerializeField] private GameObject stripe1;
    [SerializeField] private GameObject stripe2;
    [SerializeField] private GameObject stripe3;
    [SerializeField] private GameObject stripe4;
    [SerializeField] private GameObject stripe5;
    [SerializeField] private GameObject displayPanel;
    public bool failed;

    private Rigidbody2D playerBody;
    private float thrust = 3.0f;
    void Start()
    {
        this.stripe0.GetComponent<Rigidbody2D>().velocity = new Vector2(-1,0);
        this.stripe1.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        this.stripe2.GetComponent<Rigidbody2D>().velocity = new Vector2(-1,0);
        this.stripe3.GetComponent<Rigidbody2D>().velocity = new Vector2(-1,0);
        this.stripe4.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        this.stripe5.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);

        this.playerBody = this.player.GetComponent<Rigidbody2D>();
        this.failed = false;
    }

    void Update()
    {
        if (this.failed)
        {
            // this.displayPanel.SetActive(false);
            this.player.SetActive(false);
        }

        if (Input.GetButtonDown("Utility1"))
        {
            this.playerBody.AddForce(this.player.transform.up * this.thrust, ForceMode2D.Impulse);
        }

    }
}
