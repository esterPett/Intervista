using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InstatiateCube : MonoBehaviour
{
    [SerializeField] private int nCube = 10; //Numero di cubo da instanziare
    [SerializeField] private float distance= 5f; //Distanza dei cubi dal cilindro
    [SerializeField] private GameObject cube; //Collegamento al GameObject cube
    [SerializeField] private LayerMask parete;
    [SerializeField] private List<GameObject> cubi;
    [SerializeField] private Material pari;
    [SerializeField] private Material dispari;
    void Start()
    {
        float distanceCube = 360f / nCube; //Distanza tra ogni cubo

        for (int i = 0; i < nCube; i++)

        {
            float angle = i * distanceCube * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * distance;
            float z = Mathf.Sin(angle) * distance;

            Vector3 position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

            GameObject obj = Instantiate(cube, position, Quaternion.identity, transform);

            
           
            //Assegno il materiale in baee se i cubi sono pari o dispari
            if (i % 2 == 0)
            {
                obj.transform.GetChild(0).GetComponent<Renderer>().material = pari;
            }
            else
            {
                obj.transform.GetChild(0).GetComponent<Renderer>().material = dispari;
            }
            cubi.Add(obj);

            obj.GetComponent<CubeDistance>().originPosition = transform;
            obj.GetComponent<CubeDistance>().distance = distance;
            obj.GetComponent<RotateCube>().player = transform;
        }

    }

  
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position-transform.right *distance);

        
    }




}
