using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private bool ignore;
	private List<PlayerProperties> players = new List<PlayerProperties>();
	//private PlayerProperties[] players = new PlayerProperties[4];
	private OverviewCamera cam;

	[SerializeField]
	private GameObject playerPrefab;

	[SerializeField]
	private Transform[] spawnPositions = new Transform[4];

	private PlayerProperties roundWinner;
	private PlayerProperties gameWinner;

	private bool initiated;
	private bool givingPraiseToRoundWinner;
	private bool roundWasDraw;

	private int roundCountDownTime = 3;
	private int requiredWins = 5;
	private int alivePlayers;

	private InGameUI ui;

	private GameState state = GameState.PreGame;

	private enum GameState
	{
		PreGame,
		PreRound,
		Round,
		RoundEnd,
		GameEnd,
		None
	}

	void Awake()
	{
		if (ignore)
		{
			return;
		}

		PlayerProperties[] earlyPlayers = FindObjectsOfType<PlayerProperties>();

		foreach (PlayerProperties props in earlyPlayers)
		{
			players.Add(props);
		}

		//Basically, if we got here via the set-up scene, spawn the rest of the planes..
		int numberOfPlayers = PlayerControllers.numberOfPlayers;
								//int numberOfPlayers = 4;

		
		int planesInScene = players.Count;

		for (int i = planesInScene; i < numberOfPlayers; i++)
		{
			GameObject instance = Instantiate(playerPrefab, spawnPositions[i].position, playerPrefab.transform.rotation);
			players.Add(instance.transform.GetChild(0).GetComponent<PlayerProperties>());
			instance.GetComponent<PlayerProperties>().SetProperties(i);
		}
		
	}

	void Start ()
	{
		if (ignore)
		{
			return;
		}		

		ui = transform.GetComponentInChildren<InGameUI>();

		cam = FindObjectOfType<OverviewCamera>();
		alivePlayers = players.Count;
	}

	IEnumerator InitiateRound()
	{
		initiated = true;
		roundWinner = null;
		gameWinner = null;  //Just in case..
		roundWasDraw = false;
		float camResetTime = 1f;

		cam.SetCameraTargetPosition(cam.StartPosition, camResetTime);

		yield return new WaitForSeconds(camResetTime);

		int count = roundCountDownTime;

		while (count > 0)
		{
			ui.SetMiddleScreenText(count.ToString());
			count--;

			yield return new WaitForSeconds(1f);
		}

		ui.SetMiddleScreenText("GO!");

		state = GameState.Round;

		initiated = false;

		yield return new WaitForSeconds(0.5f);

		ui.DisableMiddleScreenText();
	}

	IEnumerator GivePraiseToRoundWinner()
	{
		Time.timeScale = 0.2f;
		Time.fixedDeltaTime = 0.2f * 0.02f;
		givingPraiseToRoundWinner = true;

		float zoomTime = 0.5f;
		//cam.ZoomOnTarget(roundWinner.transform, zoomTime, 20f, 2f);	//Unfinished
		//cam.SetTracking(false);

		yield return new WaitForSecondsRealtime(zoomTime);

		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f;

		ui.SetMiddleScreenText(roundWinner.PlayerName + " wins the round!", true);

		yield return new WaitForSeconds(3f);

		ui.DisableMiddleScreenText();

		if (roundWinner.wins == requiredWins)
		{
			state = GameState.GameEnd;
		}
		else
		{
			state = GameState.PreRound;
		}

		givingPraiseToRoundWinner = false;
	}

	IEnumerator RoundDrawRoutine()
	{
		roundWasDraw = true;
		ui.SetMiddleScreenText("DRAW!");

		yield return new WaitForSeconds(2f);
		ui.DisableMiddleScreenText();

		state = GameState.PreRound;
	}

	void UpdatePerServer()
	{
		switch (state)
		{
			case GameState.PreGame:

				if (alivePlayers > 1)
				{
					state = GameState.PreRound;
				}
				else
				{
					Debug.Log("Not enough players to start.");
					state = GameState.None;
				}

				break;

			case GameState.PreRound:

				if (initiated)
				{
					return;
				}

				StartCoroutine(InitiateRound());

				break;

			case GameState.Round:

				if (roundWinner)
				{
					state = GameState.RoundEnd;
				}
				else
				{
					if (alivePlayers == 0)
					{
						if (!roundWasDraw)
						{
							StartCoroutine(RoundDrawRoutine());
						}
					}
				}
				break;

			case GameState.RoundEnd:

				if (!givingPraiseToRoundWinner)
				{
					StartCoroutine(GivePraiseToRoundWinner());
				}

				break;

			case GameState.GameEnd:

				break;

			case GameState.None:

				break;
		}
	}

	void UpdatePerPlayer()
	{
		int currentAlivePlayers = 0;
		string playerUIName = "Player";
		int playerIndex = 0;

		//print(players[3].name);

		foreach (PlayerProperties player in players)
		{
			if (!player.Health.Dead)
			{
				currentAlivePlayers++;
			}

			if (state >= GameState.PreRound &&
				state <= GameState.RoundEnd)
			{
				string temporaryName = playerUIName + " " + (playerIndex + 1).ToString();	//Replace me later with real names.
				ui.DrawPlayerScore(playerIndex, temporaryName, player.wins);
			}


			switch (state)
			{
				case GameState.PreGame:	//Never goes in here atm

					player.DisableInput();
					player.FreezePosition();

					break;

				case GameState.PreRound:
					
					if (PlayerControllers.inputs[playerIndex] != PlayerControllers.ControllType.Noone)
					{
						PlayerControllers.ControllType inputTypeToUse = PlayerControllers.inputs[playerIndex];
						player.SetInputType(inputTypeToUse);
						print(inputTypeToUse);
					}

					player.DisableInput();
					player.FreezePosition();					

					if (player.Health.Dead)
					{
						player.Revive();
					}

					player.ResetPosition();
					player.ResetSmasherPosition();

					break;
				case GameState.Round:

					player.EnableInput();
					player.UnFreezePosition();

					if (alivePlayers == 1 && !player.Health.Dead)
					{
						//This person wins the round.

						if (!roundWinner)
						{
							roundWinner = player;
							roundWinner.wins++;
						}
					}

					break;

				case GameState.RoundEnd:

					break;

				case GameState.GameEnd:

					break;
				case GameState.None:

					break;
			}

			playerIndex++;
		}

		alivePlayers = currentAlivePlayers;
	}

	void Update ()
	{
		if (ignore)
		{
			return;
		}

		//Do things for each server/game frame
		UpdatePerServer();

		//Do things for each player
		UpdatePerPlayer();		
	}
}