using UnityEngine;

public class FadeInAnim : MonoBehaviour
{

    float time = 0;

    // Update is called once per frame
    void Update()
    {
        if (time < 3f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time / 3);
        }
        else
        {
            time = 0;
            this.gameObject.SetActive(false);
        }
        time += Time.deltaTime;

    }

    public void resetAnim()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        this.gameObject.SetActive(true);
        time = 0;
    }
}
