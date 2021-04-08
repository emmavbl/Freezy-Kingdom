using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{

    [SerializeField] GameObject[] statsPosition;
    [SerializeField] GameObject statText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
            GameObject temp = Instantiate(statText,
                statsPosition[i].transform.position,
                Quaternion.identity,
                transform);
            temp.GetComponentInChildren<Text>().text = stats[i].Print(); ;

        }
    }
}
