using UnityEngine.UI;
using UnityEngine;

public class FloatUpText : MonoBehaviour
{
    Text text;
    float alpha = 1;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
        text.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, 20, 0);//going up speed
        alpha -= 0.05f; //fading speed
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        if (alpha < 0) Destroy(this.gameObject);
    }
}
