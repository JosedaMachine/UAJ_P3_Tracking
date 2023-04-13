using GameTracker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBeforeParryEvent : MonoBehaviour
{
    private Bloqueo parry;
    private float timeStamp;
    private bool startCounting;
    public float timeWindow_UntilDeath = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        parry = GetComponent<Bloqueo>();
        startCounting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(startCounting)
        {
            timeStamp += Time.deltaTime;
            if(timeStamp > timeWindow_UntilDeath)
            {
                startCounting = false;
            }
        }
        else if(parry.isActiveAndEnabled)
        {
            timeStamp = 0.0f;
            startCounting = true;
        }
            
    }

    public void sendEventDiedFromBullet()
    {
        if(startCounting)
        {
            //Proceso del evento
            DieFromBulletEvent e = TrackerSystem.GetInstance().Crea();//Hay que hacer el evento die from bullet cabrones
            e.setLevel((short)GameManager.instance.actualScene);
            //e.setTimeAfterDeath(timeStamp);
            TrackerSystem.GetInstance().trackEvent(e);
            Debug.Log("EVENT: DIED AFTER BLOCK");
        }
    }
}
