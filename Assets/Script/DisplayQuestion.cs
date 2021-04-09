using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class DisplayQuestion : MonoBehaviour
{
    public Card card;
    public Text questionText;
    public GameObject buttonPrefab;

    public GameObject[] buttonsPosition;
    public GameObject characterPosition;

    // Start is called before the first frame update
    void Start()
    {
		GameManager GM = FindObjectOfType<GameManager>();
		//GM.played.Add(GM.playable.Remove(card));
		GM.Game();

		questionText.text = card.content;

        AddButtons();

        // set card as picked

    }

    public void UpdateToCard(Card _card)
	{
        card = _card;

        questionText.text = _card.content;

        //destroy previous character
        if (characterPosition.transform.childCount > 0)
		{
            var character = characterPosition.transform.GetChild(0);
            Debug.Log(character.gameObject);
            Destroy(character.gameObject);
		}

        // destroy previous buttons
        Button[] buttons =  GetComponentsInChildren<Button>();
		foreach (Button button in buttons)
		{
            Destroy(button.gameObject);
		}

        // give them back !
        AddButtons();

        Instantiate(card.character,
                characterPosition.transform.position,
                Quaternion.identity,
                characterPosition.transform);

	}

    void AddButtons()
	{
		for (int i = 0; i < card.answers.Length; i++)
		{
            Answer a = card.answers[i];
            GameObject temp = Instantiate(buttonPrefab, buttonsPosition[i].transform.position, Quaternion.identity, transform);
            temp.GetComponentInChildren<Text>().text = a.content;
            temp.GetComponent<ButtonAnswer>().answer = a;
        }
	}


}
