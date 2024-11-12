using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
     public Transform player;
    [SerializeField] private float speed = 2.0f;

    // Ho scelto di usare il metodo LateUpdate invece di Update per avere una rotazione poiché avevo bisogno di calcolare
    // la rotazione al frame successivo per non avere conflitti conl'interazione cubo-parete
   private void LateUpdate()
    {

        transform.RotateAround(player.position, Vector3.up, speed * Time.deltaTime);   
    }
}
