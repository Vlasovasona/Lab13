using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_10;
using Лаба12_часть4;

namespace Lab_13
{
    public class CollectionHandlerEventArgs<T>: EventArgs where T : Library_10.IInit, System.ICloneable, new() //класс для передачи информации о событиях
    {
        public string TypeOfChanges { get; set; } //тип изменений
        public PointHash<T> Element { get; set; } //для элемента с которым произошли изменения
        public PointHash<T> NewElement { get; set; } // для нового элемента (в случае замены)

        public CollectionHandlerEventArgs(string type, PointHash<T> element, PointHash<T> newElement) // Include newElement in the constructor
        {
            TypeOfChanges = type;
            Element = element;
            NewElement = newElement;
        }

    }
}
