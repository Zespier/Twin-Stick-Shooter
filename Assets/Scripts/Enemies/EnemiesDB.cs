using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesDB", menuName = "Script. Object/EnemiesDB")]
public class EnemiesDB : ScriptableObject {

    public List<GameObject> enemies = new List<GameObject>();

}
