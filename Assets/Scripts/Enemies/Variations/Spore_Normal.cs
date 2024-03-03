using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore_Normal : Enemy {

    [Header("Spore_Normal Attributes")]
    public float speed_Spore_Normal = 6f;
    public float distanceToReachPlayer_Spore_Normal = 5f;

    public override float Speed => speed_Spore_Normal;
    public override float DistanceToReachPlayer => distanceToReachPlayer_Spore_Normal;

    #region Behaviour with States

    public override void FinishedShooting() {
        ChangeState(typeof(ZombieState));
    }

    #endregion

    public override void ReachingPlayer() {
        ChangeState(typeof(ShootInConeState));
    }

    private void Attack() {
        //TODO: Damageplayer
    }

}
