using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TestObject : MonoBehaviour
{
    public ShaderMemoryInformationScriptableObject SO;
    public ComputeShader shader;

    float maxPetals;
    void Start()
    {
        int flowerCount = Random.Range(8, 50);
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
                maxPetals = FlowerStruct.GetPetalCount(),
                amplitude = UnityEngine.Random.Range(0.25f, 1.25f),
                petalPoints = 1000,
                pos = position
            };
            flower.petalWidth = flower.amplitude * 0.2f;
            SO.flowers[i] = flower;

            GameObject Petals = new("Petals");
            Petals.AddComponent<LayoutElement>().layoutPriority = 2;
            Petals.transform.SetParent(transform);
            LineRenderer renderer = Petals.AddComponent<LineRenderer>();
            renderer.material = new(Shader.Find("Unlit/Color"));
            renderer.material.SetColor("_Color", Random.ColorHSV());
            renderer.startWidth = SO.flowers[i].petalWidth;
            renderer.endWidth = SO.flowers[i].petalWidth;

            SO.petals[i] = Petals.GetComponent<LineRenderer>();

        }
    }

    void Update()
    { 
        for (int i = 0; i < FlowerStruct.GetPetalCount(); i++)
        {
            SO.flowers[i].petalCount = Mathf.Lerp(SO.flowers[i].petalCount, SO.flowers[i].maxPetals, Time.deltaTime);
            Debug.Log(SO.flowers[i].petalCount);
        }
        Draw.SinePetal(SO);
    }

}
