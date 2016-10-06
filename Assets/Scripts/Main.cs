using UnityEngine;

using System;
using System.Collections;

using Base;

using Pathfinding;

public class Main : MonoBehaviour
{
    private DungeonGame game;

    void Awake()
    {
        game = DungeonGame.Instance;
        InitManagers();
    }

    void Start()
    {
        StartCoroutine(InitCoroutine());
    }

    void Update()
    {
    }

    private void InitManagers()
    {
        game.InputManager = GetComponentInChildren<InputManager>();
        game.Start();
    }

    private IEnumerator InitCoroutine()
    {
        GameObject mapPrefab = Resources.Load<GameObject>("Map/MapTest");
        GameObject.Instantiate(mapPrefab);

        yield return new WaitForEndOfFrame();

        AstarPath.active.Scan();

        yield return new WaitForEndOfFrame();

        GameObject characterPrefab = Resources.Load<GameObject>("Map/CharacterTest");
        GameObject.Instantiate(characterPrefab);
    }


}

