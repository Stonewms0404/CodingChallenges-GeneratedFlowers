using UnityEngine;
using System;
using Unity.Mathematics;
using UnityEngine.UI;

public class GPUFlower : MonoBehaviour
{
    public FlowerStruct flower;
    public GameObject Petals, Stem;
    public LineRenderer PetalsRen, StemRen;
    public ComputeShader computeShader;
    public ShaderMemoryInformationScriptableObject SO;

    float petals, totalPetals;

    Vector2 petalPos, headPos;

    void Start()
    {
        headPos = ExtraMethods.RandomVector2(7,4);
        petalPos = headPos;
        CreateFlower().transform.SetParent(transform);
        CreateStem().transform.SetParent(transform);
    }
    GameObject CreateStem()
    {
        Material mat = new(Shader.Find("Unlit/Color"));
        mat.SetColor("_Color", new(0, UnityEngine.Random.Range(0.2f, 1), 0, 1));
        StemRen = Create.Line(headPos, new Vector3(headPos.x, -8, 0), flower.petalWidth / 1.5f, mat);
        Stem = StemRen.gameObject;
        Stem.AddComponent<LayoutElement>().layoutPriority = 2;
        Stem.transform.position = new(Stem.transform.position.x, Stem.transform.position.y, -0.1f);
        return Stem;
    }

    GameObject CreateFlower()
    {
        flower = new()
        {
            maxPetals = FlowerStruct.GetPetalCount(),
            amplitude = UnityEngine.Random.Range(0.25f, 1.25f),
            petalPoints = 1000,
            petalCount = FlowerStruct.GetPetalCount(),
            pos = headPos
        };
        flower.petalWidth = flower.amplitude * 0.2f;

        petals = 0;
        totalPetals = flower.petalCount;

        Petals = new("Petals");
        Petals.AddComponent<LayoutElement>().layoutPriority = 2;
        Petals.transform.SetParent(transform);

        PetalsRen = Petals.AddComponent<LineRenderer>();
        PetalsRen.material = new(Shader.Find("Unlit/Color"));
        PetalsRen.material.SetColor("_Color", UnityEngine.Random.ColorHSV());
        PetalsRen.startWidth = flower.petalWidth;
        PetalsRen.endWidth = flower.petalWidth;
        PetalsRen.positionCount = flower.petalPoints;

        Petals.transform.position = new(Petals.transform.position.x, Petals.transform.position.y, 0);
        return Petals;
    }

    private void FixedUpdate()
    {
        MoveFlowerHead();
        LerpPetals();
    }

    void MoveFlowerHead()
    {
        if (ExtraMethods.AbsDist(StemRen.GetPosition(0).x, petalPos.x) < 0.1f)
            petalPos.x = UnityEngine.Random.Range(headPos.x - 1f, headPos.x + 1f);
        
        StemRen.SetPosition(0, Animation.Linear(StemRen.GetPosition(0), (Vector3)petalPos, 1, 0.1f));
    }

    void LerpPetals()
    {
        if (Petals == null)
            CreateFlower();
        if (ExtraMethods.AbsDist(petals, flower.petalCount) > 0.1f)
        {
            petals = Mathf.Lerp(petals, flower.petalCount, Time.deltaTime);
            Draw.SinePetal(computeShader, PetalsRen, StemRen.GetPosition(0), flower.petalPoints, flower.amplitude, petals);
        }
        else
        {
            flower.petalCount = UnityEngine.Random.Range(totalPetals - 1, totalPetals + 1);
        }
    }
}
