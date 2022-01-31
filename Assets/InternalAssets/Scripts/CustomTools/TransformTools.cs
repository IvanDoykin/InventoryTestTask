using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTools : MonoBehaviour
{
    public static TransformTools Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SmoothMoveTo(Transform obj, Vector3 destination, float time)
    {
        StartCoroutine(SmoothMove(obj, destination, time));
    }

    private IEnumerator SmoothMove(Transform obj, Vector3 destination, float time)
    {
        float waitedTime = 0f;
        Vector3 startPoint = obj.transform.position;

        while (waitedTime < time)
        {
            waitedTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(startPoint, destination, waitedTime / time);
            yield return null;
        }
    }

    public void SmoothRotationTo(Transform obj, Vector3 eulerRotation, float time)
    {
        StartCoroutine(SmoothRotation(obj, eulerRotation, time));
    }

    private IEnumerator SmoothRotation(Transform obj, Vector3 eulerRotation, float time)
    {
        float waitedTime = 0f;
        Vector3 startRotation = obj.transform.eulerAngles;

        while (waitedTime < time)
        {
            waitedTime += Time.deltaTime;
            obj.transform.eulerAngles = Vector3.Lerp(startRotation, eulerRotation, waitedTime / time);
            yield return null;
        }
    }

}
