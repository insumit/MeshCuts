using UnityEngine;
using System.Collections;

public class Scaling : MonoBehaviour
{
    public int startSize = 1;
    public int minSize = 1;
    public int maxSize = 2;

    public float speed = 2.0f;

    public  Vector3 targetScale;
    public  Vector3 baseScale;
    public  int currScale;

    void Start()
    {
        baseScale = transform.localScale;
        transform.localScale = baseScale * startSize;
        currScale = startSize;
        targetScale = baseScale * startSize;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, speed * Time.deltaTime);

        //For testing. Comment this "if" section to use custom controls for scaling
        if (Input.GetKeyDown(KeyCode.UpArrow))
            ChangeSize(true);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            ChangeSize(false);
    }

    public void ChangeSize(bool bigger)
    {

        if (bigger)
            currScale++;
        else
            currScale--;

        currScale = Mathf.Clamp(currScale, minSize, maxSize + 1);

        targetScale = baseScale * currScale;
    }
}
