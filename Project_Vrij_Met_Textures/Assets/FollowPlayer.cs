using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using DG.Tweening;

public class FollowPlayer : MonoBehaviour
{
    public float TargetDistance;
    private float MoveSpeed;
    public float MaxSpeed, MinSpeed;
    public float damping;
    public Transform target;
    private float CloseRange;
    private Vector3 PreviousPosition;
    private Vector3 PreviousRotation;
    private bool minionHasArrived;
    private bool allowMinionMovement = true;
    public float approachDistance = 0.05f;
    public float rotationSpeed = 180f;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    public AudioSource AudioUp;
    public AudioSource AudioDown;

    private Animator anim;
    public float blownUpTime = 2f;
    public float amp = 0.05f;
    void Start(){
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        PreviousPosition = target.position;
        PreviousRotation = new Vector3(target.rotation.x, target.rotation.y, target.rotation.z);
        CloseRange = target.position.x + 5;
        TargetDistance = Vector3.Distance(target.position, transform.position);
        float sin = amp * Mathf.Sin(Time.time); 
        transform.position += sin * transform.up;

        if(Input.GetKeyDown(KeyCode.Space) &&allowMinionMovement){
            Debug.Log("Poof"); 
            StartCoroutine(Timer());
        }

        if(!allowMinionMovement) return;
        //Vector3.Lerp(transform.position, target.position, MoveSpeed * (Time.deltaTime * 0.6f));
        //transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
        if(TargetDistance > approachDistance){
          MoveSpeed = Mathf.Lerp(MoveSpeed, MaxSpeed, Time.deltaTime);
          rotationSpeed = 180f;
        }else{
            MoveSpeed = Mathf.Lerp(MoveSpeed, MinSpeed, Time.deltaTime);
            rotationSpeed = 90f;
        }


        transform.position += transform.forward* MoveSpeed *Time.deltaTime;
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed *Time .deltaTime );
        // Quaternion.Lerp(transform.rotation, target.rotation, MoveSpeed * (Time.deltaTime * 0.6f));
        // if (TargetDistance > 1)
        // {
        //     minionHasArrived = false;
        //     LookAtPlayer();
        //     if (allowMinionMovement)
        //     {
                
        //         //transform.Translate(Vector3.forward * MoveSpeed * (Time.deltaTime * 0.6f));

        //         if (PreviousRotation != new Vector3(target.rotation.x, target.rotation.y, target.rotation.z) && allowMinionMovement)
        //         {
        //             transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, MoveSpeed * (Time.deltaTime * 0.6f));
        //         }
        //     }
        // }
        // if (TargetDistance < 1)
        // {
        //     minionHasArrived = true;
        //     StartCoroutine("Timer");
        // }

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

        anim.SetBool("BlowUp", true);
        AudioUp.Play();
        anim.SetFloat("Speed", 3f);
        allowMinionMovement = false;
        yield return new WaitForSeconds(blownUpTime);
        
        anim.SetBool("BlowUp", false);
        AudioDown.Play();
        anim.SetFloat("Speed", -1f);
        yield return new WaitForSeconds(1f);
        allowMinionMovement = true;
    }

    void LookAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime + damping);
    }

    void OnTriggerEnter(Collider col){
        if(col.tag == "Player" && allowMinionMovement){
            StartCoroutine(Timer());
        }

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