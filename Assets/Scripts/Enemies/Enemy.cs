using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable {

    public float speed = 10f;

    [Header("Damageable")]
    public float hp;

    #region Taking damage and deactivation

    public override void TakeDamage(Vector3 position, float damage, bool crit, string damageType) {
        base.TakeDamage(position, damage, crit, damageType);

        hp -= damage;
        CheckDeath();
    }

    private void CheckDeath() {
        if (hp < 0) {
            Deactivate();
        }
    }

    private void Deactivate() {
        gameObject.SetActive(false);

    }

    #endregion
}
