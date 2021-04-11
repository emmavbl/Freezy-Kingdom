using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSign : MonoBehaviour
{
    void Start()
    {
        Vector3 scale = this.transform.localScale;
        this.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, scale, 0.5f).setOnComplete(Growing); 

    }

    void Growing()
	{
        LeanTween.moveY(gameObject, 500, 2).setDestroyOnComplete(true);
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 2).setEaseInOutQuart();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
