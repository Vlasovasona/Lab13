using System.Collections;
using System.Diagnostics.Metrics;
using Library_10;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;
using Лаба12_часть4;

namespace Lab_13
{
    internal class Program
    {
        static sbyte InputSbyteNumber(string msg = "Введите число")  //функция для проверки введенного числа на тип sbyte
        {
            Console.WriteLine(msg); //вывод сообщения msg
            bool isConvert; //объявление переменной, отвечающей за проверку на корректность
            sbyte number; //переменная, которой будет присвоено корректно введенное число
            do
            {
                isConvert = sbyte.TryParse(Console.ReadLine(), out number); //проверка на принадлежность типу sbyte
                if (!isConvert || number <= 0)
                {
                    Console.WriteLine("Неправильно введено число. Возможно вы ввели слишком длинное число. Попробуйте заново"); //в случае провала, вывод сообщения о некорректном вводе числа
                    isConvert = false;
                }
            } while (!isConvert); //повторение цикла до тех пор, пока пользователь не введет корректное число
            return number; //ф-ция принимает значение введенного корректного числа
        }

        static int InputIntNumber(string msg = "Введите число")  //функция для проверки введенного числа на тип sbyte
        {
            Console.WriteLine(msg); //вывод сообщения msg
            bool isConvert; //объявление переменной, отвечающей за проверку на корректность
            int number; //переменная, которой будет присвоено корректно введенное число
            do
            {
                isConvert = int.TryParse(Console.ReadLine(), out number); //проверка на принадлежность типу sbyte
                if (!isConvert || number <= 0)
                {
                    Console.WriteLine("Неправильно введено число. Возможно вы ввели слишком длинное число. Попробуйте заново"); //в случае провала, вывод сообщения о некорректном вводе числа
                    isConvert = false;
                }
            } while (!isConvert); //повторение цикла до тех пор, пока пользователь не введет корректное число
            return number; //ф-ция принимает значение введенного корректного числа
        }


        static void DeleteElement(MyObservableCollection<HandTool> table)
        {
            if (table.Count <= 0 || table == null) throw new Exception("Таблица пустая или не создана");
            HandTool tool = new HandTool();
            Console.WriteLine("Введите элемент, который нужно найти и удалить");
            tool.Init();
            if (table.Contains(tool))
            {
                table.Remove(tool); //здесь был вызван метод RemoveData, нужно было Remove, по этой причине не записывалась информация про удаление элемента в журнал
                if (table.Count == 0) Console.WriteLine("В ходе удаления была получена пустая таблица");
            }
            else
                throw new Exception("Элемент не найден в таблице. Удаление невозможно");
            Console.WriteLine("Удаление выполнено");
        }

        static void AddElement(MyObservableCollection<HandTool> col)
        {
            if (col.Count <= 0 || col == null) throw new Exception("Таблица пустая или не создана");
            HandTool tool = new HandTool();
            tool.Init();
            col.Add(tool);
        }

        static void PrintCollection(MyObservableCollection<HandTool> collection)
        {
            if (collection.Count <= 0 || collection == null) throw new Exception("Таблица пустая или не создана");
            Console.WriteLine("Вывод хеш-таблицы");
            collection.Print();
        }

        static void ChangeData(MyObservableCollection<HandTool> collection)
        {
            if (collection.Count <= 0 || collection == null) throw new Exception("Таблица пустая или не создана");
            HandTool tool = new HandTool();
            Console.WriteLine("Введите элемент, который нужно заменить");
            tool.Init();
            if (collection.Contains(tool))
            {
                Console.WriteLine("Введите элемент для замены");
                HandTool tool2 = new HandTool();
                tool2.Init();
                collection[collection.IndexOf(tool)] = tool2; //сначала вводим элемент, который нужно заменить, затем метод IndexOf находит его индекс в коллекции и тогда вызывается индексатор от найденного индекса
                Console.WriteLine($"Замена элемента {tool} на элемент {tool2} прошла успешно!");
            }
            else throw new Exception("Элемент для замены не найден");
        }

