using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTracker;

public class EventoBloqueo : MonoBehaviour
{
    public float tamano = 5f;
    public LayerMask layerBalas;

    private Bloqueo parry;
    private PowerUpPurple purple;

    private bool onHold = false;

    private GameObject lastBullet = null;   //Última bala que ha pasado por el radio

    private void Awake()
    {
        purple = GetComponentInParent<PowerUpPurple>();
        parry = GetComponentInParent<Bloqueo>();
    }

    private void Update()
    {
        Collider2D balasEnRadio = Physics2D.OverlapCircle(transform.position, tamano, layerBalas);
        ParryEvent e = TrackerSystem.GetInstance().CreateEvent<ParryEvent>();

        //Para que no te saque 14 parrys de la misma bala
        if (balasEnRadio != null && parry.enabled && !onHold && balasEnRadio.gameObject != lastBullet)
        {
            //Asignamos la última bala
            lastBullet = balasEnRadio.gameObject;
            e.setBlocked(parry.HasBlocked());
            e.setLevel((short)GameManager.instance.getCurrentLevel());
            e.setPurplePowerUp(purple.enabled);
            Debug.Log("EVENT: PARRY");
            onHold = true;

        }

        if (!parry.enabled && onHold)
        {
            e.setBlocked(parry.HasBlocked());
            if (parry.HasBlocked())
                Debug.Log("EVENT: PARRY DONE");
            TrackerSystem.GetInstance().trackEvent(e);
            Debug.Log("EVENT: SEND");
            onHold = false;
        }
    }
}
