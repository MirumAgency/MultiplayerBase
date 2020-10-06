using UnityEngine;

public class ColorGenerator
{
    public Color ReturnRamdonGrayColor ()
    {
        float color = (Random.Range(0f, 255f) * 0.58f) + (Random.Range(0f, 255f) * 0.17f) + (Random.Range(0f, 255f) * 0.8f);
        var co = new Color(color / 255, color/255, color/255);
        return co;
    }
}
