using System;
using UnityEngine;

public class ReadControllType : MonoBehaviour {

    public void ReadInputType()
    {
        StaticControll.numberOfPlayers = 0;
        StaticControll.inputs = new InputType.Type[4]; //reset all inputs to InputType.Type.Noone
        foreach (Transform child in transform)
        {
            InputType type = child.GetComponentInChildren<InputType>();
            if (type)
            {
                StaticControll.inputs[StaticControll.numberOfPlayers] = type.getType;
                StaticControll.numberOfPlayers++;
            }
        } 
    }

    public void PrintInputType()
        {
        string newPrint = StaticControll.numberOfPlayers.ToString() + " - ";
        for (int i = 0; i<StaticControll.inputs.Length; i++)
            {
            newPrint += StaticControll.inputs[i].ToString();
            newPrint += " ";
            }

        print(newPrint);
        }

    
}
