using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore_Small : Enemy {

    [Header("Spore_Small Attributes")]
    public float speed_Spore_Small = 15f;
    public float distanceToReachPlayer_Spore_Small = 0.1f;

    private float _attackTimer;

    public override float Speed => speed_Spore_Small;
    public override float DistanceToReachPlayer => distanceToReachPlayer_Spore_Small;

    public override void ReachingPlayer() {

        if (_attackTimer + (1f / attackRate) < Time.time) {

            _attackTimer = Time.time;
            Attack();

        } else {
            return;
        }
    }

    private void Attack() {
        //TODO: Damageplayer
    }

}
