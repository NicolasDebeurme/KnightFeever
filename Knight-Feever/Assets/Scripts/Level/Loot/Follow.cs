using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [HideInInspector]public GameObject target;
    Vector3 velocity = Vector3.zero;

    public float minSpeed=7;
    public float maxSpeed=11; 

    bool _isFollowing = false;
    public void StartFollowing()
    {
        _isFollowing = true;
    }
    void Update()
    {
        if(_isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref velocity, Time.deltaTime*Random.Range(minSpeed,maxSpeed));
        }
    }
}
