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
        // Tiempo que tarda en pulsarlo
        timeStamp += Time.deltaTime;
        if (Input.GetKeyDown("joystick button 4") || Input.GetMouseButtonDown(1))
        {
            ParryInputAfterDeath e = TrackerSystem.GetInstance().CreateEvent<ParryInputAfterDeath>();
            e.setTimeAfterDeath(timeStamp);
            e.setLevel((short)GameManager.instance.actualScene);
            TrackerSystem.GetInstance().trackEvent(e);
            Debug.Log("EVENT: AFTER DEATH BLOCK");
        }
    }

    public void playerDied()
    {
        //Se setea el tiempo
        timeStamp = 0.0f;
    }
}
