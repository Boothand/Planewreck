using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private bool ignore;
	private PlayerProperties[] players;
	private OverviewCamera cam;

	private PlayerProperties roundWinner;
	private PlayerProperties gameWinner;

	private bool initiated;
	private bool givingPraiseToRoundWinner;

	private int roundCountDownTime = 3;
	private int requiredWins = 5;
	private int alivePlayers;

	private InGameUI ui;

	private GameState state = GameState.PreGame;

	private enum GameState
	{
		PreGame,
		Preround,
		Round,
		RoundEnd,
		GameEnd,
		None
	}

	void Start ()
	{
		if (ignore)
		{
			return;
		}

		ui = transform.GetComponentInChildren<InGameUI>();

		players = FindObjectsOfType<PlayerProperties>();
		cam = FindObjectOfType<OverviewCamera>();
		alivePlayers = players.Length;
	}

	IEnumerator InitiateRound()
	{
		initiated = true;
		roundWinner = null;
		gameWinner = null;  //Just in case..
		float camResetTime = 1f;

		cam.SetCameraTargetPosition(cam.StartPosition, camResetTime);

		yield return new WaitForSeconds(camResetTime);

		int count = roundCountDownTime;

		while (count > 0)
		{
			ui.SetMiddleScreenText(count.ToString());
			count--;
			print(count);

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
		givingPraiseToRoundWinner = true;

		float zoomTime = 0.5f;
		cam.ZoomOnTarget(roundWinner.transform, zoomTime, 1f);

		yield return new WaitForSeconds(zoomTime);

		ui.SetMiddleScreenText(roundWinner.PlayerName + " wins the round!", true);

		yield return new WaitForSeconds(3f);

		ui.DisableMiddleScreenText();

		if (roundWinner.Wins == requiredWins)
		{
			state = GameState.GameEnd;
		}
		else
		{
			state = GameState.Preround;
		}

		givingPraiseToRoundWinner = false;
	}

	void UpdatePerServer()
	{
		switch (state)
		{
			case GameState.PreGame:

				if (alivePlayers > 1)
				{
					state = GameState.Preround;
				}
				else
				{
					Debug.Log("Not enough players to start.");
					state = GameState.None;
				}

				break;

			case GameState.Preround:

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

		foreach (PlayerProperties player in players)
		{
			if (!player.Health.Dead)
			{
				currentAlivePlayers++;
			}

			switch (state)
			{
				case GameState.PreGame:

					player.DisableInput();
					player.FreezePosition();

					break;

				case GameState.Preround:

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
						roundWinner = player;
					}

					break;

				case GameState.RoundEnd:

					break;

				case GameState.GameEnd:

					break;
				case GameState.None:

					break;
			}
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