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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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

    public void SetStats(List<Stats> stats)
	{
		for (int i = 0; i < stats.Count; i++)
		{
            StartCoroutine(DisplayPoints(stats[i], (2*i) + .5f));
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
