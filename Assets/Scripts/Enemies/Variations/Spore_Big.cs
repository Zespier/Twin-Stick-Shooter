using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spore_Big : Enemy {

    [Header("Spore_Big Attributes")]
    public float speed_Spore_Big = 5f;
    public ParticleSystem explosion;
    public BoxCollider2D boxCollider;

    private bool _exploded;

    public override float Speed => speed_Spore_Big;

    public override void ReachingPlayer() {
        Explode();
    }

    private void Explode() {
        if (_exploded) { return; }
        _exploded = true;

        sprite.enabled = false;
        explosion.Play();
        rb.velocity = Vector3.zero;
        rb.isKinematic = false;
        boxCollider.enabled = false;
        enabled = false;

        StartCoroutine(C_WaitSecondsToDestroy(explosion.main.startLifetime.constant + 0.1f));
    }

    private IEnumerator C_WaitSecondsToDestroy(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
