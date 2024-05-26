using Library_10;
using Lab_13;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Collections.ObjectModel;

namespace TestLab13
{
    [TestClass]
    public class Test_Lab13
    {
        //тестирование конструкторов
        [TestMethod]
        public void Test_ConstuctorWithoutParams() //тест проверка на создание пустого объекта MyCollection
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>();
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void Test_ConstuctorOnlyName() //тест проверка на создание пустого объекта MyCollection
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>("r");
            Assert.AreEqual("r", collection.NameOfCollection);
        }

        [TestMethod]
        public void Test_Constructor_Length() //проверка конструктора, формаирующего коллекцию по ее длине
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию с помощью ввода ее длины
            Assert.AreEqual(collection.Count, 5); //проверяем чтобы коллекция содержала 5 элементов
        }
        //тестирование конструкторов завершено

        //тестирование нумератора
        [TestMethod]
        public void GetEnumerator_WhenCollectionHasItems_ShouldEnumerateAllItems() //нумератор для коллекции 
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 1); //создаем коллекцию и заполняем ее элементами
            Instrument tool1 = new Instrument("Q", 1);
            Instrument tool2 = new Instrument("W", 12);
            Instrument tool3 = new Instrument("E", 123);
            PointHash<Instrument> firstElement = collection.GetFirstValue();

            collection.Add(tool1);
            collection.Add(tool2);
            collection.Add(tool3);
            collection.Remove(firstElement.Data);

            Instrument[] result = new Instrument[3];
            int index = 0;
            foreach (Instrument item in collection)
            {
                result[index] = item;
                index++;
            }
            CollectionAssert.AreEqual(new Instrument[] { tool1, tool2, tool3 }, result);
        }

        [TestMethod]
        public void GetEnumerator_CollectionHasRemovedElement() //нумератор для коллекции с удаленным элементом
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 1); //создаем коллекцию и заполняем ее элементами
            Instrument tool1 = new Instrument("Q", 1);
            Instrument tool2 = new Instrument("W", 12);
            Instrument tool3 = new Instrument("E", 123);
            PointHash<Instrument> firstElement = collection.GetFirstValue();

            collection.Add(tool1);
            collection.Add(tool2);
            collection.Add(tool3);

            collection.Remove(tool2);
            collection.Remove(firstElement.Data);

            Instrument[] result = new Instrument[2];
            int index = 0;
            foreach (Instrument item in collection)
            {
                result[index] = item;
                index++;
            }

            CollectionAssert.AreEqual(new Instrument[] { tool1, tool3 }, result);
        }
        //тестирование нумератора завершено

        //тестирование ICollection
        [TestMethod]
        public void ICollection_CopyTo() // проверка CopyTo 
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            collection.CopyTo(list, 0);
            PointHash<Instrument> value = collection.GetFirstValue();
            Assert.AreEqual(value.Data, list[0]);
        }

        [TestMethod]
        public void ICollection_Count() //проверка счетчика элементов
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            collection.CopyTo(list, 0);
            Assert.AreEqual(collection.Count, list.Length);
        }
        //тестирование ICollection

        //блок Exception
        [TestMethod]
        public void ICollection_CopyTo_ExceptionIndexOutsideOfListLength() //проверка исключения при некорректном вводе индекса при попытке скопирровать значения коллекции в массив
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            Assert.ThrowsException<Exception>(() =>
            {
                collection.CopyTo(list, 6);
            });
        }

        [TestMethod]
        public void ICollection_CopyTo_ExceptionNotEnoughListLength() //ошибка, когда не хватает места для всех элементов в массиве
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            Assert.ThrowsException<Exception>(() =>
            {
                collection.CopyTo(list, 2);
            });
        }

        [TestMethod]
        public void TestClear() //проверка очистки памяти
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 6);
            collection.Clear();
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void TestAdd() //проврека добавления элемента
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 1);
            Instrument t = new Instrument();
            collection.Add(t);
            Assert.IsTrue(collection.Contains(t));

        }

        //ТЕСТЫ ИЗ ВТОРОЙ ЧАСТИ ДЛЯ ПОКРЫТИЯ КЛАССОВ ХЕШ-ТАБЛИЦЫ
        //блок Exception
        [TestMethod]
        public void Test_CreateTable_Exception() //тестирование ошибки при попытке формирования пустой таблицы
        {
            Assert.ThrowsException<Exception>(() =>
            {
                MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(-1);
            });
        }

        [TestMethod]
        public void Test_AddExistingElement_Exception() //тестирование ошибки при попытке формирования пустой таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            Instrument tool = new Instrument("q", 1);
            table.AddPoint(tool);
            Assert.ThrowsException<Exception>(() =>
            {
                table.AddPoint(tool);
            });
        }

        [TestMethod]
        public void Test_PrintNullTable_Exception() //тестирование ошибки при попытке печати пустой таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>();
            Assert.ThrowsException<Exception>(() =>
            {
                table.Print();
            });
        }//блок Exception закончен

        [TestMethod]
        public void TestCreateTable() //тестирование конструктора для создания хеш-таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(5);
            Assert.AreEqual(table.Capacity, 5);
        }

        //тестривание AddPoint
        [TestMethod]
        public void TestAddPointToHashTable() //тестирование добавления элемента в таблицу
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(5);
            HandTool tool = new HandTool();
            table.AddPoint(tool);
            Assert.IsTrue(table.Contains(tool));
        }

        [TestMethod]
        public void TestAddCount() //тестирование увеличения Count после добавления элемента в таблицу
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(5);
            HandTool tool = new HandTool();
            table.AddPoint(tool);
            Assert.AreEqual(6, table.Count);
        }

        //тестиование удаления элемента из таблицы
        [TestMethod]
        public void TestRemovePointFromHashTableTrue() //тестирование добавления удаления существующего элемента из таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            HandTool tool = new HandTool();
            table.AddPoint(tool);
            table.RemoveData(tool);
            Assert.IsFalse(table.Contains(tool));
        }

        [TestMethod]
        public void TestRemovePointFromHashTable_False() //тестирование удаления несуществующего элемента из таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            HandTool tool = new HandTool();
            table.AddPoint(tool);
            table.RemoveData(tool);
            Assert.IsFalse(table.Contains(tool));
        }

        [TestMethod]
        public void TestRemovePointFromHashTable_OutOfKey_False() //тестирование удаления несуществующего элемента из таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            Instrument tool = new Instrument("Бензопила дружба нового поколения", 9999);
            Assert.IsFalse(table.RemoveData(tool));
        }

        [TestMethod]
        public void TestRemovePoint_FromBeginingOfTableTable() //тестирование удаления первого в цепочке элемента из таблицы
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            Instrument tool2 = new Instrument("Перфоратор", 98);
            Instrument tool3 = new Instrument("Штангенциркуль", 85);
            Instrument tool4 = new Instrument("Микрометр", 41);
            Instrument tool5 = new Instrument("RRR", 1234);
            Instrument tool6 = new Instrument("RRR", 1235);

            table.AddPoint(tool2);
            table.AddPoint(tool3);
            table.AddPoint(tool4);
            table.AddPoint(tool5);
            table.AddPoint(tool6);

            PointHash<Instrument> tool = new PointHash<Instrument>();
            PointHash<Instrument> pointHash = table.GetFirstValue();
            tool = pointHash;
            table.RemoveData(tool.Data);
            Assert.IsFalse(table.Contains(tool.Data));
        }


        //тестирование метода Contains
        [TestMethod]
        public void TestContainsPointTrue() //метод Contains когда элемент есть в таблице
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            HandTool tool = new HandTool();
            table.AddPoint(tool);
            Assert.IsTrue(table.Contains(tool));
        }

        [TestMethod]
        public void TestContainsPointFalse() //когда элемента нет в таблице
        {
            MyHashTable<Library_10.Instrument> table = new MyHashTable<Library_10.Instrument>(1);
            HandTool tool = new HandTool();
            Assert.IsFalse(table.Contains(tool));
        }

        //тестирование ToString для PointHash
        [TestMethod]
        public void TestToStringPoint() //тестирование ToString для класса узла
        {
            HandTool tool = new HandTool();
            PointHash<Library_10.Instrument> p = new PointHash<Library_10.Instrument>(tool);
            Assert.AreEqual(p.ToString(), tool.ToString());
        }

        [TestMethod]
        public void TestConstructWhithoutParamNext() //конструктор узла без параметров, Next = null
        {
            PointHash<Instrument> p = new PointHash<Instrument>();
            Assert.IsNull(p.Next);
        }

        [TestMethod]
        public void TestConstructWhithoutParamPred() //конструктор узла без параметров, Pred = null
        {
            PointHash<Instrument> p = new PointHash<Instrument>();
            Assert.IsNull(p.Pred);
        }

        //тестирование методов ToString и GetHashCode для класса PointHash
        [TestMethod]
        public void ToString_WhenDataIsNull_ReturnEmptyString() //конструктор без параметров метод ToString
        {
            PointHash<Instrument> point = new PointHash<Library_10.Instrument>();
            string result = point.ToString();
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void ToString_WhenDataIsNotNull_ReturnDataToString()
        {
            Library_10.Instrument tool = new Instrument();
            tool.RandomInit();
            PointHash<Instrument> point = new PointHash<Instrument>(tool);
            string result = point.ToString();
            Assert.AreEqual(tool.ToString(), result);
        }

        [TestMethod]
        public void GetHashCode_WhenDataIsNull_ReturnZero() //тестирование GetHashCode для узла, созданного с помощью конструктора без параметров
        {
            PointHash<Instrument> point = new PointHash<Library_10.Instrument>();
            int result = point.GetHashCode();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetHashCode_WhenDataIsNotNull_ReturnDataHashCode() //тестиование GetHashCode для заполненного узла
        {
            Library_10.Instrument tool = new Instrument();
            tool.RandomInit();
            PointHash<Instrument> point = new PointHash<Library_10.Instrument>(tool);
            int result = point.GetHashCode();
            Assert.AreEqual(tool.GetHashCode(), result);
        }

        [TestMethod]
        public void CopyTo() //тестиование GetHashCode для заполненного узла
        {
            MyCollection<Instrument> col = new MyCollection<Instrument>("w", 6);
            MyCollection<Instrument> col2 = new MyCollection<Instrument>("w", col);

            Assert.AreEqual(col.GetFirstValue().Data, col2.GetFirstValue().Data);
        }

        [TestMethod]
        public void CopyTo_Default() //тестиование GetHashCode для заполненного узла
        {
            MyCollection<Instrument> col = new MyCollection<Instrument>("w", 1);
            Instrument[] arr = new Instrument[3];
            arr[0] = new Instrument("ww", 12);
            arr[1] = new Instrument("ww", 11);
            arr[2] = new Instrument("ww", 10);
            col.CopyTo(arr, 1);
            Assert.AreEqual(col[0], arr[1]);
        }

        [TestMethod]
        public void CopyToCount() //тестиование GetHashCode для заполненного узла
        {
            MyCollection<Instrument> col = new MyCollection<Instrument>("w", 6);
            MyCollection<Instrument> col2 = new MyCollection<Instrument>("w", col);

            Assert.AreEqual(col.Count, col2.Count);
        }

        [TestMethod]
        public void Remove_OneElementInChain() //тестиование GetHashCode для заполненного узла
        {
            MyCollection<HandTool> col = new MyCollection<HandTool>("w", 1);
            PointHash<HandTool> tool = col.GetFirstValue();
            col.Remove(tool.Data);
            HandTool tool1 = new HandTool(82, "Штангенциркуль", "Алюминий");
            HandTool tool2 = new HandTool(12, "Штангенциркуль", "Медь");
            HandTool tool3 = new HandTool(30, "Углометр", "Резина");
            HandTool tool4 = new HandTool(53, "Кусачки", "Конструкционная сталь");

            col.Add(tool1);
            col.Add(tool2);
            col.Add(tool3);
            col.Add(tool4);

            col.Remove(tool1);
            Assert.IsFalse(col.Contains(tool1));
        }

        [TestMethod]
        public void TestIndexGet() //тестирование get индексатора
        {
            MyCollection<HandTool> col = new MyCollection<HandTool>("w", 1);
            HandTool tool = col.GetFirstValue().Data;
            Assert.AreEqual(tool, col[0]);
        }

        [TestMethod]
        public void TestIndexGet_Exception() //тестирование исключение при get индексатора
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            Assert.ThrowsException<Exception>(() =>
            {
                collection.Remove(collection[-1]);
            });
        }

        [TestMethod]
        public void TestIndexSet_Exception() //тестирование исключения в set 
        {
            MyCollection<Instrument> collection = new MyCollection<Instrument>("w", 5); //создаем коллекцию и заполняем ее элементами
            Instrument[] list = new Instrument[5];
            Assert.ThrowsException<Exception>(() =>
            {
                collection[-1] = new Instrument();
            });
        }

        [TestMethod]
        public void TestIndexSet() //тестиование set 
        {
            MyCollection<HandTool> col = new MyCollection<HandTool>("w", 1);
            HandTool tool = col.GetFirstValue().Data;
            col[0] = new HandTool();
            Assert.AreEqual(new HandTool(), col[0]);
        }

        [TestMethod]
        public void IsReadOnly_MyCollection() //тестирование свойства IsReadOnly в MyCollection
        {
            MyCollection<HandTool> col = new MyCollection<HandTool>("w", 1);
            Assert.IsFalse(col.IsReadOnly);
        }

        [TestMethod]
        public void Count_MyCollection() //тестирование кол-ва элементов в MyCollection
        {
            MyCollection<HandTool> col = new MyCollection<HandTool>("w", 6);
            Assert.AreEqual(col.Count, 6);
        }



        //тесты для MyObservableCollection

        [TestMethod]
        public void Test_ConstuctorWithoutParams_ObsCol() //тест проверка на создание пустого объекта MyCollection
        {
            MyObservableCollection<Instrument> collection = new MyObservableCollection<Instrument>();
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void Test_ConstuctorOnlyName_ObsCol() //MyObservableCollection проверка 
        {
            MyObservableCollection<Instrument> collection = new MyObservableCollection<Instrument>("r");
            Assert.AreEqual("r", collection.NameOfCollection);
        }

        [TestMethod]
        public void Test_Constructor_Length_ObsCol() //MyObservableCollection проверка кол-ва элементов
        {
            MyObservableCollection<Instrument> collection = new MyObservableCollection<Instrument>("w", 5); //создаем коллекцию с помощью ввода ее длины
            Assert.AreEqual(collection.Count, 5); //проверяем чтобы коллекция содержала 5 элементов
        }

        [TestMethod]
        public void Test_ConstructorCollection() //констуктор MyObservableCollection
        {
            MyObservableCollection<Instrument> collection = new MyObservableCollection<Instrument>("w", 5); //создаем коллекцию с помощью ввода ее длины
            MyObservableCollection<Instrument> col2 = new MyObservableCollection<Instrument>("e", collection);
            Assert.AreEqual(collection[2], col2[2]);
        }

        //тесты для MyObservableCollection закончились
        //JournalEntry

        [TestMethod]
        public void CHEA_Constructor()         //CollcetionHandlerEventArgs конструктор
        {
            CollectionHandlerEventArgs<Instrument> item = new CollectionHandlerEventArgs<Instrument>("type", new PointHash<Instrument>(), null);
            Assert.AreEqual(item.TypeOfChanges, "type");
            Assert.AreEqual(item.Element.Data, (new PointHash<Instrument>()).Data);
        }

        [TestMethod]
        public void JournalEntry_Constructor()         //JournalEntry конструктор
        {
            MyObservableCollection<Instrument> collection = new MyObservableCollection<Instrument>("w", 5); //создаем коллекцию с помощью ввода ее длины
            CollectionHandlerEventArgs<Instrument> ite = new CollectionHandlerEventArgs<Instrument>("type", new PointHash<Instrument>(), null);
            JournalEntry<Instrument> item = new JournalEntry<Instrument>(collection, ite);
            Assert.AreEqual(item.NameOfCollection, "w");
            Assert.AreEqual(item.TypeOfChanges, ite.TypeOfChanges);
            Assert.AreEqual(item.Element, new PointHash<Instrument>(ite.Element.Data).ToString());
        }

        [TestMethod]
        public void JournalEntry_ToString()         //JournalEntry ToString
        {
            MyObservableCollection<Instrument> collection = new MyObservableCollection<Instrument>("w", 5); //создаем коллекцию с помощью ввода ее длины
            CollectionHandlerEventArgs<Instrument> ite = new CollectionHandlerEventArgs<Instrument>("type", new PointHash<Instrument>(), null);
            JournalEntry<Instrument> item = new JournalEntry<Instrument>(collection, ite);
            Assert.AreEqual(item.ToString(), $"В коллекции w, type элемент {new PointHash<Instrument>(ite.Element.Data).ToString()}");
        }
        //JournalEntry

        //Journal

        [TestMethod]
        public void Journal_Constructor()         //Journal конструктор
        {
            Journal<Instrument> j = new Journal<Instrument>("qq");
            Assert.AreEqual(j.NameOfJournal, "qq");
        }

        [TestMethod]
        public void Journal_CollectionCountChanged_Remove()         //проверка CollectionCountChanged при удалении элемента
        {
            Journal<Instrument> j = new Journal<Instrument>("qq");
            MyObservableCollection<Instrument> collection = new MyObservableCollection<Instrument>("w", 5); //создаем коллекцию с помощью ввода ее длины
            collection.CollectionCountChanged += j.CollectionCountChanged;
            Instrument tool = collection[2];
            collection.Remove(collection[2]);
            Assert.AreEqual(j.GetLastNote(), $"В коллекции w, удален элемент {tool.ToString()}");
        }

        [TestMethod]
        public void Journal_CollectionCountChanged_Add()         //проверка CollectionCountChanged при добавлении элемента
        {
            Journal<Instrument> j = new Journal<Instrument>("qq");
            MyObservableCollection<Instrument> collection = new MyObservableCollection<Instrument>("w", 5); //создаем коллекцию с помощью ввода ее длины
            collection.CollectionCountChanged += j.CollectionCountChanged;
            collection.Add(new Instrument());
            Assert.AreEqual(j.GetLastNote(), $"В коллекции w, добавлен новый элемент {(new Instrument()).ToString()}");
        }

        [TestMethod]
        public void Journal_CollectionReferenceChanged()         //проверка CollectionReferenceChanged при изменении элемента
        {
            Journal<Instrument> j = new Journal<Instrument>("qq");
            MyObservableCollection<Instrument> collection = new MyObservableCollection<Instrument>("w", 5); //создаем коллекцию с помощью ввода ее длины
            collection.CollectionReferenceChanged += j.CollectionReferenceChanged;
            Instrument t = (Instrument)collection[2].Clone();
            Instrument toolNew = new Instrument("ee", 12);
            collection[2] = toolNew;
            Assert.AreEqual($"В коллекции w, элемент {t.ToString()} изменен на {toolNew.ToString()}", j.GetLastNote());
        }

        [TestMethod]
        public void Journal_WriteNotes() //проверка исключения при попытке напечатать пустой журнал
        {
            Journal<Instrument> j = new Journal<Instrument>("q");
            Assert.ThrowsException<Exception>(() =>
            {
                j.WriteNotes();
            });
        }

        //добавленные тесты
        [TestMethod]
        public void MyCollection_IndexOf_Exceptin() //проверка исключения при попытке получить индекс несуществующего в коллекции элемента
        {
            MyCollection<Instrument> col = new MyCollection<Instrument>("q", 2);
            Instrument tool = new Instrument();
            Assert.ThrowsException<Exception>(() =>
            {
                col.IndexOf(tool);
            });
        }

        [TestMethod]
        public void CollectionCountChangedEvent() 
        {
            MyObservableCollection<Instrument> col = new MyObservableCollection<Instrument>("q", 5);
            Instrument tool = new Instrument("TOOL", 1);
            bool Event = false;
            col.Add(tool);
            col.CollectionCountChanged += (source, e) =>
            {
                Event = true;
                Assert.AreEqual("добавлен новый", e.TypeOfChanges);
                Assert.AreEqual(tool, e.Element.Data);
            };
        }

        [TestMethod]
        public void CollectionReferenceChangedEvent()
        {
            MyObservableCollection<Instrument> col = new MyObservableCollection<Instrument>("q", 5);
            Instrument tool = new Instrument("TOOL", 1);
            bool Event = false;
            col[2] = tool;
            col.CollectionReferenceChanged += (source, e) =>
            {
                Event = true;
                Assert.AreEqual("изменен", e.TypeOfChanges);
                Assert.AreEqual(tool, e.Element.Data);
            };
        }

        [TestMethod]
        public void Index_MyOC_Exception() //проверка исключения в индексаторе
        {
            MyCollection<Instrument> col = new MyCollection<Instrument>("q", 5);
            Instrument tool = new Instrument();
            col[1] = tool;
            Assert.ThrowsException<Exception>(() =>
            {
                col[3] = tool;
            });
        }

        [TestMethod]
        public void WriteNotes_WhenEmptyJournal_ThrowsException() //проверка ошибки при попытке печати пустого журнала
        {
            Journal<Instrument> journal = new Journal<Instrument>("Test Journal");
            Assert.ThrowsException<Exception>(() => journal.WriteNotes());
        }

        [TestMethod]
        public void Print_Journal() //метод проверки печати в случае изменения элемента
        {
            Instrument tool = new Instrument();
            Journal<Instrument> journal = new Journal<Instrument>("Test Journal");

            MyObservableCollection<Instrument> myCollection = new MyObservableCollection<Instrument>("q",4); 
            CollectionHandlerEventArgs<Instrument> eventArgs = new CollectionHandlerEventArgs<Instrument>("изменен", new PointHash<Instrument>(tool), new PointHash<Instrument>()); //Искусственне

            journal.CollectionReferenceChanged(myCollection, eventArgs); //Вызываюн метод, который обрабатывает событие в классе Journal

            var consoleOutput = ConsoleOutput(() => { journal.WriteNotes(); });

            Assert.IsTrue(consoleOutput.Contains($"Журнал: Test Journal. В хеш-таблице(-у) q изменен элемент {tool} на ")); //Метод Print Journal, печать журнала с записями
        }

        // Приватний метод CaptureConsoleOutput принимает делегат Action в качестве параметра

        private string ConsoleOutput(Action action) //вспомогательным метод для тестов проверки печати

        {
            // Создается новый StringWriter, который будет использоваться для перехвата вывода консоли
            // StringWriter это обертка над StringBuilder для записи символов в поток строк
            using (var consoleOutput = new StringWriter())
            {
                // Устанавливается consoleOutput как поток вывода консоли, чтобы перехватить вывод этой консоли
                Console.SetOut(consoleOutput);
                // Выполняется переданное действие (action), которое содержит операции вывода информации в консоль
                action.Invoke();
                return consoleOutput.ToString();
            }
        }

        [TestMethod]
        public void Print_Journal_Test2() //тестирование вывода на печать сообзения о добавлении
        {
            Instrument tool = new Instrument();
            Journal<Instrument> journal = new Journal<Instrument>("Test Journal");

            MyObservableCollection<Instrument> myCollection = new MyObservableCollection<Instrument>("q", 4);
            CollectionHandlerEventArgs<Instrument> eventArgs = new CollectionHandlerEventArgs<Instrument>("добавлен", new PointHash<Instrument>(tool), null); //Искусственне

            journal.CollectionCountChanged(myCollection, eventArgs); //Вызываюн метод, который обрабатывает событие в классе Journal

            var consoleOutput = ConsoleOutput(() => { journal.WriteNotes(); });

            Assert.IsTrue(consoleOutput.Contains($"Журнал: Test Journal. В хеш-таблице(-у) q добавлен элемент {tool}")); //Метод Print Journal, печать журнала с записями
        }
    }
}
