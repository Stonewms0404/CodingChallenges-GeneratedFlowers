using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Extension methods for certain methods that are not available normally.
/// </summary>
public static class ExtraMethods
{
    /// <summary>
    /// Sets the alpha value of the SpriteRenderer's color to the new alpha value passed in.
    /// </summary>
    /// <param name="spriteRenderer"></param>
    /// <param name="alpha">New alpha value for the color</param>
    public static void Fade(this SpriteRenderer spriteRenderer, float alpha)
    {
        var color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
    /// <summary>
    /// Sets the alpha value of the SpriteRenderer's color to the new alpha and color value passed in.
    /// </summary>
    /// <param name="spriteRenderer"></param>
    /// <param name="alpha">New alpha value for the color</param>
    public static void Fade(this SpriteRenderer spriteRenderer, float alpha, Color color)
    {
        color.a = alpha;
        spriteRenderer.color = color;
    }
    /// <summary>
    /// Sets the alpha value of the SpriteRenderer's color to the new alpha value passed in. (Using an array)
    /// </summary>
    /// <param name="spriteRenderer"></param>
    /// <param name="alpha">New alpha value for the color</param>
    public static void Fade(this SpriteRenderer[] spriteRenderer, float alpha)
    {
        foreach (var sprite in spriteRenderer) sprite.Fade(alpha);
    }
    /// <summary>
    /// Sets the alpha value of the SpriteRenderer's color to the new alpha and color value passed in. (Using an array)
    /// </summary>
    /// <param name="spriteRenderer"></param>
    /// <param name="alpha">New alpha value for the color</param>
    public static void Fade(this SpriteRenderer[] spriteRenderer, float alpha, Color color)
    {
        foreach (var sprite in spriteRenderer) sprite.Fade(alpha, color);
    }

    /// <summary>
    /// Takes the last item of the list and removes it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Pop<T>(this List<T> list)
    {
        list.Remove(list.Last());
    }
    /// <summary>
    /// Checks if the list is null. If it is, create a new list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Check<T>(this List<T> list)
    {
        if (list == null) list = new List<T>();
    }

    /// <summary>
    /// Changes the boolean to an integer. true: 1. false: 0.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int ToInt(this bool value)
    {
        return value ? 1 : 0;
    }

    /// <summary>
    /// Lerps between the first color and the second color.
    /// </summary>
    /// <param name="c1">First Color</param>
    /// <param name="c2">Second Color</param>
    /// <param name="amount">The Lerp speed between the colors</param>
    /// <returns></returns>
    public static Color Lerp(this Color c1, Color c2, float amount)
    {
        c1.r = Mathf.Lerp(c1.r, c2.r, amount);
        c1.g = Mathf.Lerp(c1.g, c2.g, amount);
        c1.b = Mathf.Lerp(c1.b, c2.b, amount);
        return c1;
    }

    public static void Scale(this GameObject obj, float scale)
    {
        obj.transform.localScale = Vector3.one * scale;
    }
    public static void Scale(this Vector2 obj, float scale)
    {
        obj = Vector2.one * scale;
    }
    public static void Scale(this Vector3 obj, float scale)
    {
        obj = Vector3.one * scale;
    }
        
    public static float AbsDist(Vector3 obj1, Vector3 obj2)
    {
        return Mathf.Abs(Vector3.Distance(obj1, obj2));
    }
    public static float AbsDist(float a, float b)
    {
        return Mathf.Abs(a-b);
    }

    public static void CheckForIntegers(string input, out int result)
    {
        int iter = 0;
        result = -1;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] >= 57 && input[i] <= 48)
            {
                iter++;
                result = i * (int)Math.Pow(10.0, (double)iter);
            }
        }
    }
}

