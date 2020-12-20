using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalOrOnline : MonoBehaviour
{
    public int index;

    void OnCollisionEnter(Collision col)
    {
        PlayerPrefs.SetInt("Mode", index);              // сохранение выбранного мода в долгострочной памяти
        SceneManager.LoadScene(0);                      // переход на сцену с ромашкинским месторождением
    }
}