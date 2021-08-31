using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHealthChanged))]
    float healthLevel;

    void OnHealthChanged(float oldFloat, float newFloat)
    {
        //Update Health on all clients
        
    }

    [Server]
    void ChangeHealthLevel(float value)
    {

        float newHealth = healthLevel + value;
        newHealth = Mathf.Clamp(newHealth,0,100);
        CheckIfDead();
    }
    
    [Server]
    void SetHealthLevelTo(float value)
    {
        healthLevel = value;
        CheckIfDead();
    }

    [Server]
    void CheckIfDead()
    {
        if (healthLevel == 0)
        {
            //Pizda tebe
            Debug.Log("You ded");
        }
    }
}
