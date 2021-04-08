using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyElement : MonoBehaviour
{

	[SerializeField] float time;
	[SerializeField] LeanTweenType easeType;
	Vector3 position;

	private void OnEnable()
	{
		LeanTween.scale(gameObject, new Vector3(.95f, .955f, .95f), time).setLoopPingPong().setEase(easeType);
	}
}

