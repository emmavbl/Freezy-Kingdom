using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayQuestion : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI questionText;
    public GameObject buttonPrefab;

    public GameObject[] buttonsPosition;
    // Start is called before the first frame update
    void Start()
    {
        questionText.text = card.content;

		for (int i = 0; i < card.answers.Length; i++)
		{
            Answer a = card.answers[i];
            GameObject temp = Instantiate(buttonPrefab, buttonsPosition[i].transform.position, Quaternion.identity, transform);
            temp.GetComponentInChildren<Text>().text = a.content;
            temp.GetComponent<ButtonAnswer>().answer = a;
        }

    }


}
