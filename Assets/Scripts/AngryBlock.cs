using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBlock : MonoBehaviour
{

    [SerializeField] private GameObject[] waypoints;
    private int currentWayPointIndex = 0;
    [SerializeField] private float speed = 1f;
    private float upSpeed = 0;
    private Animator anim;
    private bool hit = false;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWayPointIndex].transform.position, transform.position) < .1f)
        {
            hit = true;
            anim.SetBool("Hits", hit);
            StartCoroutine(ResetHitState());
            upSpeed = 0;
            currentWayPointIndex++;
            if (currentWayPointIndex >= waypoints.Length)
            {

                currentWayPointIndex = 0;
            }
        }
        upSpeed = upSpeed + 0.0001f;
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, Time.deltaTime * speed + upSpeed);
    }

    private IEnumerator ResetHitState()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("Hits", false);
        hit = false;
    }






}