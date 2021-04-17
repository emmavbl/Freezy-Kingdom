using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayQuestion : MonoBehaviour
{
    public Card card;
    public Text questionText;
    public GameObject buttonPrefab;

    public GameObject[] buttonsPosition;
    public GameObject characterPosition;
    public GameObject backgroundPlace;
    public GameObject backgroundPosition;
    
    [SerializeField] Sprite schoolSprite;
    [SerializeField] Sprite fishingSprite;
    [SerializeField] Sprite bankSprite;

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
        Debug.Log(GameManager.currentPlace);
        // set background
        Sprite background = null;
        Destroy(backgroundPosition.GetComponentInChildren<Image>());
        if (GameManager.currentPlace != Place.Castle)
            FindObjectOfType<AudioManager>().Stop("Castle");

        switch (GameManager.currentPlace)
		{
			case Place.Fishing:
                FindObjectOfType<AudioManager>().Play("Fishing");
                background = fishingSprite;
				break;
			case Place.School:
                FindObjectOfType<AudioManager>().Play("School");
                background = schoolSprite;
                break;
			case Place.Bank:
                FindObjectOfType<AudioManager>().Play("Bank");
                background = bankSprite;
				break;
			default:
                FindObjectOfType<AudioManager>().Play("Castle");
                background = null;
				break;
		}
        if (background != null)
		{
            var obj = Instantiate(backgroundPlace, backgroundPosition.transform.position, Quaternion.identity, backgroundPosition.transform);
            obj.GetComponentInChildren<Image>().sprite = background;
		}


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

        // get character 
        Vector3 position = new Vector3(-100, characterPosition.transform.position.y, characterPosition.transform.position.z);
        var temp = Instantiate(card.character,
                position,
                Quaternion.identity,
                characterPosition.transform);
        LeanTween.moveX(temp, characterPosition.transform.position.x, 0.5f).setEaseOutBack();
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
