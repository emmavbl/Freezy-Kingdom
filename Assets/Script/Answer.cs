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

	public Deck deckToUnlock;

	public Place placeToUnlock;

	public int lifetimeQuestion = 15;

	public void Action()
	{

		// store stats in game manager
		GameManager GM = FindObjectOfType<GameManager>();
		GM.AddStats(stats);

	// si possible :
		// put card as playable (notplayable/played -> playable)
		GM.FindAndPutInPlayable(cardToUnlock);

		// add deck to playable
		if(deckToUnlock != null)
		{
			GM.AddDeck(deckToUnlock);
		}
		
		// add place to Game Manager
		if(placeToUnlock != Place.Castle)
		{
			GM.AddPlace(placeToUnlock);
		}
		
		// set end in game manager
		if (endToUnlock != null)
		{
			Debug.Log("End triggered");
			GM.end = true;
			GM.activatedEnd= endToUnlock;
		}

		question.lifeTime = lifetimeQuestion;

	}
}
