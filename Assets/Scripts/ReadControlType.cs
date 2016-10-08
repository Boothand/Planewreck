using System;
using UnityEngine;

public class ReadControlType : MonoBehaviour
{

	public void ReadInputType()
	{
		GlobalVariables.numberOfPlayers = 0;
		GlobalVariables.inputs = new GlobalVariables.ControlType[4]; //reset all inputs to InputType.Type.Noone

		foreach (Transform child in transform)
		{
			SetupPlayers type = child.GetComponentInChildren<SetupPlayers>();

			if (type)
			{
				GlobalVariables.inputs[GlobalVariables.numberOfPlayers] = type.type;
				GlobalVariables.numberOfPlayers++;
			}
		}
	}

	public void PrintInputType()
	{
		string newPrint = GlobalVariables.numberOfPlayers.ToString() + " - ";
		for (int i = 0; i < GlobalVariables.inputs.Length; i++)
		{
			newPrint += GlobalVariables.inputs[i].ToString();
			newPrint += " ";
		}

		print(newPrint);
	}
}
