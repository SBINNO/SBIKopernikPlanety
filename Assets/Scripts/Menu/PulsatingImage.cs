using UnityEngine;
using UnityEngine.UI;

public class PulsatingImage : MonoBehaviour
{
    public float pulsateSpeed = 1.0f;
    public float minAlpha = 0.0f;
    public float maxAlpha = 1.0f;
    // Start is called before the first frame update

    private Image image;
    private bool isImageNull = true;
    void Start()
    {
        image = GetComponent<Image>();
        if (image != null)
        {
            isImageNull = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isImageNull)
        {
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(Time.time * pulsateSpeed, 1.0f));

            Color imageColor = image.color;
            imageColor.a = alpha;
            image.color = imageColor;
        }
    }
}
