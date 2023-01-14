using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TestTools;

public class CharacterControllerTests : InputTestFixture
{
    //[UnityTest]
    //public IEnumerator PlayerMovesUp()
    //{
    //    var originalPosition = new Vector3(0, 0);

    //    var keyboard = InputSystem.AddDevice<Keyboard>();

    //    var prefab = CreatePlayer(originalPosition);

    //    MovePlayer(keyboard.wKey);

    //    yield return new WaitForSeconds(3f);

    //    var newPosition = prefab.transform.position;

    //    Assert.Greater(newPosition.y, originalPosition.y);

    //    GameObject.Destroy(prefab);
    //}

    //[UnityTest]
    //public IEnumerator PlayerMovesLeft()
    //{
    //    var originalPosition = new Vector3(0, 0);

    //    var keyboard = InputSystem.AddDevice<Keyboard>();

    //    var prefab = CreatePlayer(originalPosition);

    //    MovePlayer(keyboard.aKey);

    //    yield return new WaitForSeconds(3f);

    //    var newPosition = prefab.transform.position;

    //    Assert.Less(newPosition.x, originalPosition.x);

    //    GameObject.Destroy(prefab);
    //}

    //[UnityTest]
    //public IEnumerator PlayerMovesRight()
    //{
    //    var originalPosition = new Vector3(0, 0);

    //    var keyboard = InputSystem.AddDevice<Keyboard>();

    //    var prefab = CreatePlayer(originalPosition);

    //    MovePlayer(keyboard.dKey);

    //    yield return new WaitForSeconds(3f);

    //    var newPosition = prefab.transform.position;

    //    Assert.Greater(newPosition.x, originalPosition.x);

    //    GameObject.Destroy(prefab);
    //}

    //[UnityTest]
    //public IEnumerator PlayerMovesDown()
    //{
    //    var originalPosition = new Vector3(0, 0);

    //    var keyboard = InputSystem.AddDevice<Keyboard>();

    //    var prefab = CreatePlayer(originalPosition);

    //    MovePlayer(keyboard.sKey);

    //    yield return new WaitForSeconds(3f);

    //    var newPosition = prefab.transform.position;

    //    Assert.Less(newPosition.y, originalPosition.y);

    //    GameObject.Destroy(prefab);
    //}

    //private void MovePlayer(KeyControl key)
    //{
    //    Set(key, 1, 2);
    //}

    //private static GameObject CreatePlayer(Vector3 originalPosition)
    //{
    //    var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/MainCharacter.prefab");



    //    prefab = GameObject.Instantiate(prefab, originalPosition, Quaternion.identity);
    //    return prefab;
    //}
}
