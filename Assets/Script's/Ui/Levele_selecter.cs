using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public GameObject scrollbar;
    private float scrollPos = 0;
    private float[] pos;

    private Touchscreen touchscreen;

    // Use this for initialization

    void Awake()
    {
        int childCount = transform.childCount;
        pos = new float[childCount];
        float distance = 1f / (childCount - 1f);
        for (int i = 0; i < childCount; i++)
        {
            pos[i] = distance * i;
        }
        touchscreen = Touchscreen.current;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (touchscreen != null  )
        {
            if (touchscreen.touches.Count > 0)
            {
                scrollPos = scrollbar.GetComponent<Scrollbar>().value;
            }
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollPos < pos[i] + (1f / (pos.Length - 1f)) / 2f && scrollPos > pos[i] - (1f / (pos.Length - 1f)) / 2f)
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (1f / (pos.Length - 1f)) / 2f && scrollPos > pos[i] - (1f / (pos.Length - 1f)) / 2f)
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }
    }
    public void LoadeLeavel(string level)
    {
        SceneManager.LoadScene(level);
    }
}