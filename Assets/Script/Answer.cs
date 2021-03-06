using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Answer", menuName = "Answer")]
public class Answer : ScriptableObject
{
	[TextArea(15, 20)] 
	public string content;

	public Card question; 
	[SerializeField] public Stats stats;
	public Card[] cardToUnlock;
	public End endToUnlock = null;

	public Place placeToUnlock;

	public int lifetimeQuestion = 15;

	public int morningAnswer = 0; // -1:reponse n?gative, 0:n'est pas morning card, 1: reponse positive; 


	public void Action()
	{

		// store stats in game manager
		GameManager GM = FindObjectOfType<GameManager>();
		if(morningAnswer == 0) GM.AddStats(stats);


		if (morningAnswer == 1) // special morning question is answered yes 
		{
			switch (placeToUnlock)
			{
				case Place.Castle:
					GameManager.currentGameplayDeck = GameManager.gameplayDeck;
					GameManager.currentPlace = Place.Castle;
					break;
				case Place.Fishing:
					GameManager.currentGameplayDeck = GameManager.fishingGameplayDeck;
					GameManager.currentPlace = Place.Fishing;
					break;
				case Place.School:
					GameManager.currentGameplayDeck = GameManager.schoolGameplayDeck;
					GameManager.currentPlace = Place.School;
					break;
				case Place.Bank:
					GameManager.currentGameplayDeck = GameManager.bankGameplayDeck;
					GameManager.currentPlace = Place.Bank;
					break;
				default:
					GameManager.currentGameplayDeck = GameManager.gameplayDeck;
					GameManager.currentPlace = Place.Castle;
					break;
			}

			// check if some played.cards are replayable 
			GM.CheckReusableCard();
			GM.ConsoleDecks();
			// genere le deck de 3 question
			GameManager.turnDeck = GM.GenerateDayDeck(3);

		} else // if not special question unlock what is to unlock
		{
			//set gameplay deck to normal gameplay deck
			//GameManager.currentGameplayDeck = GameManager.gameplayDeck;

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

	}
}
