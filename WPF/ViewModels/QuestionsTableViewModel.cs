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
            QuestionsTable = new QuestionsTable(new QuestionsLine[]
            {
                new QuestionsLine("Основы програмиронивая", new QuestionsTableItem[]
                {
                    new QuestionsTableItem("Какой термин обозначает набор команд, выполняемых компьютером?", 100, true, "Программа"),
                    new QuestionsTableItem("Как называется управляющая конструкция, позволяющая повторять выполнение определенного блока кода до выполнения заданного условия?", 200, true, "Цикл"),
                    new QuestionsTableItem("Как называется процесс перевода исходного кода программы на языке программирования в машинный код?", 300, true, "Компиляция"),
                    new QuestionsTableItem("Какой оператор позволяет установить условие выполнения блока кода", 400, true, "If", "если"),
                    new QuestionsTableItem("Как называется функция, которая вызывает саму себя?", 500, true, "Рекурсия"),
                    new QuestionsTableItem("Какой оператор используется для увеличения значения переменной на 1?", 600, true, "Increment", "инкеремент")
                })
            });
        }
    }
}
