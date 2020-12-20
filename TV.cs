using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TV : MonoBehaviour
{
    public Text text;

    IEnumerator Read()
    {
        if (PlayerPrefs.GetInt("Mode") == 1)                                            // если выбран онлайн режим
        {
            text.text = "Соединение...";                                                // выводим текст о соединении, пока идёт обращение и загрузка с сервера
            WWWForm form = new WWWForm();
            form.AddField("mode", "read");                                              // добавляем поле "read" в форму, чтобы он вернул нам данные о всех учениках
            WWW www = new WWW("http://itkvantum.ru/vr.php", form);                      // отправляем все значения на сервер
            yield return www;                                                           // ожидаем результаты запроса
            if (www.text != "")                                                         // если есть хоть какие-то данные, то...
            {
                text.text = "";                                                         // стираем текст "Соединение..."
                string[] breakdown = www.text.Split('|');                               // разбивка данных на - фамилию и имя, баллы за каждый вопрос
                for (int i = 0; i < www.text.Split('|').Length - 1; i++)                // перебор результатов разбивки
                {
                    int count = 0;                                                      // объявление переменной, которая будет хранить сумму всех баллов конкретного ученика
                    text.text += breakdown[i].Split(',')[0] + " | ";                    // сбор финального текста
                    for (int j = 0; j < 5; j++)                                         // перебор отдельно взятого балла
                    {
                        char numeral = breakdown[i].Split(',')[1][j];                   // значение отдельного балл
                        text.text += numeral + " ";                                     // сбор финального текста
                        if (numeral == '1')                                             // если за это задание получен балл, то...
                            count++;                                                    // добавление этого балла к общей сумме
                    }
                    text.text += "| в сумме " + count + " баллов\n";                    // сбор финального текста
                }
            }
            else
            {
                text.alignment = TextAnchor.MiddleCenter;                               // расположение текста посередине
                text.color = new Color(1, 1, 1, 0.4f);                                  // изменение прозрачности текста
                text.fontSize = 50;                                                     // увеличение текста в размерах
                text.text = "Пусто";                                                    // пояснение, что в базе данных отсутсвуют данные
            }
        }
        else                                                                            // если выбран локальный режим
            text.text = PlayerPrefs.GetString("Table");                                 // вывод локальных данных
    }

    void Start()
    {
        text.text = "";                                                                 // очищение текста при старте от значений по умолчанию в редакторе
        StartCoroutine(Read());                                                         // запуск корутина, который выводит данные на экран телевизора
    }
}
