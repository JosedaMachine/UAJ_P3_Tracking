using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTracker;

public class ParryEvent_AfterDeath : MonoBehaviour
{
    private float timeStamp;
    private bool justDied;
    public float timeWindow_AfterDeath = 0.5f;

    void Start()
    {
        justDied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (justDied)
        {
            if (Input.GetKeyDown("joystick button 4") || Input.GetMouseButtonDown(1))
            {
                ParryInputAfterDeath e = TrackerSystem.GetInstance().CreateEvent<ParryInputAfterDeath>();
                e.setTimeAfterDeath(timeStamp);
                e.setLevel((short)GameManager.instance.actualScene);
                TrackerSystem.GetInstance().trackEvent(e);
                Debug.Log("EVENT: AFTER DEATH BLOCK");
                justDied = false;
            }

            // Tiempo que tarda en pulsarlo
            timeStamp += Time.deltaTime;
            if(timeStamp > timeWindow_AfterDeath && justDied)
            {
                justDied = false;
            }
        }
    }

    public void playerDied()
    {
        //Se setea el tiempo
        timeStamp = 0.0f;
    }
}
