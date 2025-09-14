using UnityEngine;
using TMPro;
using System.Collections;

public class ImpactScoreText : MonoBehaviour
{
    [SerializeField] private TMP_Text tmp;

    [Header("Motion")]
    public Vector3 moveOffset = new Vector3(0f, 1f, 0f);
    public float duration = 0.8f;

    private Color baseColor;

    private void Awake()
    {
        if (!tmp) tmp = GetComponent<TMP_Text>();
        baseColor = tmp.color;
    }

    private void OnEnable()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        Vector3 start = transform.position;
        Vector3 end = start + moveOffset;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float u = t / duration;

            // position
            transform.position = Vector3.Lerp(start, end, u);

            // fade out
            Color c = baseColor;
            c.a = Mathf.Lerp(1f, 0f, u);
            tmp.color = c;

            yield return null;
        }

        Destroy(gameObject);
    }
}
