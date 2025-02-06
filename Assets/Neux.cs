using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(LineRenderer))]


public class Neux : MonoBehaviour
{
    public bool test;

    private LineRenderer lineRenderer;

    // Propriétés publiques pour modifier la ligne dans l'inspecteur
    public Material lineMaterial;    // Matériau de la ligne
    public float lineWidth = 0.1f;   // Épaisseur de la ligne


    public float smoothTime = 0.3f; // Temps pour atteindre la position
    private Vector3 velocity = Vector3.zero;
    public List<Neux> list;
    private bool drapeau_MoveTargetTowardsSelf=false;
    private Vector3 Vector3_MoveTargetTowardsSelf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;  // Définir l'épaisseur au début
        lineRenderer.endWidth = lineWidth;    // Définir l'épaisseur à la fin

        if (lineMaterial != null)
        {
            lineRenderer.material = lineMaterial;
        }
        foreach (Neux neux in list)
        {
            AddNeux(neux);
        }
        if (test)
        {
            LanceMove(0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (drapeau_MoveTargetTowardsSelf)
        {
            Vector3.SmoothDamp(Vector3_MoveTargetTowardsSelf, transform.position, ref velocity, smoothTime);
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
        if (gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).tag == "Brick")
        {
            return gameObject.transform.GetChild(0).gameObject;
        }
        return null;
    }
    public void SetBrick(GameObject brick)//prend la Brick et la mais à la place de la Brick précédente, sans détruir la Brick préssédente
    {
        if (gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).tag == "Brick")
        {
            gameObject.transform.GetChild(0).gameObject.transform.SetParent(null);
            brick.transform.SetParent(gameObject.transform);
        }
        brick.transform.SetParent(gameObject.transform);
    }
    public void DestroyBrickFiliation()//Enlève la Brick de la filiation
    {
        if (gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).tag == "Brick")
        {
            gameObject.transform.GetChild(0).gameObject.transform.SetParent(null);
        }
    }


    public void destroy()
    {
        if (gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).tag == "Brick")
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        else
        {
            Debug.Log("Brick null ou pas une brick !");
        }
    }

    public void MoveTargetTowardsSelf(Vector3 target)
    {
        drapeau_MoveTargetTowardsSelf = true;
        Vector3_MoveTargetTowardsSelf = target;
    }
    public void LanceMove(int i)//Prend le numéraux du neux relier et envoie la Brick vers vous
    {
        list[i].MoveTargetTowardsSelf(transform.position);
        this.SetBrick(list[i].GetBrick());
        list[i].DestroyBrickFiliation();

    }
    public void AddNeux(Neux neux)
    {
        DrawLineBetweenPoints(transform.position, neux.transform.position);
    }

}
