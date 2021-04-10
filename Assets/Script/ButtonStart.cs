using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStart : MonoBehaviour
{
    public void OnClick()
	{
        FindObjectOfType<AudioManager>().Play("Q1");
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), .5f).setEaseInBack().setOnComplete(Quit);

    }
    private void Quit()
	{
        FindObjectOfType<GameManager>().GetScene(1);

    }

	// Start is called before the first frame update
	void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), .5f).setEaseOutBack();
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), .5f).setEaseOutBack();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
