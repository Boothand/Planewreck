using System;
using UnityEngine;

public class ReadControllType : MonoBehaviour {

    public void ReadInputType()
    {
        PlayerControllers.numberOfPlayers = 0;
        PlayerControllers.inputs = new PlayerControllers.ControllType[4]; //reset all inputs to InputType.Type.Noone
        foreach (Transform child in transform)
        {
            SetupPlayers type = child.GetComponentInChildren<SetupPlayers>();
            if (type)
            {
                PlayerControllers.inputs[PlayerControllers.numberOfPlayers] = type.getType;
                PlayerControllers.numberOfPlayers++;
            }
        } 
    }

    public void PrintInputType()
        {
        string newPrint = PlayerControllers.numberOfPlayers.ToString() + " - ";
        for (int i = 0; i<PlayerControllers.inputs.Length; i++)
            {
            newPrint += PlayerControllers.inputs[i].ToString();
            newPrint += " ";
            }

        print(newPrint);
        }

    
}
