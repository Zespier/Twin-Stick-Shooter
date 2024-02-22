using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed = 20f;
    public float damage = 1f;
    public float baseDamagePercentage = 100f;

    [HideInInspector] public WeaponController weaponController;
    protected float _deathTimer;

    public float DamagePercentage { get => (baseDamagePercentage/* + PlayerStats.instance.*/) / 100f; }

    private void Update() {
        //_deathTimer += Time.deltaTime;

        //if (_deathTimer >= 3) {
        //    Deactivate();
        //}

    }

    /// <summary>
    /// Shoots this bullet in the direction specified with a random angle desviation
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="desviationAngle"></param>
    public void Shoot(Vector2 direction, float desviationAngle) {
        Rotate(direction);
        transform.up = BulletFireDesviation.RandomBulletFireDesviation2D(transform, desviationAngle, ShootDirectionReference.up);
        rb.velocity = speed * transform.up;
    }

    /// <summary>
    /// Rotates towards the specified direction
    /// </summary>
    /// <param name="direction"></param>
    private void Rotate(Vector2 direction) {
        transform.up = direction;
    }

    /// <summary>
    /// Deactivates and resets the bullet, ready for pooling
    /// </summary>
    public virtual void Deactivate() {
        gameObject.SetActive(false);
        _deathTimer = 0;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.TryGetComponent(out IDamageable damageable)) {
            damageable.TakeDamage(collision.transform.position, damage * DamagePercentage, Random.Range(0, 100) < 10, "energy");
            Deactivate();
        }
    }

}