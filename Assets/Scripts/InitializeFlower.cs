using UnityEngine;
using System;
using Unity.Mathematics;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class InitializeFlower : MonoBehaviour
{
    public FlowerStruct flower;
    public GameObject Petals, Stem;

    float petals, petalCount;
    Material flowerMat, stemMat;

    Vector2 petalPos, petalFinalPos, stemPos, stemFinalPos = new(0, -8f);

    public void Start()
    {
        Vector2 position = new(UnityEngine.Random.Range(-6.0f, 6), UnityEngine.Random.Range(-4.0f, 4));
        flowerMat = new Material(Shader.Find("Unlit/Color"));
        stemMat = new Material(Shader.Find("Unlit/Color"));

        flower = new FlowerStruct
        {
            petalCount = FlowerStruct.GetPetalCount(),
            amplitude = UnityEngine.Random.Range(0.25f, 1.25f),
            petalPoints = 1000,
            pos = position
        };
        flower.petalWidth = flower.amplitude * 0.2f;

        petalCount = flower.petalCount;

        stemPos = flower.pos;
        petalPos = flower.pos;
        stemFinalPos.x = stemPos.x;

        Color petalColor;
        do
        {
            petalColor = UnityEngine.Random.ColorHSV();
            if (petalColor.r < 0.2f && petalColor.g < 0.2f && petalColor.b < 0.2f)
                continue;
            else
                break;
        } while (true);

        flowerMat.SetColor("_Color", petalColor);

        stemMat.SetColor("_Color", new(0, UnityEngine.Random.Range(0.2f, 1f), 0, 1));


        Petals = new("Petals");
        Petals.AddComponent<LayoutElement>().layoutPriority = 2;
        Petals.transform.SetParent(transform);

    }

    private void FixedUpdate()
    {
        LerpPetals();
        LerpStem();
        /*if (Petals != null)
            MovePetals();*/
    }

    void LerpStem()
    {
        if (stemPos != petalPos || Stem == null)
        {
            if (Stem != null)
                Destroy(Stem);

            stemPos = Animation.CubicInOut(stemPos, stemFinalPos, Time.deltaTime);
            Stem = Create.Line(petalPos, stemPos, flower.petalWidth / 1.5f, stemMat);

            Stem.AddComponent<LayoutElement>().layoutPriority = 1;

            Stem.name = "Stem";
            Stem.transform.SetParent(transform);
        }
    }

    void LerpPetals()
    {
        if (petals < flower.petalCount - 0.01f || (Vector2)Petals.transform.position != petalPos)
        {
            if (Petals != null) Destroy(Petals);

            petals = Mathf.Lerp(petals, flower.petalCount, Time.deltaTime);
            Petals = Create.SinePetal(flower.pos, flower.petalPoints, flower.petalWidth, flowerMat, flower.amplitude, petals);
            Petals.transform.SetParent(transform);
        }
    }

    void MovePetals()
    {
        if ((Vector2)Petals.transform.position != petalPos)
        {
            petalPos = Animation.CubicInOut(Petals.transform.position, petalPos +
                petalFinalPos,
                UnityEngine.Random.Range(1, 5f));
            Petals.transform.position = petalPos;
        }
        else
        {
            petalPos = new Vector2(UnityEngine.Random.Range(-1, 1f), petalPos.y);
        }
        
    }
}