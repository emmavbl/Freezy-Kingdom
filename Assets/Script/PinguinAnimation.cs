using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinguinAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveY(gameObject, transform.position.y + 15, .6f).setLoopPingPong().setEaseInOutSine().setDelay(Random.Range(0f, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
