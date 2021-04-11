using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnswer : MonoBehaviour
{
    public Answer answer;

    public void onClick()
	{
		FindObjectOfType<AudioManager>().Play("Q1");
		answer.Action();
		LeanTween.moveX(FindObjectOfType<PinguinAnimation>().gameObject, 2100, 0.6f).setEaseInBack();
		Invoke("NextCard", 0.6f);
	}

	void NextCard()
	{
		FindObjectOfType<GameManager>().DisplayNextCard();
	}

}
