using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class Enemy : Damageable {

    [Header("Enemy Base Attributes")]

    public Rigidbody2D rb;
    public float speed = 10f;
    public Transform player;
    public SpriteRenderer sprite;

    public virtual float Speed => speed;

    [Header("Damageable")]
    public float hp = 1000f;

    [Header("States")]
    public List<AttackBaseState> states;
    public AttackBaseState currentState;

    private void Start() {
        currentState.OnStateEnter();
    }

    private void Update() {
        FlipSprite();
        currentState.StateUpdate();
    }

    private void LateUpdate() {
        currentState.StateLateUpdate();
    }

    //private void Movement() {
    //    Vector3 step = Time.deltaTime * speed * (player.position - transform.position).normalized;
    //    transform.position += step;
    //}

    private void FlipSprite() {
        sprite.flipX = (player.position - transform.position).x < 0;
    }

    public virtual void ChangeState(Type state) {
        if (!states.Exists(s => s.GetType() == state)) {
            Debug.LogError("This state is not registered");
            return;
        }

        currentState.OnStateExit();
        currentState = states.Find(s => s.GetType() == state);
        currentState.OnStateEnter();
    }

    #region Taking damage and deactivation

    public override void TakeDamage(Vector3 position, float damage, bool crit, string damageType) {
        base.TakeDamage(position, damage, crit, damageType);

        hp -= damage;
        CheckDeath();
    }

    private void CheckDeath() {
        if (hp < 0) {
            Deactivate();
        }
    }

    private void Deactivate() {
        gameObject.SetActive(false);

    }

    #endregion
}