public static class Create
{
    public static MeshRenderer Point(Material mat)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.position = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        renderer.sharedMaterial = mat;
        return renderer;
    }
    public static MeshRenderer Point(Vector3 pos, Material mat)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.position = pos;
        obj.transform.localScale = Vector3.one;
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        renderer.sharedMaterial = mat;
        return renderer;
    }
    public static MeshRenderer Point(Vector3 pos, float radius, Material mat)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.position = pos;
        obj.transform.localScale = Vector3.one * radius;
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        renderer.sharedMaterial = mat;
        return renderer;
    }

    public static GameObject Line(Vector3 begPoint, Vector3 endPoint, float width, Material mat)
    {
        GameObject line = new("LineObject");
        LineRenderer renderer = line.AddComponent<LineRenderer>();

        renderer.material = mat;
        renderer.startWidth = width;
        renderer.endWidth = width;
        renderer.positionCount = 2;
        Vector3[] points = { begPoint, endPoint };
        renderer.SetPositions(points);

        return line;
    }
    public static GameObject Line(Vector3 begPoint, Vector3 endPoint, float startWidth, float endWidth, Material mat)
    {
        GameObject line = new GameObject();
        line.name = "LineObject";
        LineRenderer renderer = line.AddComponent<LineRenderer>();

        renderer.material = mat;
        renderer.startWidth = startWidth;
        renderer.endWidth = endWidth;
        renderer.positionCount = 2;
        Vector3[] points = { begPoint, endPoint };
        renderer.SetPositions(points);

        return line;
    }

    public static GameObject Sine(Vector2 startPos, int points, float width, Color startColor, Color endColor, float amplitude = 1.0f, float frequency = 1.0f)
    {
        GameObject line = new GameObject
        {
            name = "SineObject"
        };
        LineRenderer renderer = line.AddComponent<LineRenderer>();

        renderer.startColor = startColor;
        renderer.endColor = endColor;
        renderer.startWidth = width;
        renderer.endWidth = width;

        float xStart = 0;
        float Tau = 2 * Mathf.PI;
        float xFinish = Tau;

        renderer.positionCount = points;
        for (int i = 0; i < points; i++)
        {
            float progress = (float)i / (points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = Mathf.Sin(Tau * frequency * x) * amplitude;
            renderer.SetPosition(i, new(x + startPos.x, y + startPos.y));
        }

        return line;
    }
    public static GameObject Sine(Vector2 startPos, int points, float width, Material mat, float amplitude = 1.0f, float frequency = 1.0f)
    {
        GameObject line = new GameObject();
        line.name = "SineObject";
        LineRenderer renderer = line.AddComponent<LineRenderer>();

        renderer.material = mat;
        renderer.startWidth = width;
        renderer.endWidth = width;

        float xStart = 0;
        float Tau = 2 * Mathf.PI;
        float xFinish = Tau;

        renderer.positionCount = points;
        for (int i = 0; i < points; i++)
        {
            float progress = (float)i / (points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = Mathf.Sin(Tau * frequency * x) * amplitude;
            renderer.SetPosition(i, new(x + startPos.x, y + startPos.y));
        }

        return line;
    }

    public static GameObject SinePetal(Vector2 startPos, int points, float width, Material mat, float amplitude = 1.0f, float petalCount = 1.0f)
    {
        GameObject line = new("Petals");
        LineRenderer renderer = line.AddComponent<LineRenderer>();

        renderer.material = mat;

        renderer.startWidth = width;
        renderer.endWidth = width;

        float xStart = 0;
        float Tau = 2 * Mathf.PI;
        float xFinish = Tau;

        renderer.positionCount = points;
        for (int i = 0; i < points; i++)
        {
            float progress = (float)i / (points - 1);
            float theta = Mathf.Lerp(xStart, xFinish, progress);
            float r = Mathf.Sin(petalCount * theta) * amplitude;
            float x = r * Mathf.Cos(theta);
            float y = r * Mathf.Sin(theta);

            renderer.SetPosition(i, new(x + startPos.x, y + startPos.y));
        }

        return renderer.gameObject;
    }
    public static GameObject SinePetal(FlowerStruct flower, Material flowerMat)
    {
        GameObject line = new("Petals");
        LineRenderer renderer = line.AddComponent<LineRenderer>();

        renderer.material = flowerMat;

        renderer.startWidth = flower.petalWidth;
        renderer.endWidth = flower.petalWidth;

        float xStart = 0;
        float xFinish = 2 * Mathf.PI;

        renderer.positionCount = flower.petalPoints;
        for (int i = 0; i < flower.petalPoints; i++)
        {
            float progress = (float)i / (flower.petalPoints - 1);
            float theta = Mathf.Lerp(xStart, xFinish, progress);
            float r = Mathf.Sin(flower.petalCount * theta) * flower.amplitude;
            float x = r * Mathf.Cos(theta);
            float y = r * Mathf.Sin(theta);

            renderer.SetPosition(i, new(x + flower.pos.x, y + flower.pos.y));
        }

        return renderer.gameObject;
    }
}

public static class Draw
{
    /// <summary>
    /// Draws a petal shape using a compute shader.
    /// </summary>
    /// <param name="shaderInfo"></param>
    /// <param name="computeShader"></param>
    public static void SinePetal(ShaderMemoryInformationScriptableObject shaderInfo, ComputeShader computeShader)
    {
        FlowerStruct[] data = shaderInfo.flowers;
        Position[] points = new Position[shaderInfo.flowers[0].petalPoints];

        int pointsBytes = sizeof(float) * 2 * points.Length;

        int kernel = computeShader.FindKernel("CSMain");
        computeShader.GetKernelThreadGroupSizes(kernel, out uint threadx, out uint thready, out uint threadz);

        ComputeBuffer petalBuffer = new(data.Length, shaderInfo.bytesForShader);
        ComputeBuffer pointsBuffer = new(points.Length, pointsBytes);
        petalBuffer.SetData(data);
        pointsBuffer.SetData(points);
        computeShader.SetBuffer(kernel, "flowers", petalBuffer);
        computeShader.SetBuffer(kernel, "points", pointsBuffer);
        computeShader.SetFloat("resolution", shaderInfo.flowers.Length);
        computeShader.Dispatch(kernel, (int)threadx, (int)thready, (int)threadz);

        petalBuffer.GetData(data);
        pointsBuffer.GetData(points);

        for (int i = 0; i < data.Length; i++)
        {
            LineRenderer renderer = shaderInfo.petals[i];

            renderer.positionCount = shaderInfo.flowers[i].petalPoints;

            for (int j = 0; j < points.Length; j++)
            {
                renderer.SetPosition(i, new(points[j].x, points[j].y));
            }
        }

        petalBuffer.Dispose();
        pointsBuffer.Dispose();
    }
}

public static class Animation
{
    public static Vector2 Linear(Vector2 start, Vector2 end, float duration, float speed = 1.0f)
    {
        float animT = Mathf.Clamp01(speed * Time.deltaTime / duration);

        Vector2 newPos = Vector2.Lerp(start, end, animT);
        return newPos;
    }

    public static Vector2 CubicIn(Vector2 start, Vector2 end, float duration, float speed = 1.0f)
    {
        float animT = Mathf.Clamp01(speed * Time.deltaTime / duration);
        animT = Mathf.Pow(animT, 3);

        Vector2 newPos = Vector2.Lerp(start, end, animT);
        return newPos;
    }
    public static Vector2 CubicOut(Vector2 start, Vector2 end, float duration, float speed = 1.0f)
    {
        float animT = Mathf.Clamp01(speed * Time.deltaTime / duration);
        animT = 1 - Mathf.Pow(1 - animT, 3);

        Vector2 newPos = Vector2.Lerp(start, end, animT);
        return newPos;
    }
    public static Vector2 CubicInOut(Vector2 start, Vector2 end, float duration, float speed = 1.0f)
    {
        float animT = Mathf.Clamp01(speed * Time.deltaTime / duration);
        float a = Mathf.Round(animT);
        animT = 4 * animT * (1 - a) + (1 - 4 * Mathf.Pow( (1 - animT), 3) ) * a;

        Vector2 newPos = Vector2.Lerp(start, end, animT);
        return newPos;
    }
}

public struct Position
{
    public float x, y;
}

public struct FlowerStruct
{
    public Vector2 pos;
    public float petalWidth, petalCount,  amplitude;
    public int petalPoints;
    public static int GetPetalCount()
    {
       return UnityEngine.Random.Range(4, 11);
    }
} 