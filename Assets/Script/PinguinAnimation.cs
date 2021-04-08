using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinguinAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveY(gameObject, transform.position.y + 100, .5f + Random.Range(0f,0.5f)).setLoopPingPong().setEaseInExpo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
