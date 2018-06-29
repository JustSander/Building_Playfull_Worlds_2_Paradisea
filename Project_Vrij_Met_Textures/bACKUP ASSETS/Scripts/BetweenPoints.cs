using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenPoints : MonoBehaviour {

    public GameObject[] points;

    private Transform target;
    private int currentPoint = 0;
    public float swimSpeed;
    public float rotSpeed;


    void Start () {
        target = points[currentPoint].transform;
	}
	
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * swimSpeed);

        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation,rotation, rotSpeed * Time.deltaTime);
        print(points.Length);
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            if (currentPoint >= points.Length -1)
                currentPoint = 0;
            else
                currentPoint++;

            target = points[currentPoint].transform;
        }
	}
}
