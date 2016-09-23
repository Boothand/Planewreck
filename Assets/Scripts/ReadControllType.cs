using System;
using UnityEngine;

public class ReadControllType : MonoBehaviour {

    public void ReadInputType()
    {
        StaticControll.numberOfPlayers = 0;
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

    private void PrintInputType()
        {
        string newPrint = StaticControll.numberOfPlayers.ToString() + " - ";
        for (int i = 0; i<StaticControll.numberOfPlayers; i++)
            {
            newPrint += StaticControll.inputs[i].ToString();
            newPrint += " ";
            }

        print(newPrint);
        }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ReadInputType();

        if (Input.GetKeyDown(KeyCode.P))
            PrintInputType();
    }

    
}
