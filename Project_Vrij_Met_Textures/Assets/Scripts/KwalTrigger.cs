using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KwalTrigger : MonoBehaviour
{
    public GameObject[] allJellyFish;
    public bool debugMode = false;

    private void Update()
    {
        if (debugMode)
        {
            debugMode = false;
            foreach (GameObject t in allJellyFish)
            {
                t.GetComponent<BetweenPoints>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject t in allJellyFish)
            {
                t.GetComponent<BetweenPoints>().enabled = true;
            }
        }
    }
}
