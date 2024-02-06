using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float initialMoney = 1000f;
    public float moneyPerSecond = 100f;
    public float timeBetweenWaves = 3f;
    public List<Wave> waves = new List<Wave>();

    private int _currentWave;
    private float _currentMoney;
    private float _waveTimer;
    private List<Coroutine> c_ActiveWaves = new List<Coroutine>();
    private List<Wave> _wavesAskingForMoney = new List<Wave>();

    private void Update() {
        _waveTimer += Time.deltaTime;

        GenerateMoney();
        ShareMoneyBetweenWaves();

        CheckForSpawnableWaves();
    }

    private void OnEnable() {
        Events.OnStartWave += InitializeMoney;
    }

    private void OnDisable() {
        Events.OnStartWave -= InitializeMoney;
    }

    private void InitializeMoney() {
        _currentMoney = initialMoney;
    }

    private void GenerateMoney() {
        _currentMoney += MoneyPerSecondThisWave() * Time.deltaTime;
    }

    private void ShareMoneyBetweenWaves() {
        if (_wavesAskingForMoney.Count > 0) {
            float moneyPerWave = _currentMoney / _wavesAskingForMoney.Count;
            foreach (Wave wave in _wavesAskingForMoney) {

                wave.money += moneyPerWave;
                _currentMoney -= moneyPerWave;
            }
        }
    }

    private float MoneyPerSecondThisWave() {
        return Mathf.Lerp(moneyPerSecond, moneyPerSecond * 2, (float)_currentWave / (waves.Count - 1));
    }

    /// <summary>
    /// Checks if the next wave can be spawned
    /// </summary>
    private void CheckForSpawnableWaves() {

        if (_waveTimer >= timeBetweenWaves) {
            switch (waves[_currentWave].startType) {
                case StartType.additive:
                    c_ActiveWaves.Add(StartCoroutine(C_SpawnWave(waves[_currentWave])));
                    break;
                case StartType.exclusive:
                    if (c_ActiveWaves != null && c_ActiveWaves.Count <= 0) {
                        c_ActiveWaves.Add(StartCoroutine(C_SpawnWave(waves[_currentWave])));
                    }
                    //TODO: Check if there is no more enemies from previous waves => Checking if there is any wave spawning on the c_ActiveWaves
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Spawns the specified wave
    /// The spawn method depends on the WaveType
    /// </summary>
    /// <param name="wave"></param>
    /// <returns></returns>
    private IEnumerator C_SpawnWave(Wave wave) {

        switch (wave.waveType) {
            case WaveType.continuos:

                while (wave.amount > 0) {
                    WaveAsksForMoney(wave);
                    WaveConsumesMoney(wave);
                }

                break;
            case WaveType.instantaneous:
                SpawnWaveInstantaneous(wave);
                yield break;
            default:
                SpawnWaveInstantaneous(wave);
                yield break;
        }

        yield return null;
    }

    /// <summary>
    /// Instantaneously spawns every enemy on the wave
    /// Depending on the startType, it will wait for enough money or debt the wave
    /// </summary>
    /// <param name="wave"></param>
    private void SpawnWaveInstantaneous(Wave wave) {
        for (int i = 0; i < wave.amount; i++) {
            //TODO: SPAWN ALL ENEMIES TROUGH EVERY SPAWNER => POSITIONS WILL COME AFTER THAT
        }
    }

    private void WaveAsksForMoney(Wave wave) {
        _wavesAskingForMoney.Add(wave);

    }

    private void WaveConsumesMoney(Wave wave) {

    }
}

[System.Serializable]
public class Wave {
    public WaveType waveType;
    public StartType startType;
    public EnemyType enemyType;
    public float amount;
    public float money;
}

public enum WaveType {
    continuos,
    instantaneous
}

public enum StartType {
    additive,
    exclusive
}

public enum EnemyType {
    normal,
    fast,
    heavy,
    miniboss,
    boss
}