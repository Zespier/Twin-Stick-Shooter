using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy {

    public float turretSpeed = 0f;
    public Transform body;

    public override float Speed => turretSpeed;

    public float RotationSpeed { get; set; }

    protected override void Update() {
        base.Update();
        RotateBody();
    }

    protected override void FlipSprite() {

    }

    private void RotateBody() {
        float rotationValue = Vector3.SignedAngle(body.up, player.position - body.transform.position, Vector3.forward) > 0 ? RotationSpeed : -RotationSpeed;
        body.up = Quaternion.AngleAxis(rotationValue * Time.deltaTime, Vector3.forward) * body.up;
    }
}
