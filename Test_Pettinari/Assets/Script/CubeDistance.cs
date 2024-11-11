using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeDistance : MonoBehaviour
{
    
    [SerializeField] private GameObject cube;
    [SerializeField] private float meshConstant; //Costante per regolare la distanza tra il cubo e la superficie 
    public Transform originPosition;
    public float distance; //Distanza massima del Raycast
    public LayerMask parete;

  
    void FixedUpdate()
    {
        cube.transform.LookAt(originPosition); //Ruoto il cubo in modo che guardi sempre verso l'originPosition
       
        //Calcola la direzione normalizzata dal cubo verso l'origine
        Vector3 direction = (transform.position - originPosition.position).normalized;
         RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position + direction  ,originPosition.position, out hit, distance, parete))

        {   
            Vector3 nuovaPosizione = hit.point - (hit.normal * meshConstant); 
            Collider[] colliders = Physics.OverlapBox(nuovaPosizione,cube.transform.localScale / 2, cube.transform.rotation, parete);
            Debug.DrawLine(originPosition.position, nuovaPosizione, Color.red);
            if (colliders.Length == 0)
            {
                // Aggiorna la posizione del cubo con un avvicinamento graduale
                cube.transform.position = Vector3.Lerp(transform.position, nuovaPosizione, 0.5f);
            }
            else
            {
                cube.transform.position = hit.normal * meshConstant + transform.position;
            }


        }
        else

           {
             cube.transform.position = Vector3.Lerp(cube.transform.position, transform.position, 0.5f);
           }

        
        Debug.DrawLine(transform.position + direction , originPosition.position);


        
    }
}

