using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New End", menuName = "End")]
public class End : ScriptableObject
{
	public string endName;
	[TextArea(15, 20)]
	public string description;
	public int sceneId;
	
}
