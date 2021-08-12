using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerCountdown : MonoBehaviour
{
    public GameObject Counter;
    private const int MOD=60;
    public int TimeLeft = 120;
    public bool takingAway = false;

    bool is_sound = true; // can play the sound

    public GameObject countingdownAudio;

    void DisplayTime()
    {
        Counter.GetComponent<Text>().text = (TimeLeft / MOD).ToString("00") + ":" + (TimeLeft % MOD).ToString("00");
    }

    void Start()
    {
        DisplayTime();
    }

    void Update()
    {
        if (takingAway == false && TimeLeft > 0)
        {
            StartCoroutine(TimerTake());
        }
        if (TimeLeft == 10)
        {
            playAlarm();
        }
        if (TimeLeft <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private IEnumerator TimerTake() // display countdown
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        TimeLeft -= 1;
        DisplayTime();
        takingAway = false;
    }

    void playAlarm()
    {
        if (is_sound)
        {
            Instantiate(countingdownAudio, new Vector3(0, 0, 0), Quaternion.identity);
            countingdownAudio.GetComponent<AudioSource>().Play();
            is_sound = false;
        }
    }
}
