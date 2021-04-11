using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCinematic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Stop("GameOver");

		for (int i = 0; i < 9; i++)
		{
            FindObjectOfType<AudioManager>().Play("Penguin_3", Random.Range(0, 0.8f));
            FindObjectOfType<AudioManager>().Play("Penguin_8", Random.Range(0, 0.8f));

        }

        string[] tab = new string[]
        {
            "Philéas", "Louve", "Raphael", "Mael", "Owen", "Wesley", "Emma", "Océane", "Noa", 
            "Salim", "Nicola", "Lucas", "Lara", "Evan", "Titouan", "Charles", "Hugo","Mathieu", "Angie", "Éloïse"
        };
        Image[] bulles = GetComponentsInChildren<Image>();
		for (int i = 2; i < 4; i++)
		{
            bulles[i].transform.localScale = new Vector3(0, 0, 0);
            LeanTween.scale(bulles[i].gameObject, new Vector3(1, 1, 1), 0.5f).setEaseOutBack().setDelay((i-2)*2f);
			if (i == 3)
			{
                bulles[i].GetComponentInChildren<Text>().text = "Oh non c'est toujours toi " + tab[Random.Range(0, tab.Length)] + " !";
			}
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
	{
        FindObjectOfType<GameManager>().GetScene(2);
    }
}
