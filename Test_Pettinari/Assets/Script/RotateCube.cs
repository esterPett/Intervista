using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
     public Transform player;
    [SerializeField] private float speed = 2.0f;
    void Update()
    {

        transform.RotateAround(player.position, Vector3.up, speed * Time.deltaTime);   
    }
}
