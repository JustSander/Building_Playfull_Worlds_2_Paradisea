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
        if (other.gameObject.tag == "vis")
        {
            anim = other.GetComponent<Animator>();
            anim.SetBool("BlowUp", true);
            GameObject.FindGameObjectWithTag("vis").GetComponent<FollowPlayer>().enabled = false;
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
                    GameObject.FindGameObjectWithTag("vis").GetComponent<FollowPlayer>().enabled = true;
                    anim.SetFloat("Speed", -1f);
                    timer = 0;
                }
            }
        }
    }
}
