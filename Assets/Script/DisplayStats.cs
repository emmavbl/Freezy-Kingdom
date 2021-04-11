using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{

    [SerializeField] GameObject[] statsPosition;
    [SerializeField] GameObject statText;
    [SerializeField] GameObject plus;
    [SerializeField] GameObject moins;

    [SerializeField] GameObject happy;
    [SerializeField] GameObject anger;
    [SerializeField] GameObject black;

    // Start is called before the first frame update
    void Start()
    {

    }


    public void OnClick()
	{
        Button button = GetComponentInChildren<Button>();
        FindObjectOfType<AudioManager>().Play("Q2");
        LeanTween.scale(button.gameObject, new Vector3(0, 0, 0), .5f).setEaseInBack();
        Invoke("EndNigth", 0.5f);
    }

    public void EndNigth()
    {

        var GM = FindObjectOfType<GameManager>();

        if (GM.end)
        {
            Debug.Log("Game Over");
            GM.GameOver();
            return;
        };

        Destroy(GetComponent<DisplayStats>().gameObject);
        GM.Turn();
    }

    public void SetStats(Stats stats, List<Stats> turnStats)
	{
        PinguinAnimation[] pinguins = GetComponentsInChildren<PinguinAnimation>();
        float[] gameStats = new float[3] {stats.wealth, stats.community, stats.ecosystem };

		for (int i = 0; i < 3; i++)
		{
            RectTransform[] positions = pinguins[i].GetComponentsInChildren<RectTransform>();

			if (gameStats[i] >= 10) //si le pigouin est content
			{
                var GO_happy = Instantiate(happy,
                    positions[2].transform.position, 
                    Quaternion.identity,
                    positions[2].transform);
                GO_happy.transform.localScale = new Vector3(1f, 1f, 1f) * ((Mathf.Abs(gameStats[i]-10) / 10)+0.2f);

                var GO_black = Instantiate(black,
                    positions[1].transform.position,
                    Quaternion.identity,
                    positions[1].transform);
                GO_black.GetComponent<CanvasGroup>().alpha = (Mathf.Abs(gameStats[i] - 10f) / 10f)+0.2f;
            }
			else
			{
                var GO_anger = Instantiate(anger,
                    positions[2].transform.position,
                    Quaternion.identity,
                    positions[2].transform);
                GO_anger.transform.localScale = new Vector3(1f, 1f, 1f) * ((Mathf.Abs(gameStats[i] - 10) / 10)+0.2f);

            }
        }

		for (int i = 0; i < turnStats.Count; i++)
		{
            StartCoroutine(DisplayPoints(turnStats[i], (2*i) + .5f));
		}
    }

    IEnumerator DisplayPoints(Stats stats, float delay)
	{
        yield return new WaitForSeconds(delay);
        Debug.Log(stats.Print());

        GameObject wealth = null;
        if (stats.wealth > 0)
        {
            FindObjectOfType<AudioManager>().Play("Jauge+1");
            wealth = Instantiate(plus,
                statsPosition[0].transform.position,
                Quaternion.identity,
                transform);
        }
        else if (stats.wealth < 0)
        {
            FindObjectOfType<AudioManager>().Play("Jauge-1");
            wealth = Instantiate(moins,
                statsPosition[0].transform.position,
                Quaternion.identity,
                transform);
        }
		if (wealth != null)
		{
			wealth.transform.localScale = new Vector3(1, 1, 1) * (1 + ((Mathf.Abs(stats.wealth)-1)*0.5f));
		}

        GameObject community = null;
        if (stats.community > 0)
        {
            FindObjectOfType<AudioManager>().Play("Jauge+2");
            community = Instantiate(plus,
                statsPosition[1].transform.position,
                Quaternion.identity,
                transform);
        }
        else if (stats.community < 0)
        {
            FindObjectOfType<AudioManager>().Play("Jauge-2");
            community = Instantiate(moins,
                statsPosition[1].transform.position,
                Quaternion.identity,
                transform);
        }
		if (community != null)
		{
			community.transform.localScale = new Vector3(1, 1, 1) * (1 + ((Mathf.Abs(stats.community)-1) * 0.5f));
		}

        GameObject ecosystem = null;
        if (stats.ecosystem > 0)
        {
            FindObjectOfType<AudioManager>().Play("Jauge+3");
            ecosystem = Instantiate(plus,
                statsPosition[2].transform.position,
                Quaternion.identity,
                transform);
        }
        else if (stats.ecosystem < 0)
        {
            FindObjectOfType<AudioManager>().Play("Jauge-3");
            ecosystem = Instantiate(moins,
                statsPosition[2].transform.position,
                Quaternion.identity,
                transform);
        }
		if (ecosystem != null)
		{
			ecosystem.transform.localScale = new Vector3(1, 1, 1) * ( 1 + ((Mathf.Abs(stats.ecosystem)-1) *0.5f));
		}

    }
}
