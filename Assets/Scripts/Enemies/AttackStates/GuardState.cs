using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardState : AttackBaseState {

    public float guardTime = 2f;
    public float rotationSpeed = 50f;

    private float _guardTimer;

    public override void OnStateEnter() {
        Turret turret = controller as Turret;
        if (turret != null) {
            turret.RotationSpeed = rotationSpeed;
        }

        _guardTimer = 0;
    }

    public override void OnStateExit() {
    }

    public override void StateLateUpdate() {
        _guardTimer += Time.deltaTime;
        if (_guardTimer >= guardTime) {
            controller.ChangeState(typeof(ShootState));
        }
    }

    public override void StateUpdate() {
    }
}
