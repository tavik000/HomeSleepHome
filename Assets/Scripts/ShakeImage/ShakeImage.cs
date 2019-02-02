using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeImage : MonoBehaviour
{
    private RectTransform rt;
    private Vector3 originalPosition;
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        originalPosition = rt.transform.position;
        gameObject.SetActive(false);
    }
    void FixedUpdate()
    {
        rt.transform.position = new Vector2(originalPosition.x+Random.Range(-10,10),originalPosition.y+Random.Range(-10,10));
    }
}

