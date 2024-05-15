using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TestObject : MonoBehaviour
{
    public ShaderMemoryInformationScriptableObject SO;
    public ComputeShader shader;

    void Start()
    {
        int flowerCount = 8;
        SO.numOfFlowers = flowerCount;
        SO.computeShader = shader;

        SO.petals = new LineRenderer[flowerCount];
        SO.flowers = new FlowerStruct[flowerCount];
        SO.stems = new LineRenderer[flowerCount];

        for (int i = 0; i < flowerCount; i++)
        {
            Vector2 position = new(UnityEngine.Random.Range(-6.0f, 6), UnityEngine.Random.Range(-4.0f, 4));
            FlowerStruct flower = new()
            {
                petalCount = FlowerStruct.GetPetalCount(),
                amplitude = UnityEngine.Random.Range(0.25f, 1.25f),
                petalPoints = 1000,
                pos = position
            };
            flower.petalWidth = flower.amplitude * 0.2f;

            GameObject Petals = new("Petals");
            Petals.AddComponent<LayoutElement>().layoutPriority = 2;
            Petals.transform.SetParent(transform);
            Petals.AddComponent<LineRenderer>();

            SO.petals[i] = Petals.GetComponent<LineRenderer>();
            SO.petals[i].material = new(Shader.Find("Unlit/Color"));
            SO.petals[i].material.SetColor("_Color", Random.ColorHSV());

            SO.flowers[i] = flower;
        }
    }

    void Update()
    { 
        Draw.SinePetal(SO);
    }

}
