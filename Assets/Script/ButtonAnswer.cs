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
		FindObjectOfType<GameManager>().DisplayNextCard();
	}

}
