﻿using UnityEngine;
using UnityEngine.SceneManagement;

// Script de muerte del jugador

public class Death : MonoBehaviour
    // Meter al jugador para que muera
{
    [SerializeField] GameObject muerto = null;

    bool active = true;

    //habilita o desabilita la capacidad de morir.
    private void OnEnable() 
    {
        active = true;    
    }
    private void OnDisable()
    {
        active = false;
    }

    // Al ser invocado el método Dead() se considera al jugador muerto,
    // es eliminado y se invoca un "cadáver"
    public void Dead()
    {
        if (active)
        {
            AudioManager.instance.StopAllSFX();
            Instantiate(muerto, transform.position, Quaternion.identity);

            // TODO: Forzar envio de bloqueo (De lo contrario si mueres en mitad del parry no se manda el evento)

            // TODO: Evento de muerte
            try
            {
                muerto.GetComponent<ParryEvent_AfterDeath>().playerDied();
                GetComponent<DeathBeforeParryEvent>().sendEventDiedFromBullet();
            }
            catch
            {
                Debug.Log("NO tracker scripts asigned");
            }

            //Envío del evento de muerte
            Destroy(this.gameObject);  
        }
    }
}
