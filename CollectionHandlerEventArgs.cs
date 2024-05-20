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
        public string TypeOfChanges { get; set; } //тип изменений в коллекции
        public PointHash<T> Element { get; set; } //св-ство для ссылки на объект, скоторым связаны изменения

        public CollectionHandlerEventArgs(string type, PointHash<T> element) //упаковка информации для дальнейшей передачи
        {
            TypeOfChanges = type;
            Element = element;
        }

    }
}
