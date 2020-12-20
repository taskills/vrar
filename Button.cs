using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(1);                                                                                  // загружаем сцену с экзаменом
    }

    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(36.1f, 1.205f, -16.6f);         // идёт поиск по тегу "Player" 
    }
}