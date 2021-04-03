using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Answer", menuName = "Answer")]
public class Answer : ScriptableObject
{
	public string content;

	public float wealth;
	public float ecosystem;
	public float community;

	public Card[] toUnlock;


	void Action()
	{
		Debug.Log("Activate answer " + content);
	}
}
