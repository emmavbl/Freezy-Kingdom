using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayQuestion : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI questionText;

    public Button[] answerButtons;
    // Start is called before the first frame update
    void Start()
    {
        questionText.text = card.content;

		for (int i = 0; i < card.answers.Length; i++)
		{
            Answer a = card.answers[i];
            Debug.Log(answerButtons[i]);
            answerButtons[i].GetComponentInChildren<Text>().text = a.content;
        }

    }


}
