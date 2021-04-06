using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Answer", menuName = "Answer")]
public class Answer : ScriptableObject
{
	public string content;

	[SerializeField] public Stats stats; 
	public Card[] cardToUnlock;

	// End endToUnlock;
	// Deck deckToUnlock;

	// Place placeToUnlock

	public void Action()
	{
		Debug.Log("Activate answer " + content);

		FindObjectOfType<GameManager>().AddStats(stats);
		// store stats in game manager

		// si possible :
			// put card as playable
			// add deck to playable
			// set end in game manager

		// add place to map
	}
}
