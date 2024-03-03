using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInConeState : AttackBaseState {
    
    public Transform shootPoint;
    public Transform bulletContainer;
    public GameObject bulletPrefab;
    public float fireRate = 8f;
    public float shootingDuration = 3f;
    public float rotationSpeed = 150f;

    private float _shootTimer;
    private float _durationTimer;

    public void Shoot() {
        Bullet newBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, bulletContainer).GetComponent<Bullet>();
        newBullet.Shoot(shootPoint.up, 2f);
    }

    public override void OnStateEnter() {
        _shootTimer = Time.time;
        _durationTimer = 0;
        Turret turret = controller as Turret;
        if (turret != null) {
            turret.RotationSpeed = rotationSpeed;
        }
    }

    public override void OnStateExit() {
    }

    public override void StateLateUpdate() {
        _durationTimer += Time.deltaTime;
        if (_durationTimer >= shootingDuration) {
            controller.ChangeState(typeof(GuardState));
        }
    }

    public override void StateUpdate() {
        if (_shootTimer + 1f / fireRate < Time.time) {
            _shootTimer = Time.time;
            Shoot();
        }
    }
}
