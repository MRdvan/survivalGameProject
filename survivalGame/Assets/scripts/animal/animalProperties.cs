using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu( fileName = "animal", menuName = "new animal")]
public class animalProperties : ScriptableObject
{
    public int health = 100;
    public int index = 0;
    public Vector3[] positions;
    
    public float runSpeed;
    public float fleeSpeed;
    public float patrolSpeed;
    public float fleeDistance;
    public float runDistance;

    public List<GameObject> meats = new List<GameObject>();
}
