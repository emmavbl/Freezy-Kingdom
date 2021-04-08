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
                transform.position.x + Random.Range(-1000, 1000),
                transform.position.y + Random.Range(-150, -100),
                0
                );
            ;
            Instantiate(temp, transform.position + offset , Quaternion.identity, transform);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
