using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
	static int numberOfCard;
	public string cardName;

	public bool canBePicked;
	//public bool picked = false;
	public int lifeTime = 0;

	public string content;
	public Answer[] answers;

	public GameObject character;


	// init lifetime for the first use
	public void initLifeTime()
	{
		lifeTime = 2;
	}

	public bool IsEqual(Card c)
	{
		return this.GetInstanceID() == c.GetInstanceID();
	}

}
