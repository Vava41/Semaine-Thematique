using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(LineRenderer))]


public class Neux : MonoBehaviour
{
    private LineRenderer lineRenderer;

    // Propriétés publiques pour modifier la ligne dans l'inspecteur
    public Material lineMaterial;    // Matériau de la ligne
    public float lineWidth = 0.1f;   // Épaisseur de la ligne


    public float smoothTime = 0.3f; // Temps pour atteindre la position
    private Vector3 velocity = Vector3.zero;
    public List<Neux> list;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineMaterial != null)
        {
            lineRenderer.material = lineMaterial;
        }
    }

    void DrawLineBetweenPoints(Vector3 start, Vector3 end)
    {
        // Définir les positions de départ et de fin de la ligne
        lineRenderer.positionCount = 2;   // La ligne a 2 points (start et end)
        lineRenderer.SetPosition(0, start);   // Point de départ
        lineRenderer.SetPosition(1, end);     // Point d'arrivée
    }

    public GameObject GetBrick()//retourne la Brick possèder
    {
        return gameObject.AddComponent<DropZone>().transform.GetChild(0).gameObject;   
        
    }
    public void SetBrick(GameObject brick)//modifie la Brick possèder
    {
        gameObject.AddComponent<DropZone>().transform.GetChild(0).gameObject.transform.SetParent(null);

        brick.transform.SetParent(this.gameObject.AddComponent<DropZone>().gameObject.transform);
    }
    public void DestroyBrickFiliation()
    {
        gameObject.AddComponent<DropZone>().transform.GetChild(0).gameObject.transform.SetParent(null);
    }


    public void destroy()
    {
        if (gameObject.GetComponent<DropZone>().Brick == null)
        {
            Debug.Log("Brick null !");
        }
        else
        {
            Destroy(gameObject.GetComponent<DropZone>().Brick);
        }
    }

    public void MoveTargetTowardsSelf(Vector3 target)
    {
        Vector3.SmoothDamp(target, transform.position, ref velocity, smoothTime);
    }
    public void LanceMove(int i)//Prend le numéraux du neux relier et envoie la Brick
    {
        list[i].MoveTargetTowardsSelf(transform.position);
        list[i].SetBrick(this.GetBrick());
        DestroyBrickFiliation();
    }
    public void AddNeux(Neux neux)
    {
        list.Add(neux);
        DrawLineBetweenPoints(transform.position, neux.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
