using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExamCtrl : MonoBehaviour
{
    public Text text;
    int[] answer = new int[5];
    int stadia, question;
    bool isWriteName, isFullText;
    string ru = "абвгдежзийклмнопрстуфхцчшщыьэюя", name, surname;

    public IEnumerator _Write()
    {
        if (PlayerPrefs.GetInt("Mode") == 1)                                                                // если выбран онлайн режим
        {
            WWWForm form = new WWWForm();
            form.AddField("mode", "write");                                                                 // добавляем поле "write" в форму, чтобы он записал данные об ученике, который прошел тест
            form.AddField("name", name);                                                                    // добавляем поле "write" в форму, в которой будет имя ученика
            form.AddField("surname", surname);                                                              // добавляем поле "write" в форму, в которой будет фамилия ученика
            for (int i = 1; i < 6; i++)                                                                     // перебор ответов на вопросы
                form.AddField("quiz" + i, answer[i - 1].ToString());                                        // добавляем поле "quiz" + индекс вопроса в форму, в которой будет результат ответа на данынй вопрос
            WWW www = new WWW("http://itkvantum.ru/vr.php", form);                                          // отправляем все значения на сервер
            yield return www;                                                                               // ожидаем результаты запроса
        }
        else                                                                                                // если выбран локальный режим
        {
            string line = name + " " + surname + " | ";                                                     // запись имени и фамилии в переменную line
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                line += answer[i] + " ";                                                                    // добавление результата за ответ на данынй вопрос в line
                if (answer[i] == '1')                                                                       // если ответ правильный
                    count++;                                                                                // добаление балла к общей сумме
            }
            line += "| в сумме " + count + " баллов\n";                                                     // добавление суммы баллов в line
            PlayerPrefs.SetString("Table", PlayerPrefs.GetString("Table") + line);                          // запись нового ученика в локальную базу данных
        }
        SceneManager.LoadScene(0);                                                                          // переход на сцену с ромашкинским месторождением
    }

    public void Write(int index)
    {
        if (isWriteName)
        {
            if (index == 100)
            {
                if (stadia == 0)
                {
                    stadia = 1;
                    name = text.text;
                    text.text = "Введите фамилию";
                    isFullText = true;
                }
                else if (stadia == 1)
                {
                    stadia = 2;
                    surname = text.text;
                    text.text = "";
                    StartCoroutine(_Write());
                }
                return;
            }
            else
            {
                if (isFullText)
                {
                    isFullText = false;
                    text.text = "";
                }
                if (text.text.Length < 15)
                    text.text += ru[index];
            }
        }
        else
        {
            if (index == 100)
            {
                question++;
                if (question == 1)
                {
                    if (text.text.ToLower() != "мирчинк")
                    {
                        text.text = "Какой горизонт Ромашкинского месторождения находится на глубине 200м?";
                        isFullText = true;
                        return;
                    }
                    text.text = "Какой горизонт Ромашкинского месторождения находится на глубине 200м?";
                    isFullText = true;
                }
                else if (question == 2)
                {
                    if (text.text.ToLower() != "филлиповский")
                    {
                        text.text = "Какой ярус Ромашкинского месторождения находится на глубине 380м?";
                        isFullText = true;
                        return;
                    }
                    text.text = "Какой ярус Ромашкинского месторождения находится на глубине 380м?";
                    isFullText = true;
                }
                else if (question == 3)
                {
                    if (text.text.ToLower() != "касимовский")
                    {
                        text.text = "На каком ярусе встречаются первые залежи нефти?";
                        isFullText = true;
                        return;
                    }
                    text.text = "На каком ярусе встречаются первые залежи нефти?";
                    isFullText = true;
                }
                else if (question == 4)
                {
                    if (text.text.ToLower() != "гжельский")
                    {
                        text.text = "На каком горизонте начинаются доломиты и ангидриты?";
                        isFullText = true;
                        return;
                    }
                    text.text = "На каком горизонте начинаются доломиты и ангидриты?";
                    isFullText = true;
                }
                else if (question == 5)
                {
                    if (text.text.ToLower() != "иренский")
                    {
                        isWriteName = true;
                        isFullText = true;
                        text.text = "Введите имя";
                        return;
                    }
                    isWriteName = true;
                    isFullText = true;
                    text.text = "Введите имя";
                }
                answer[question - 1] = 1;
            }
            else
            {
                if (isFullText)
                {
                    isFullText = false;
                    text.text = "";
                }
                if (text.text.Length < 15)
                    text.text += ru[index];
            }
        }
    }

    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = Vector3.zero;

        text.text = "Какая фамилия геолога, который открыл Ромашкинское месторождение?";
        question = 0;
        isFullText = true;
    }
}