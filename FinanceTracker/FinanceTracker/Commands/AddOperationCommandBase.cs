using FinanceTracker.Enums;
using FinanceTracker.Interfaces;
using FinanceTracker.Models;
using FinanceTracker.Utils;

namespace FinanceTracker.Commands
{
    public abstract class AddOperationCommandBase : ICommand
    {
        protected OperationType _type;
        public void Execute(Account account)
        {
            UserInputReader reader = new UserInputReader();
            Guid id = Guid.NewGuid();

            /*
            В чем возникла проблема: нужно сделать категория операции, но для
            Income одни категории, для Expence другие -> разные enum нужны
            Отсюда следующая задача: определить в райнтайме, в зависимости от типа операции
            какого типа свойста "Category"(смотри папку Enums) создать
            Я попробовал это реализовать через базовый класс "CategoryBase",
            который наследуют классы "CategoryIncome/CategoryExpence"
            Каждый из классов "детей" имеет нужный enum. 
            Таким образом я хотел решить вышеизложеную задачу
            Дальше читай в классе UserInputReader
             */
            CategoryBase category = reader.GetCategory(_type); // идея в том, чтобы здесь получить нужную категорию в райнтайме
            decimal amount = reader.GetAmount();
            string description = reader.GetDescription();
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            Operation operation = new Operation(id, _type, category, amount, description, date);
            account.Operations.Add(operation);
            account.UpdateBalance(operation);
        }

        public AddOperationCommandBase(OperationType type)
        {
            _type = type;
        }
    }
}
