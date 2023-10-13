using WPF.Models;

namespace WPF.ViewModels
{
    public class QuestionsTableViewModel : ViewModel
    {
        private QuestionsTable questionsTable;
        public QuestionsTable QuestionsTable
        {
            get => questionsTable;
            set => Set(ref questionsTable, value);
        }

        public QuestionsTableViewModel()
        {
            QuestionsTable = new QuestionsTable("Тест",
                new QuestionsLine("Основы програмиронивания", new QuestionsLineItem[]
            {
                new QuestionsLineItem("Какой термин обозначает набор команд, выполняемых компьютером?", 100, false, "Программа"),
                new QuestionsLineItem("Как называется управляющая конструкция, позволяющая повторять выполнение определенного блока кода до выполнения заданного условия?", 200, false, "Цикл"),
                new QuestionsLineItem("Как называется процесс перевода исходного кода программы на языке программирования в машинный код?", 300, false, "Компиляция"),
                new QuestionsLineItem("Какой оператор позволяет установить условие выполнения блока кода", 400, false, "If", "если"),
                new QuestionsLineItem("Как называется функция, которая вызывает саму себя?", 500, false, "Рекурсия"),
                new QuestionsLineItem("Какой оператор используется для увеличения значения переменной на 1?", 600, false, "Increment", "инкеремент")
            }),
                new QuestionsLine("Фейк програмиронивание", new QuestionsLineItem[]
            {
                new QuestionsLineItem("Какой термин обозначает набор команд, выполняемых компьютером?", 1000, false, "Программа"),
                new QuestionsLineItem("Как называется управляющая конструкция, позволяющая повторять выполнение определенного блока кода до выполнения заданного условия?", 2000, false, "Цикл"),
                new QuestionsLineItem("Как называется процесс перевода исходного кода программы на языке программирования в машинный код?", 3000, false, "Компиляция"),
                new QuestionsLineItem("Какой оператор позволяет установить условие выполнения блока кода", 4000, false, "If", "если"),
                new QuestionsLineItem("Как называется функция, которая вызывает саму себя?", 5000, false, "Рекурсия"),
                new QuestionsLineItem("Какой оператор используется для увеличения значения переменной на 1?", 6000, false, "Increment", "инкеремент")
            }));
        }
    }
}
