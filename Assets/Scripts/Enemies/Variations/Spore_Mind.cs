using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore_Mind : MonoBehaviour {

    public GameObject spore_Big;
    public GameObject spore_Normal;
    public GameObject spore_Small;

    private void OnEnable() {
        Events.OnEnemyDeath += SporeDeath;
    }

    private void OnDisable() {
        Events.OnEnemyDeath -= SporeDeath;
    }

    public void SporeDeath(Enemy spore) {

        Enum.TryParse(spore.GetType().ToString(), out SporeType sporeType);

        switch (sporeType) {
            case SporeType.Spore_Big:
                Instantiate(spore_Normal, spore.transform.position, Quaternion.identity, EnemyContainer.instance.transform);
                break;

            case SporeType.Spore_Normal:
                Instantiate(spore_Small, spore.transform.position, Quaternion.identity, EnemyContainer.instance.transform);
                break;

            case SporeType.Spore_Small:
                //Instantiate(spore_Small, spore.transform.position, Quaternion.identity, EnemyContainer.instance.transform);
                break;

            case SporeType.none:
                //Instantiate(spore_Small, spore.transform.position, Quaternion.identity, EnemyContainer.instance.transform);
                break;

            default:
                break;
        }
    }
}

public enum SporeType {
    none,
    Spore_Big,
    Spore_Normal,
    Spore_Small,
}
