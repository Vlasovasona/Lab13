using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_10;

namespace Lab_13
{
    public delegate void CollectionHandler<T>(object source, CollectionHandlerEventArgs<T> e) where T : IInit, ICloneable, new();

    public class MyObservableCollection<T> : MyCollection<T> where T : IInit, ICloneable, new()
    {
        public event CollectionHandler<T> CollectionCountChanged; // Событие, срабатывающее при изменении количества элементов в коллекции
        public event CollectionHandler<T> CollectionReferenceChanged; // Событие, срабатывающее при изменении ссылок на элементы в коллекции

        // Конструкторы класса MyObservableCollection
        public MyObservableCollection() : base() { }

        public MyObservableCollection(string name) : base(name) { }

        public MyObservableCollection(string name, int size) : base(name, size) { }

        public MyObservableCollection(string name, MyCollection<T> c) : base(name, c) { }

        public void OnCollectionCountChanged(object source, CollectionHandlerEventArgs<T> e)        // Метод для вызова события CollectionCountChanged
        {
            CollectionCountChanged?.Invoke(source, e);
        }

        public void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs<T> e)        // Метод для вызова события CollectionReferenceChanged
        {
            CollectionReferenceChanged?.Invoke(source, e);
        }

        public void Add(T item)        // Переопределение метода Add для добавления нового элемента в коллекцию
        {
            base.Add(item);
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs<T>("добавлен новый", new PointHash<T>(item), null));
        }

        public void Remove(T item)        // Переопределение метода Remove для удаления элемента из коллекции
        {
            base.Remove(item);
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs<T>("удален", new PointHash<T>(item), null));
        }

        public T this[int index] // Переопределение индексатора для доступа и обновления элементов коллекции
        {
            get { return base[index]; }
            set
            {
                if (this.Contains(value)) throw new Exception("Невозможно добавить элемент, который уже находится в коллекции");
                else
                {
                    T tool = base[index];
                    base[index] = value;
                    OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs<T>("изменен", new PointHash<T>(tool), new PointHash<T>(value)));
                }
            }
        }
    }
}
