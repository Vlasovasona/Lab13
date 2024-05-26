using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Library_10;

namespace Lab_13
{
    // Класс JournalEntry<T> представляет запись журнала, отражающую изменения в коллекции
    public class JournalEntry<T> where T : IInit, ICloneable, new()
    {
        public string NameOfCollection { get; set; } // Имя коллекции, в которой произошли изменения
        public string TypeOfChanges { get; set; } // Тип произошедших изменений
        public string Element { get; set; } // Элемент, над которым произошли изменения
        public string NewElement { get; set; } // в случае если элемент был заменен, сохраняем новое значение

        public JournalEntry(object source, CollectionHandlerEventArgs<T> e)
        {
            NameOfCollection = ((MyCollection<T>)source).NameOfCollection;
            TypeOfChanges = e.TypeOfChanges;
            Element = new PointHash<T>(e.Element.Data).ToString();
            if (e.NewElement != null)
            {
                NewElement = new PointHash<T>(e.NewElement.Data).ToString();
            }
            else NewElement = null;
        }

        public string ToString()        // Переопределенный метод ToString для возвращения строкового представления записи журнала
        {
            if (NewElement == null)
                return $"В коллекции {NameOfCollection}, {TypeOfChanges} элемент {Element}";
            else return $"В коллекции {NameOfCollection}, элемент {Element} {TypeOfChanges} на {NewElement}";
        }
    }

    // Класс Journal<T> представляет журнал, в который записывается информация об изменениях коллекции
    public class Journal<T> where T : IInit, ICloneable, new()
    {
        List<JournalEntry<T>> arr; // Список записей журнала
        public string NameOfJournal; // Имя журнала

        public Journal(string str)        // Конструктор класса для инициализации
        {
            arr = new List<JournalEntry<T>>();
            NameOfJournal = str;
        }

        public string GetLastNote() //метод для тестирования
        {
            foreach (var item in arr)
            {
                return item.ToString();
            }
            return null;
        }

        public void CollectionCountChanged(object source, CollectionHandlerEventArgs<T> e)        // Метод для обработки события изменения количества элементов в коллекции
        {
            JournalEntry<T> newElement = new JournalEntry<T>(source, e); // Создание новой записи
            arr.Add(newElement); // Добавление записи в журнал
        }

        public void CollectionReferenceChanged(object source, CollectionHandlerEventArgs<T> e)        // Метод для обработки события изменения ссылок на элементы в коллекции
        {
            JournalEntry<T> newElement = new JournalEntry<T>(source, e); // Создание новой записи
            arr.Add(newElement); // Добавление записи в журнал
        }

        public void WriteNotes()        // Метод для вывода записей журнала на консоль
        {
            if (arr.Count == 0) throw new Exception("Журнал пустой");
            foreach (var entry in arr)
            {
                if (entry.NewElement == null)
                    Console.WriteLine($"Журнал: {NameOfJournal}. В хеш-таблице(-у) {entry.NameOfCollection} {entry.TypeOfChanges} элемент {entry.Element}.");
                else
                    Console.WriteLine($"Журнал: {NameOfJournal}. В хеш-таблице(-у) {entry.NameOfCollection} {entry.TypeOfChanges} элемент {entry.Element} на {entry.NewElement}");
            }
        }
    }
}
