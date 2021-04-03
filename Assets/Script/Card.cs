using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
	public string cardName;

	public bool canBePicked;
	public bool picked;

	public string content;
	public Answer[] answers;

}