        static void CreateCollection(ref MyObservableCollection<HandTool> collection, string name)
        {
            int size1 = InputIntNumber($"Введите количество элементов {name}");
            if (size1 <= 0) throw new Exception("хеш-таблица не может быть нулевой или отрицательной длины");
            collection = new MyObservableCollection<HandTool>(name, size1);
            Console.WriteLine($"{name} сформирована");
        }

        static void Main(string[] args)
        {
            MyObservableCollection<HandTool> col1 = new MyObservableCollection<HandTool>("Первая коллекция");
            MyObservableCollection<HandTool> col2 = new MyObservableCollection<HandTool>("Вторая коллекция");

            Journal<HandTool> j1 = new Journal<HandTool>("Первый журнал");
            Journal<HandTool> j2 = new Journal<HandTool>("Второй журнал");
            sbyte answer1; //объявление переменных, которые отвечают за выбранный пункт меню
            do
            {
                Console.WriteLine("1. Сформировать 1 хеш-таблицу с помощью ввода длины");
                Console.WriteLine("2. Сформировать 2 хеш-таблицу с помощью ввода длины");

                Console.WriteLine("\nДействия с первой коллекцией:");
                Console.WriteLine("3. Добавить элемент в первую коллекцию");
                Console.WriteLine("4. Удалить элемент из первой коллекции");
                Console.WriteLine("5. Присвоить новое значение какому-нибудь элементу первой коллекции");
                Console.WriteLine("6. Вывести первую коллекцию");

                Console.WriteLine("\nДействия со второй коллекцией:");
                Console.WriteLine("7. Добавить элемент во вторую коллекцию");
                Console.WriteLine("8. Удалить элемент из второй коллекции");
                Console.WriteLine("9. Присвоить новое значение какому-нибудь элементу второй коллекции");
                Console.WriteLine("10. Вывести вторую коллекцию");
                Console.WriteLine("11. Вывести журналы");

                Console.WriteLine("\n12. Завершить работу программы");
                Console.WriteLine("___________________________________________________________________________________________");

                answer1 = InputSbyteNumber();

                switch (answer1)
                {
                    case 1: //формирование коллекции ввод длины
                        {
                            try
                            {
                                CreateCollection(ref col1, "Первая коллекция");

                                col1.CollectionCountChanged += j1.CollectionCountChanged;
                                col1.CollectionReferenceChanged += j1.CollectionReferenceChanged;
                                col1.CollectionReferenceChanged += j2.CollectionReferenceChanged;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 2:
                        {
                            try
                            {
                                CreateCollection(ref col2, "Вторая коллекция");

                                col2.CollectionReferenceChanged += j2.CollectionReferenceChanged;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 3: //добавить элемент в первую
                        {
                            try
                            {
                                AddElement(col1);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 4: //удаление элемента первой коллекции
                        {
                            try
                            {
                                DeleteElement(col1);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 5: //изменение значения элемента первой коллекции
                        {
                            try
                            {
                                ChangeData(col1);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 6: //вывод на экран первой коллекции
                        {
                            try
                            {
                                PrintCollection(col1);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 7: //добавить элемент во вторую коллекцию
                        {
                            try
                            {
                                AddElement(col2);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 8: //удалить элемент из второй коллекции
                        {
                            try
                            {
                                DeleteElement(col2);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 9: //присвоить новое значение элементу из второй коллекции
                        {
                            try
                            {
                                ChangeData(col2);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 10: //вывести вторую коллекцию
                        {
                            try
                            {
                                PrintCollection(col2);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 11: //проверить содержится ли элемент в коллекции
                        {
                            try
                            {
                                j1.WriteNotes();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            try
                            {
                                j2.WriteNotes();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 12: //завершение работы программы
                        {
                            Console.WriteLine("Демонстрация завершена");
                            break;
                        }
                    default: //неправильный ввод пункта меню
                        {
                            Console.WriteLine("Неправильно задан пункт меню");
                            break;
                        }
                }
            } while (answer1 != 12);

        }
    }
}


