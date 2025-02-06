using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(LineRenderer))]


public class Neux : MonoBehaviour
{
    //private Timer timer = -1;
    public int timer_max;
    private List<LineRenderer> lineRenderers;
    public bool test;
    private GameObject test_game;
    public int Number_sol;
    private static bool isrun = false;
    // Propri�t�s publiques pour modifier la ligne dans l'inspecteur
    public Material lineMaterial;    // Mat�riau de la ligne
    public float lineWidth = 0.1f;   // �paisseur de la ligne


    public float smoothTime = 0.3f; // Temps pour atteindre la position
    private Vector3 velocity = Vector3.zero;
    public List<Neux> liste;
    private bool drapeau_MoveTargetTowardsSelf = false;
    private Vector3 Target_MoveTargetTowardsSelf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // D�finir l'�paisseur � la fin


        foreach (Neux neux in liste)
        {
            AddNeux(neux);
        }
        if (test)
        {
            LanceMove(liste[0]);
        }
    }

    public bool asBrick()
    {
        return (gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).tag == "Brick");
    }

    /*public string addColorNeux(Neux neux_)
    {
        list
    }*/


    // Update is called once per framet
    void Update()
    {
        Gravity();
        if (drapeau_MoveTargetTowardsSelf)
        {
            test_game.transform.position = Vector3.SmoothDamp(test_game.transform.position, Target_MoveTargetTowardsSelf, ref velocity, smoothTime);
            //if (timer == 0)
            //{
            //    timer += Time.deltaTime;
            //    if (timer > timer_max)
            //    {
            //        test_game.transform.position = Target_MoveTargetTowardsSelf;
            //        timer = -1;
            //        isrun = false;
            //    }
            //}
        }
        Check();
    }
    public string addColor()
    {
        return gameObject.AddComponent<DropZone>().couleur;
    }
    void DrawLineBetweenPoints(Vector3 start, Vector3 end)
    {
        GameObject lineObj = new GameObject();
        LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
        if (lineMaterial != null)
        {
            lineRenderer.material = lineMaterial;
        }
        lineRenderer.startWidth = lineWidth;  // D�finir l'�paisseur au d�but
        lineRenderer.endWidth = lineWidth;
        // D�finir les positions de d�part et de fin de la ligne
        lineRenderer.positionCount = 2;   // La ligne a 2 points (start et end)
        lineRenderer.SetPosition(0, start);   // Point de d�part
        lineRenderer.SetPosition(1, end);     // Point d'arriv�e
    }

    public GameObject GetBrick()//retourne la Brick poss�der
    {
        if (gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).tag == "Brick")
        {
            return gameObject.transform.GetChild(0).gameObject;
        }
        return null;
    }
    public void SetBrick(GameObject brick)//prend la Brick et la mais � la place de la Brick pr�c�dente, sans d�truir la Brick pr�ss�dente
    {
        if (gameObject.transform.childCount > 0 && gameObject.transform.GetChild(0).tag == "Brick")
        {
            gameObject.transform.GetChild(0).gameObject.transform.SetParent(null);
            brick.transform.SetParent(gameObject.transform);
            brick.transform.SetAsFirstSibling(); // Place l'enfant en premi�re position
        }
        else
        {
            brick.transform.SetParent(gameObject.transform);
            brick.transform.SetAsFirstSibling(); // Place l'enfant en premi�re position

        }
    }
    public void DestroyBrickFiliation()//Enl�ve la Brick de la filiation
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
        Target_MoveTargetTowardsSelf = target;
        test_game = GetBrick();
    }
    public void LanceMove(Neux neu)//Prend le num�raux du neux relier et envoie la Brick vers vous
    {
        if (!isrun)
        {
            //ytimer = 0;
            isrun = true;
            neu.MoveTargetTowardsSelf(transform.position);
            this.SetBrick(neu.GetBrick());
            neu.DestroyBrickFiliation();
        }
    }
    public void AddNeux(Neux neux)
    {
        DrawLineBetweenPoints(transform.position, neux.transform.position);
    }
    public void Gravity()//A chaque moments
    {
        if (!asBrick())
        {
            foreach (var item in liste)
            {
                if (item.Number_sol > Number_sol)
                {
                    if (item.asBrick())
                    {
                        LanceMove(item);
                    }
                }
            }
        }
    }

    void Check()
    {
        for (int i = 0; i < liste.Capacity; i++)
        {
            if (liste[i].addColor() == "rouge" && addColor() == "rouge")
            {
                liste[i].destroy();
                destroy();
            }
        }
    }
}
