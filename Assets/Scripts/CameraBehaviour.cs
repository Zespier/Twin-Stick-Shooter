using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public Camera mainCamera;
    public Transform player;

    public float movementDivision = 0.2f;
    public float distanceDivision = 0.5f;
    public float targetDivision = 0.2f;
    public float targetMultiplier = 1f;
    public float shakeDuration = 0.2f;
    public float shakeAmplitude = 0.1f;
    [Range(5, 20)] public float distance = 7f;

    private float _currentDistance;
    private Coroutine _cameraShakeAnimation;
    private Vector3 _nextTarget;
    private Vector3 _currentTarget;

    public static CameraBehaviour instance;
    private void Awake() {
        if (!instance) {
            instance = this;
        }
        _currentDistance = distance;
    }

    private void OnEnable() {
        Events.OnTargetMove += MoveTarget;
    }

    private void OnDisable() {
        Events.OnTargetMove -= MoveTarget;
    }

    private void LateUpdate() {
        DistanceMovement();
        TargetMovement();
        CameraMovement(player.position, _currentTarget);
    }

    private void CameraMovement(Vector3 playerPosition, Vector3 target) {
        transform.position = Vector3.Lerp(transform.position, playerPosition + target, Time.deltaTime / movementDivision);
    }

    private void DistanceMovement() {
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, GetDistance(), Time.deltaTime / distanceDivision);
    }

    private void TargetMovement() {
        _currentTarget = Vector2.Lerp(_currentTarget, _nextTarget, Time.deltaTime / targetDivision);
    }

    private float GetDistance() {
        return distance;
    }

    private void MoveTarget(Vector2 position) {
        _nextTarget = position * targetMultiplier;
    }

    public void CameraShake() {
        if (_cameraShakeAnimation != null) {
            StopCoroutine(_cameraShakeAnimation);
        }

        _cameraShakeAnimation = StartCoroutine(CameraShakeAnimation());
    }
    private IEnumerator CameraShakeAnimation() {

        float timer = shakeDuration;
        while (timer >= 0) {

            mainCamera.transform.localPosition = new Vector3(UnityEngine.Random.Range(-shakeAmplitude, shakeAmplitude), UnityEngine.Random.Range(-shakeAmplitude, shakeAmplitude), UnityEngine.Random.Range(-shakeAmplitude, shakeAmplitude));

            timer -= Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.localPosition = Vector3.zero;
    }

}