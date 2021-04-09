using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Answer", menuName = "Answer")]
public class Answer : ScriptableObject
{
	public string content;

	public Card question; 
	[SerializeField] public Stats stats;
	public Card[] cardToUnlock;
	public End endToUnlock = null;

	public Place placeToUnlock;

	public int lifetimeQuestion = 15;

	public int morningAnswer = 0; // -1:reponse négative, 0:n'est pas morning card, 1: reponse positive; 


	public void Action()
	{

		// store stats in game manager
		GameManager GM = FindObjectOfType<GameManager>();
		GM.AddStats(stats);

		if (morningAnswer == 0)
		{
			// put card as playable (notplayable/played -> playable)
			GM.FindAndPutInPlayable(cardToUnlock);

			// add place to Game Manager
			if (placeToUnlock != Place.Castle)
			{
				GM.AddPlace(placeToUnlock);
			}

			// set end in game manager
			if (endToUnlock != null)
			{
				Debug.Log("End triggered");
				GM.end = true;
				GM.activatedEnd = endToUnlock;
			}

			question.lifeTime = lifetimeQuestion;
		}
		else if (morningAnswer == 1)
		{
			switch (placeToUnlock)
			{
				case Place.Castle:
					GameManager.currentGameplayDeck = GameManager.gameplayDeck;
					break;
				case Place.Fishing:
					GameManager.currentGameplayDeck = GameManager.fishingGameplayDeck;
					break;
				case Place.School:
					GameManager.currentGameplayDeck = GameManager.schoolGameplayDeck;
					break;
				case Place.Bank:
					GameManager.currentGameplayDeck = GameManager.bankGameplayDeck;
					break;
				default:
					GameManager.currentGameplayDeck = GameManager.gameplayDeck;
					break;
			}
			// check if some played.cards are replayable 
			GM.CheckReusableCard();
			GM.ConsoleDecks();
			// genere le deck de 3 question
			GameManager.turnDeck = GM.GenerateDayDeck(3);

			GM.DisplayNextCard();
		}

	}
}
