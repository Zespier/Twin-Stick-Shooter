using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour {

    //------------Initialization----------------

    //---------------Gameplay-------------------
    //----------------Waves---------------------
    public static Action<SpawnArea> OnEnterSpawnArea; //TODO: Waves should stop even when you don't exit the zone, but in other cases likes going to the menu, etc...
    public static Action OnExitSpawnArea;
    public static Action OnStartEnemySpawner;
    public static Action OnWaveStarted;

    public static Action<Vector2> OnTargetMove;

}
