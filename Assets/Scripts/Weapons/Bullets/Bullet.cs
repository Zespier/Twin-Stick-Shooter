using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed = 20f;

    [HideInInspector] public WeaponController weaponController;
    private float _deathTimer;

    private void Update() {
        _deathTimer += Time.deltaTime;

        if (_deathTimer >= 3) {
            Deactivate();
        }

        if (!weaponController.bulletLivingArea.Contains(transform.position)) {
            Deactivate();
        }
    }

    public void Shoot(Vector2 direction, float desviationAngle) {
        Rotate(direction);
        transform.up = BulletFireDesviation.RandomBulletFireDesviation2D(transform, desviationAngle, ShootDirectionReference.up);
        rb.velocity = speed * transform.up;
    }

    private void Rotate(Vector2 direction) {
        transform.up = direction;
    }

    private void Deactivate() {
        gameObject.SetActive(false);
        _deathTimer = 0;
    }
}
