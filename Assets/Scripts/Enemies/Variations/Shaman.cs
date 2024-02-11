using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : Enemy {

    [Header("Shaman Attributes")]
    public float shamanSpeed = 5f;

    public override float Speed => shamanSpeed;

    public override void ReachPlayer() {
        Explode();
    }

    private void Explode() {

    }

}
