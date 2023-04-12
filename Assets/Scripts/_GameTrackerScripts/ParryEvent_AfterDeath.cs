using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryEvent_AfterDeath : MonoBehaviour
{
    private bool justDied;
    private float timeStamp;
    public float timeWindow_AfterDeath = 0.5f;
    private Bloqueo parry;

    private void Start()
    {
        justDied = false;
    }

    public void playerDied(ref Bloqueo p)
    {
        justDied = true;
        timeStamp = 0.0f;
        parry = p;
    }
    // Update is called once per frame
    void Update()
    {
        if (justDied)
        {
            if (Input.GetKeyDown("joystick button 4") || Input.GetMouseButtonDown(1))
            {
                //Enviar el evento y tal
                //....
            }

            //Comprobar si el tiempo de muerte ha pasado
            timeStamp += Time.deltaTime;
            if(timeStamp > timeWindow_AfterDeath)
                justDied=false;
        }
    }
}
