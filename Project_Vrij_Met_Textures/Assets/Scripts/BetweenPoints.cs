using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenPoints : MonoBehaviour

{
    public List<GameObject> points;
    public List<Vector3> targets;
    public Vector3 target;

    public int currentPoint = 0;
    public float swimSpeed;
    public float rotSpeed;
    public bool rotate = true;
    public bool kwal = false;
    public float speedMultiplier;

    void Start()
    {
        target = transform.TransformPoint(points[currentPoint].transform.localPosition);

        if(kwal)
            swimSpeed = Random.Range(swimSpeed, swimSpeed * speedMultiplier);

        foreach (GameObject t in points)
        {
            targets.Add(transform.TransformPoint(t.transform.localPosition));
        }
    }



    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * swimSpeed);
        if (rotate)
        {
            Vector3 direction = target - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSpeed * Time.deltaTime);
        }
  

        if (Vector3.Distance(transform.position, target) <= 0.2f)
        {
            if (!kwal) { 
                if (currentPoint >= points.Count - 1)
                    currentPoint = 0;
                else
                    currentPoint++;
            }
            else
            {
                currentPoint = 1;
            }

            target = targets[currentPoint];
        }
    }

}