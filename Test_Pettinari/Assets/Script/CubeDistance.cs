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
    private Vector3 wantedPosition; //Vettore per trovare il punto sulla parete
    private Vector3 direction; //Direzione calcolata verso cui puntare il raycast
    
    //Utilizzo Update per calcolare la direzione del cubo e utilizzo LateUpdate per calcolare la posizione del Raycast per il frame successivo,
    //questo viene fatto poiché nei vari test fatti mi sono resa conto che non riuscivo a calcolare il Raycast nell'Update
    //ma avevo bisogno di salvarmi i punti trovati per il frame successivo così da riuscire nell'interazione fra cubo e parete
    private void Update()
    {
        cube.transform.LookAt(originPosition); //Oriento il cubo verso la posizione di origine
        direction = (originPosition.position - transform.position).normalized; //Calcolo e normalizzo la direzione dal cubo verso l'origine
    }
     
    private void LateUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(originPosition.position, direction, out hit, distance, parete))
        {
            //Se il Raycast colpisce una parete, colcolo la posizione aggiungendo una distanza fissa dalla superficie
            wantedPosition = hit.point + hit.normal * meshConstant;

        }
        else
        {
            //Se il Raycast non colpisce la  parete, imposta la posizione desiderata a distanza massima
            wantedPosition = originPosition.position + direction * distance;
        }

        //Sposto il cubo verso la posizione desiderata
        cube.transform.position = Vector3.Lerp(transform.position, wantedPosition, 1);

    }

}
    