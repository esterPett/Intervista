using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 offest;
    void Start()
    {
        this.offest = this.transform.position - this.player.transform.position;
    }

   
    void Update()
    {
        this.transform.position = this.player.transform.position + this.offest;
    }
}
