using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFish : MonoBehaviour
{

    private float timer = 0;
    public float blownUpTime = 1;
    private Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim = GetComponent<Animator>();
            anim.SetBool("BlowUp", true);
            GetComponent<FollowPlayer>().enabled = false;
            anim.SetFloat("Speed", 1f);
        }
    }

    private void Update()
    {
        if(anim != null) { 
            if (anim.GetBool("BlowUp"))
            {
                timer += Time.deltaTime;

                if (timer >= blownUpTime)
                {
                    anim.SetBool("BlowUp", false);
                    GetComponent<FollowPlayer>().enabled = true;
                    anim.SetFloat("Speed", -1f);
                    timer = 0;
                }
            }
        }
    }
}
