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
    private GameObject[] cubes;


    private void Update()
    {
        MaintainPositionBetweenWallAndCylinder();
    }

    /*void FixedUpdate()
    {
        cube.transform.LookAt(originPosition); //Ruoto il cubo in modo che guardi sempre verso l'originPosition
       
        //Calcola la direzione normalizzata dal cubo verso l'origine
        Vector3 direction = (transform.position - originPosition.position).normalized;
         RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position,originPosition.position, out hit, distance, parete))

        {
           
            Vector3 nuovaPosizione = hit.point;//- (hit.normal * meshConstant); 
            //Collider[] colliders = Physics.OverlapBox(nuovaPosizione,cube.transform.localScale / 2, cube.transform.rotation, parete);
            
            Debug.DrawLine(transform.position, originPosition.position, Color.red);
             if (colliders.Length == 0)
             {
                 // Aggiorna la posizione del cubo con un avvicinamento graduale
                 cube.transform.position = Vector3.Lerp(originPosition.position, nuovaPosizione, 0.9f);
             }
             else
             {
                 cube.transform.position = Vector3.Lerp(nuovaPosizione, transform.position, 0.2f);
             }
           if(Vector3.Distance(originPosition.position, hit.point)<distance)
            {
                cube.transform.position = Vector3.Lerp(originPosition.position, nuovaPosizione, 0.9f);
            }
          
        }
        else

           {
             cube.transform.position = Vector3.Lerp(cube.transform.position, transform.position, 0.9f);
           }

        
        Debug.DrawLine(transform.position + direction , originPosition.position);


        
    }

    void PositionCube()
    {
        foreach (GameObject cube in cubes)
        {

        }
    }*/
    void MoveCubeTowardsWall()
    {
        // Trova tutti i collider nel raggio della distanza massima
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, distance, parete);

        float closestDistance = Mathf.Infinity;
        Collider closestWall = null;

        // Trova il muro più vicino
        foreach (var hitCollider in hitColliders)
        {
            float currentDistance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closestWall = hitCollider;
            }
        }

        // Se troviamo un muro entro la distanza massima, sposta il cubo verso il muro
        if (closestWall != null && closestDistance < distance)
        {
            PositionCubeOnWall(closestWall);
        }
        else
        {
            cube.transform.position = transform.position;
        }
    }

    void PositionCubeOnWall(Collider wall)
    {
        // Trova la normale della superficie del muro
        Vector3 wallNormal = wall.transform.forward;
        // Calcola la direzione tra il cilindro e il muro
        Vector3 directionToWallFromCylinder = wall.transform.position - originPosition.position;

        // Calcola la distanza tra il cilindro e il muro usando Vector3.Distance
        float distanceToWallFromCylinder = Vector3.Distance(originPosition.position, wall.transform.position);

        // Se la distanza tra il cilindro e il muro è troppo piccola, allontanalo
        if (distanceToWallFromCylinder < distance)
        {
            // Proietta la posizione del cubo sulla superficie del muro
            Vector3 directionToWall = transform.position - wall.transform.position;
            Vector3 projectionOnWall = directionToWall - Vector3.Dot(directionToWall, wallNormal) * wallNormal;

            // Calcola la posizione corretta lungo la superficie del muro
            Vector3 newPosition = wall.transform.position + projectionOnWall.normalized * distance;

            // Imposta la nuova posizione del cubo
            cube.transform.position = newPosition;
        }

    }

    void MoveCubeAlongWall()
    {
        // Lancia un Raycast dal cubo verso il muro
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (originPosition.position - transform.position).normalized, out hit, distance, parete))
        {
            // Se il Raycast colpisce il muro, calcola la posizione lungo il muro
            PositionCubeOnWall(hit);
        }
        else
        {
            // Se non colpisce un muro, riporta il cubo alla posizione iniziale
            cube.transform.position = transform.position;
        }
    }

    void PositionCubeOnWall(RaycastHit hit)
    {
        // Trova il punto di contatto e la normale della superficie del muro
        Vector3 contactPoint = hit.point;
        Vector3 wallNormal = hit.normal;

        // Calcola la direzione di movimento lungo la superficie del muro
        Vector3 directionAlongWall = Vector3.Cross(wallNormal, Vector3.up).normalized;

        // Calcola la nuova posizione mantenendo la distanza minima dal muro
        Vector3 targetPosition = contactPoint + wallNormal * meshConstant;

        // Sposta il cubo verso la posizione target mantenendo una distanza dal muro
        cube.transform.position = Vector3.Lerp(transform.position, targetPosition,  0.9f);

        Debug.Log("Ciao");

        
    }


    void MaintainPositionBetweenWallAndCylinder()
    {
        // Lancia un Raycast dal cubo verso l’esterno (direzione opposta al cilindro) per rilevare il muro
        RaycastHit hit;
        Vector3 directionFromCylinder = (transform.position - originPosition.position).normalized;

        if (Physics.Raycast(transform.position, directionFromCylinder, out hit, distance, parete))
        {
            // Se il Raycast colpisce un muro e il cubo è troppo vicino, lo allontana
            Vector3 wallNormal = hit.normal;  // Normale del muro
            Vector3 contactPoint = hit.point; // Punto di contatto con il muro

            // Posiziona il cubo sulla linea tra il cilindro e il muro, a distanza minima dal muro
            Vector3 targetPosition = contactPoint + wallNormal *distance;

            // Verifica che il targetPosition sia tra il cilindro e il muro
            float distanceToCylinder = Vector3.Distance(targetPosition, originPosition.position);
            float distanceToWall = Vector3.Distance(targetPosition, contactPoint);

            // Se la posizione target mantiene il cubo tra il cilindro e il muro, aggiorna la posizione
            if (distanceToWall < distanceToCylinder)
            {
                cube.transform.position = transform.position;
            }
            else
            {
                // Se targetPosition non è tra il cilindro e il muro, avvicina il cubo al cilindro
                transform.position = Vector3.Lerp(contactPoint, originPosition.position, 0.1f);
            }
        }
    }
}

