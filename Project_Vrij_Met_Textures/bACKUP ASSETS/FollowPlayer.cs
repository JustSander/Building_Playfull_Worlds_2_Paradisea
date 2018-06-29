using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class FollowPlayer : MonoBehaviour
{
    public float TargetDistance;
    public float MoveSpeed;
    public float damping;
    public Transform target;
    private float CloseRange;
    private Vector3 PreviousPosition;
    private Vector3 PreviousRotation;
    private bool minionHasArrived;
    private bool allowMinionMovement = true;


    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        PreviousPosition = target.position;
        PreviousRotation = new Vector3(target.rotation.x, target.rotation.y, target.rotation.z);
        CloseRange = target.position.x + 5;
        TargetDistance = Vector3.Distance(target.position, transform.position);

        Vector3.Lerp(transform.position, target.position, MoveSpeed * (Time.deltaTime * 0.6f));

        if (TargetDistance > 1)
        {
            minionHasArrived = false;
            LookAtPlayer();
            if (allowMinionMovement)
            {
                transform.Translate(Vector3.forward * MoveSpeed * (Time.deltaTime * 0.6f));

                if (PreviousRotation != new Vector3(target.rotation.x, target.rotation.y, target.rotation.z) && allowMinionMovement)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, MoveSpeed * (Time.deltaTime * 0.6f));
                }
            }
        }
        if (TargetDistance < 1)
        {
            minionHasArrived = true;
            StartCoroutine("Timer");
        }

        /*TargetDistance = Vector3.Distance(target.position, transform.position);

        if (TargetDistance > 2)
        {

            allowMinionMovement = true;
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
            transform.LookAt(target);
        }
        else if (TargetDistance < 3)
        {
            StartCoroutine(Timer());
        }*/
    }

        private IEnumerator Timer()
    {
        yield return new WaitForSeconds(300f);
        allowMinionMovement = false;
    }

    void LookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime + damping);
    }

    // ----- PUT IN UPDATE -----
    
    /*
    //VERSION 1
    if (allowMinionMovement)
    {

        if (TargetDistance > 5)
        {
            transform.DOMove(target.position, 5f);
        }

        else if (TargetDistance > 1)
        {
            transform.DOMove(target.position, 0.5f);
        }
        else
        {
            StartCoroutine(Timer());
        }

    }
    if (TargetDistance > 1 && !allowMinionMovement)
    {
        allowMinionMovement = true;
    }

    // VERSION 2
    PreviousPosition = target.position;
    PreviousRotation = new Vector3(target.rotation.x, target.rotation.y, target.rotation.z);
    CloseRange = target.position.x + 5;

    Vector3.Lerp(transform.position, target.position, MoveSpeed* (Time.deltaTime* 0.6f));

    if (TargetDistance > 1)
    {
        allowMinionMovement = true;
        minionHasArrived = false;
        LookAtPlayer();
        if (allowMinionMovement)
        {
            transform.Translate(Vector3.forward* MoveSpeed * (Time.deltaTime* 0.6f));

            if (PreviousRotation != new Vector3(target.rotation.x, target.rotation.y, target.rotation.z) && allowMinionMovement)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, MoveSpeed* (Time.deltaTime* 0.6f));
            }
        }
    }
    if (TargetDistance< 3)
    {
        minionHasArrived = true;
        notMovingTimer = 0;
    }

    if (minionHasArrived)
    {
        allowMinionMovement = false;

        StartCoroutine(Timer());
    }
    */

}