using UnityEngine;
using GameTracker;
//Cuando esta activo desactiva el script que te permite morir y viceversa.


//ALTERED BY ÁNGEL LÓPEZ FOR UAJ
public class PowerUpRed : MonoBehaviour
{
    Death death;
    private void Awake()
    {
        death = GetComponent<Death>();
    }
    private void OnEnable()
    {
        //Evento del power up del game tracker
        Debug.Log("EVENT:POWERUP_RED ADQUIRED");
        ObtainRedPowerUpEvent e = TrackerSystem.GetInstance().CreateEvent<ObtainRedPowerUpEvent>();
        
        e.setLevel((short)GameManager.instance.actualScene);

        TrackerSystem.GetInstance().trackEvent(e);

        if (death!=null)
        {
            death.enabled = false;
        }
    }

    private void OnDisable()
    {
        if (death != null)
        {
            death.enabled = true;
        }
    }
}
