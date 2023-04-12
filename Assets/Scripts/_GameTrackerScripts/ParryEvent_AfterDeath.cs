using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTracker;

public class ParryEvent_AfterDeath : MonoBehaviour
{
    private float timeStamp;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("joystick button 4") || Input.GetMouseButtonDown(1))
        {
            ParryInputAfterDeath e = TrackerSystem.GetInstance().CreateParryInputAfterDeathEvent();
            e.setTimeAfterDeath(timeStamp);
            e.setLevel((short)GameManager.instance.actualScene);
            TrackerSystem.GetInstance().trackEvent(e);
            Debug.Log("EVENT: AFTER DEATH BLOCK");
        }

        // Tiempo que tarda en pulsarlo
        timeStamp += Time.deltaTime;
    }
}
