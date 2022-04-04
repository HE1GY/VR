using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSide : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;


    private void FixedUpdate()
    {
        transform.position = _target.position + Vector3.up * _offset.y +
                             Vector3.ProjectOnPlane(_target.forward, Vector3.up).normalized * _offset.z +
                             Vector3.ProjectOnPlane(_target.right, Vector3.up).normalized * _offset.x;

        transform.eulerAngles = new Vector3(0, _target.eulerAngles.y, 0);
    }
}
