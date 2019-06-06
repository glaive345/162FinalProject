using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    [SerializeField] private Text UIText;
    [SerializeField] private float StartingTime;

    private float TimePassed;

    // Start is called before the first frame update
    void Start()
    {
        TimePassed = 0.0f + StartingTime;
    }

    // Update is called once per frame
    void Update()
    {
        TimePassed += Time.deltaTime;
        UIText.text = "Time: " + TimePassed.ToString("F");
    }
}
