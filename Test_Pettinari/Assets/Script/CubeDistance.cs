using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeDistance : MonoBehaviour
{
    public  Transform originPosition;
    [SerializeField] private GameObject cube;
    public float distance;
    public LayerMask parete;
    void Update()
    {
        cube.transform.LookAt(originPosition);
        Vector3 direction = (transform.position - originPosition.position).normalized;
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(originPosition.position, transform.position, out hit, distance , parete))
        {
            cube.transform.position = hit.point - direction;
           
        }
        else
        {
            cube.transform.position = transform.position;
        }

        Debug.DrawLine(originPosition.position, transform.position);
    }
}
