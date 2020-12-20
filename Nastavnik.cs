using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Nastavnik : MonoBehaviour
{
    public Text text;
    public int index;
    

    IEnumerator Wait()
    {
        text.text = "<color='#00FF00'>" + text.text + "</color>";               // изменение тектса на зелёный цвет
        yield return new WaitForSeconds(2);                                     // ожидание 2 секунды
        SceneManager.LoadScene(0);                                              // переход на сцену с ромашкинским месторождением
    }

    public IEnumerator Delete()
    {
        if (PlayerPrefs.GetInt("Mode") == 1)                                    // если выбран онлайн режим
        {
            WWWForm form = new WWWForm();
            form.AddField("mode", "delete");                                    // добавляем поле "delete" в форму, чтобы он удалил все данные на сервере
            WWW www = new WWW("http://itkvantum.ru/vr.php", form);              // отправляем все значения на сервер
            yield return www;                                                   // ожидаем результаты запроса
        }
        else                                                                    // если выбран локальный режим
            PlayerPrefs.DeleteKey("Table");                                     // удаляем локальные данные об учениках
        text.alignment = TextAnchor.MiddleCenter;                               // // расположение текста посередине
        text.color = new Color(1, 1, 1, 0.4f);                                  // изменение прозрачности текста
        text.fontSize = 50;                                                     // увеличение текста в размерах
        text.text = "<color='#FF0000'>Данные удалены</color>";                  // сообщение о том, что данные удалены
    }

    void OnCollisionEnter(Collision col)
    {
        if (index == 0)                                                         // если пользователь нажал на кнопку "Ознакомлен"
            StartCoroutine(Wait());                                             // запуск корутины
        else if (index == 1)                                                    // если пользователь нажал на кнопку "Пересдача"
            StartCoroutine(Delete());                                           // запуск корутины
    }
}