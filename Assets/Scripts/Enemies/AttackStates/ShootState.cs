using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : AttackBaseState {

    public Transform shootPoint;
    public Transform bulletContainer;
    public GameObject bulletPrefab;
    public float fireRate = 8f;
    public float shootingDuration = -1f;

    private float _shootTimer;
    private float _shootDurationTime;

    public void Shoot() {
        Bullet newBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, bulletContainer).GetComponent<Bullet>();
        newBullet.Shoot(shootPoint.up, 2f);
    }

    public override void OnStateEnter() {
        _shootDurationTime = 0;
        _shootTimer = Time.time;
    }

    public override void OnStateExit() {
    }

    public override void StateLateUpdate() {
        if (shootingDuration == -1) { return; }

        _shootDurationTime += Time.deltaTime;
        if (_shootDurationTime >= shootingDuration) {
            controller.FinishedShooting();
        }
    }

    public override void StateUpdate() {
        if (_shootTimer + 1f / fireRate < Time.time) {
            _shootTimer = Time.time;
            Shoot();
        }
    }
}
