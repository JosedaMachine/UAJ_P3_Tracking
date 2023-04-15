using UnityEngine;
using GameTracker;
//Cuando esta activo desactiva el script que te permite morir y viceversa.

public class PowerUpRed : MonoBehaviour
{
    Death death;
    private void Awake()
    {
        death = GetComponent<Death>();
    }
    private void OnEnable()
    {
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
