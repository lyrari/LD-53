using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timer;
    float startTimeSeconds = 300f;
    public float currentTime = 0;

    bool TimerInitialized;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void InitTimer()
    {
        currentTime = startTimeSeconds;
        this.gameObject.SetActive(true);
        TimerInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TimerInitialized) return;

        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            timer.text = "";
        } else
        {
            timer.text = Mathf.Floor(currentTime / 60).ToString("00") + ":" + Mathf.FloorToInt(currentTime % 60).ToString("00");
        }
    }
}
