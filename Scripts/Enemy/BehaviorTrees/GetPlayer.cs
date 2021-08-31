using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class GetPlayer : Action
{

    public enum PlayerType
    {
        none,
        nearest,
        weakest,
        strongest,
        random
    }

    public PlayerType playerType;

    public bool shouldBeInside;

    public SharedTransform playerFound;
    

    public override TaskStatus OnUpdate()
    {
        MyNetworkManager Net_manager = GetComponent<Enemy>().Net_manager;

        float value;

        switch(playerType)
        {
            case PlayerType.nearest:

            value  = 1000f;
            for (int i =0; i<Net_manager.Players.Count;i++)
            {
                float dist = Vector3.Distance(Net_manager.Players[i].transform.position, gameObject.transform.position);
                if (dist<value)
                {
                    if (shouldBeInside && Net_manager.Players[i].currentRoom !=0)
                    {
                    value = dist;
                    playerFound.Value = Net_manager.Players[i].transform;
                    }
                }
            } 

            break;
            case PlayerType.weakest:
            value  = 100f;
            for (int i =0; i<Net_manager.Players.Count;i++)
            {
                float sanity = Net_manager.Players[i].sanityLevel;
                if (sanity<value)
                {
                    if (shouldBeInside && Net_manager.Players[i].currentRoom !=0)
                    {
                    value = sanity;
                    playerFound.Value = Net_manager.Players[i].transform;
                    }
                }
            } 
            break;
            case PlayerType.strongest:
            value  = 0f;
            for (int i =0; i<Net_manager.Players.Count;i++)
            {
                float sanity = Net_manager.Players[i].sanityLevel;
                if (sanity>value)
                {
                    if (shouldBeInside && Net_manager.Players[i].currentRoom !=0)
                    {
                    value = sanity;
                    playerFound.Value = Net_manager.Players[i].transform;
                    }
                }
            } 
            break;
            case PlayerType.random:
            int index = Random.Range(0, Net_manager.Players.Count);
            if (shouldBeInside && Net_manager.Players[index].currentRoom !=0)
            {
                playerFound.Value = Net_manager.Players[index].transform;
            }
            else
            {
                index = Random.Range(0, Net_manager.Players.Count);
                if (shouldBeInside && Net_manager.Players[index].currentRoom !=0)
                {
                    playerFound.Value = Net_manager.Players[index].transform;
                }
            }
            break;
        }

        if (playerFound.Value == null)
        return TaskStatus.Failure;

        
        return TaskStatus.Success;
    }





}