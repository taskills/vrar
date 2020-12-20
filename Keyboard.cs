using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Keyboard : MonoBehaviour
{
    public ExamCtrl ctrl;
    public int index;
    bool isCan;

    IEnumerator Wait()
    {
        isCan = false;
        yield return new WaitForSeconds(1);
        isCan = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (isCan)
        {
            ctrl.Write(index);
            StartCoroutine(Wait());
        }
    }

    void Start()
    {
        isCan = true;
    }
}