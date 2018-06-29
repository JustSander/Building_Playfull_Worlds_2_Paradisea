using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishMovement : MonoBehaviour
{
    private FollowPlayer followPlayer;

    private void Start()
    {
        StartCoroutine(StartShake());
    }

    private IEnumerator StartShake()
    {
        transform.DOShakePosition(2f, 0.2f, 0, 10);
        yield return new WaitForSeconds(2f);
        StartCoroutine(StartShake());
    }
}
