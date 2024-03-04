using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore_Mind : MonoBehaviour {

    public GameObject spore_Big;
    public GameObject spore_Normal;
    public GameObject spore_Small;
    public Spore_Zone spore_Zone;

    public float spore_Zone_MediumSize = 1.5f;
    public float spore_Zone_BigSize = 2f;
    public float spore_Zone_SpawningTime = 3f;
    public float spore_Zone_lifetime = 10f;

    [Header("Plague Control")]
    public int totalSporeZones;

    public static Spore_Mind instance;
    private void Awake() {
        if (!instance) { instance = this; }
    }

    private void OnEnable() {
        Events.OnEnemyDeath += SporeDeath;
    }

    private void OnDisable() {
        Events.OnEnemyDeath -= SporeDeath;
    }

    public void SpawnSpore(GameObject prefab, Vector3 position) {
        Instantiate(prefab, position, Quaternion.identity, EnemyContainer.instance.transform);
    }

    public void Spore_Zone(Vector3 position, Spore_ZoneSize spore_ZoneSize) {

        Spore_Zone newSpore_zone = Instantiate(spore_Zone, position, Quaternion.identity, EnemyContainer.instance.transform);
        newSpore_zone.Size = spore_ZoneSize;
    }

    public void SporeDeath(Enemy spore) {

        Enum.TryParse(spore.GetType().ToString(), out SporeType sporeType);

        switch (sporeType) {
            case SporeType.none:
                break;
            case SporeType.Spore_Small:
                break;
            case SporeType.Spore_Normal:
                SpawnSpore(spore_Small, spore.transform.position);
                break;
            case SporeType.Spore_Big:
                SpawnSpore(spore_Normal, spore.transform.position);
                SpawnSpore(spore_Normal, spore.transform.position);
                Spore_Zone(spore.transform.position, Spore_ZoneSize.big);
                break;
            default:
                break;
        }
    }
}

public enum SporeType {
    none,
    Spore_Small,
    Spore_Normal,
    Spore_Big,
}

