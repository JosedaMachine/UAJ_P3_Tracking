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
    private ParryEvent parryEvent;

    private void Awake()
    {
        purple = GetComponentInParent<PowerUpPurple>();
        parry = GetComponentInParent<Bloqueo>();
    }

    private void Update()
    {
        bool balasEnRadio = Physics2D.OverlapCircle(transform.position, tamano, layerBalas);

        if (balasEnRadio && parry.enabled && !onHold)
        {
            ParryEvent e = TrackerSystem.GetInstance().CreateEvent<ParryEvent>();
            e.setBlocked(parry.HasBlocked());
            e.setLevel((short)GameManager.instance.actualScene);
            e.setPurplePowerUp(purple.enabled);
            Debug.Log("EVENT: PARRY");
            onHold = true;

            if (!parry.enabled && onHold)
            {
                e.setBlocked(parry.HasBlocked());
                if (parry.HasBlocked())
                    Debug.Log("EVENT: PARRY DONE");
                TrackerSystem.GetInstance().trackEvent(parryEvent);
                Debug.Log("EVENT: SEND");
                onHold = false;
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, tamano);
    }
}
