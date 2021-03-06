using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinguinGenerator : MonoBehaviour
{

    [SerializeField] GameObject[] pinguins;
    public int pinguinNumber;


    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < pinguinNumber; i++)
		{
            GameObject temp = pinguins[Random.Range(0, pinguins.Length)];
            Vector3 offset = new Vector3(
                transform.position.x + Random.Range(-800, 800),
                transform.position.y + Random.Range(-50,0),
                0
                );
            ;
            var tmp = Instantiate(temp, transform.position + offset , Quaternion.identity, transform);
            tmp.transform.localScale = new Vector3(1, 1, 1) * i / pinguinNumber;
		}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
