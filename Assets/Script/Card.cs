using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
	static int numberOfCard;
	public string cardName;

	public bool canBePicked;
	//public bool picked = false;
	int lifeTime = 0;

	public string content;
	public Answer[] answers;

	// public Character character;

	public void decrementTurn()
	{
		// decrement lifetime if not null
		// if lifetime = 0 ?
		// set picked to false
	}

	public bool IsEqual(Card c)
	{
		return this.GetInstanceID() == c.GetInstanceID();
	}

}
