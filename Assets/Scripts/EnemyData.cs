using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemies/New Enemy Data")]

public class EnemyData : ScriptableObject
{
    [field: SerializeField] public EnemyController EnemyPrefab { get; private set; }
    [field: SerializeField] public float movementSpeed { get; private set; }
    [field: SerializeField] public float maxHealth { get; private set; }
    [field: SerializeField] public float searchRadius { get; private set; }
    [field: SerializeField] public float movementFrequency { get; private set; }


}
