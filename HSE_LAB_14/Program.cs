using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HSE_LAB_14
{
    public class Program
    {
        private const uint UINT_MIN_VALUE = 1;
        private const uint UINT_MAX_VALUE = 100;
        private const int INT_MIN_VALUE = 0;
        private const int INT_MAX_VALUE = 7;
        private static List<List<Animals>> list = new List<List<Animals>>();

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в приложение по работе с LINQ запросами");
            while (true)
            {
                Console.WriteLine("\nМеню приложения:");
                Console.WriteLine("1 - Создание коллекции с коллекциями");
                Console.WriteLine("2 - Печать коллекции");
                Console.WriteLine("3 - Выборка данных");
                Console.WriteLine("4 - Получение счетчика");
                Console.WriteLine("5 - Использование операций над множествами");
                Console.WriteLine("6 - Агрегирование данных");
                Console.WriteLine("7 - Группировка данных");
                Console.WriteLine("0 - Выход из приложения");

                int choice = InputInt();

                if (choice == 0)
                {
                    Console.WriteLine("\n0 - Выход из приложения");
                    Console.WriteLine("Завершение работы в приложении по работе с LINQ запросами");
                    break;
                }

                switch (choice)
                {
                    case 1:
                        CreateCollection();
                        break;
                    case 2:
                        PrintCollection();
                        break;
                    case 3:
                        DataSampling();
                        break;
                    case 4:
                        GetCounter();
                        break;
                    case 5:
                        OperationsOnSets();
                        break;
                    case 6:
                        DataAggregation();
                        break;
                    case 7:
                        DataGrouping();
                        break;
                }
            }
        }

        private static void CreateCollection()
        {
            Console.WriteLine("\n1 - Создание коллекции с коллекциями");

            Console.WriteLine("Введите размер коллекции для ее создания");
            uint size = InputUint();

            list = CreateList(size);
            Console.WriteLine("Коллекция успешно создана");

            Console.WriteLine("\nСоздание коллекции с коллекциями завершено");
        }

        public static List<List<Animals>> CreateList(uint size)
        {

            var list = new List<List<Animals>>();
            Animals animal = new Animals();

            for (int i = 0; i < 4; i++)
            {
                List<Animals> newList = new List<Animals>();

                for (int j = 0; j < size; j++)
                    newList.Add((Animals)animal.CreateObjectAnimalsRandom().Clone());

                list.Add(new List<Animals>(newList));
            }
            return list;
        }

        public static void PrintCollection()
        {
            Console.WriteLine("\n2 - Печать коллекции");
            if (list.Count == 0)
            {
                Console.WriteLine("Коллекция пустая");
                return;
            }

            int order = 1;
            list.ForEach((l) =>
            {
                Console.WriteLine($"\nКоллекция {order}:");
                l.ForEach(e => Console.WriteLine(e));
                order++;
            });
            Console.WriteLine("\nПечать коллекции успешно завершена");
        }

        private static void DataSampling()
        {
            Console.WriteLine("\n3 - Выборка данных");

            if (list.Count == 0)
            {
                Console.WriteLine("Коллекция пустая");
                Console.WriteLine("Выборка данных не завершена");
                return;
            }

            Console.WriteLine("Введите имя для поиска элементов коллекции");
            string nameSearch = InputString();

            var animalsSearch1 = Query.SearchAnimals(list, nameSearch);
            var animalsSearch2 = Methods.SearchAnimals(list, nameSearch);

            Console.WriteLine("\nНайдены следующие элементы:");
            if (animalsSearch1.Count != 0)
                animalsSearch1.ForEach(a => Console.WriteLine(a));
            else
                Console.WriteLine("Элементы не найдены");

            Console.WriteLine("\nНайдены следующие элементы:");
            if (animalsSearch2.Count != 0)
                animalsSearch2.ForEach(a => Console.WriteLine(a));
            else
                Console.WriteLine("Элементы не найдены");

            Console.WriteLine("\nВыборка данных успешно завершена");
        }

        private static void GetCounter()
        {
            Console.WriteLine("\n4 - Получение счетчика");

            if (list.Count == 0)
            {
                Console.WriteLine("Коллекции пусты");
                Console.WriteLine("Получение счетчика не завершено");
                return;
            }

            Console.WriteLine("Введите имя для подсчета элементов коллекции");
            string nameCount = InputString();

            var animalsCount1 = Query.CountAnimals(list, nameCount);
            var animalsCount2 = Methods.CountAnimals(list, nameCount);

            Console.WriteLine($"\nКол-во найденных элементов: {animalsCount1}");
            Console.WriteLine($"Кол-во найденных элементов: {animalsCount2}");

            Console.WriteLine("\nПолучение счетчика успешно завершено");
        }        

        private static void OperationsOnSets()
        {
            Console.WriteLine("\n5 - Использование операций над множествами");

            if (list.Count == 0)
            {
                Console.WriteLine("Коллекции пусты");
                Console.WriteLine("Использование операций над множествами не завершено");
                return;
            }

            Console.WriteLine("Найдем имена животных, которые есть в 1 коллекции, но нет во 2 коллекции");
            var animalsExpect1 = Query.ExpectAnimals(list);
            var animalsExpect2 = Methods.ExpectAnimals(list);

            Console.WriteLine("\nИмена, которые есть в 1 коллекции, но нет во 2 коллекции:");
            if (animalsExpect1.Count != 0)
                animalsExpect1.ForEach(a => Console.WriteLine(a));
            else
                Console.WriteLine("Элементы не найдены");

            Console.WriteLine("\nИмена, которые есть в 1 коллекции, но нет во 2 коллекции:");
            if (animalsExpect2.Count != 0)
                animalsExpect2.ForEach(a => Console.WriteLine(a));
            else
                Console.WriteLine("Элементы не найдены");

            Console.WriteLine("\nТеперь объединим 1 коллекцию со 2 коллекцией");

            var animalsUnion1 = Query.UnionAnimals(list);
            var animalsUnion2 = Methods.UnionAnimals(list);

            Console.WriteLine("\nОбъединение 1 коллекции и 2 коллекции:");
            if (animalsUnion1.Count != 0)
                animalsUnion1.ForEach(a => Console.WriteLine(a));
            else
                Console.WriteLine("Коллекции пусты");

            Console.WriteLine("\nОбъединение коллекции 1 и коллекции 2:");
            if (animalsUnion2.Count != 0)
                animalsUnion2.ForEach(a => Console.WriteLine(a));
            else
                Console.WriteLine("Коллекции пусты");

            Console.WriteLine("\nА сейчас найдем имена, которые присутствуют в обоих коллекциях");

            var animalsIntersect1 = Query.IntersectAnimals(list);
            var animalsIntersect2 = Methods.IntersectAnimals(list);

            Console.WriteLine("\nИмена, которые есть в обоих коллекциях:");
            if (animalsIntersect1.Count != 0)
                animalsIntersect1.ForEach(a => Console.WriteLine(a));
            else
                Console.WriteLine("Элементы не найдены");

            Console.WriteLine("\nИмена, которые есть есть в обоих коллекциях:");
            if (animalsIntersect2.Count != 0)
                animalsIntersect2.ForEach(a => Console.WriteLine(a));
            else
                Console.WriteLine("Элементы не найдены");

            Console.WriteLine("\nИспользование операций над множествами успешно завершено");
        }

        private static void DataAggregation()
        {
            Console.WriteLine("\n6 - Агрегирование данных");

            if (list.Count == 0)
            {
                Console.WriteLine("Коллекции пусты");
                Console.WriteLine("Агрегирование данных не завершено");
                return;
            }

            Console.WriteLine("Найдем средний вес элементов коллекции");
            Console.WriteLine("Если средний вес равен 0, значит элементов нет в коллекции");

            var averageWeight1 = Query.AverageWeight(list);
            var averageWeight2 = Methods.AverageWeight(list);

            Console.WriteLine($"Средний вес элементов коллекции равен: {averageWeight1}");
            Console.WriteLine($"Средний вес элементов коллекции равен: {averageWeight2}");

            Console.WriteLine("\nАгрегирование данных успешно завершено");
        }

        private static void DataGrouping()
        {
            Console.WriteLine("\n7 - Группировка данных");

            if (list.Count == 0)
            {
                Console.WriteLine("Коллекции пусты");
                Console.WriteLine("Группировка данных не завершена");
                return;
            }

            var animalsGroup1 = Query.GroupAnimals(list);
            var animalsGroup2 = Methods.GroupAnimals(list);

            if (animalsGroup1.Count != 0)
            {
                Console.WriteLine("Сгруппированные коллекции по имени:");

                animalsGroup1.ForEach(animals =>
                {
                    Console.WriteLine(animals.Key);
                    animals.ToList().ForEach(e => Console.WriteLine(e));
                    Console.WriteLine();
                });
            }
            else
            {
                Console.WriteLine("Список пуст");
            }

            if (animalsGroup2.Count != 0)
            {
                Console.WriteLine("Сгруппированные коллекции по имени:");

                animalsGroup2.ForEach(animals =>
                {
                    Console.WriteLine(animals.Key);
                    animals.ToList().ForEach(e => Console.WriteLine(e));
                    Console.WriteLine();
                });
            }
            else
            {
                Console.WriteLine("Список пуст");
            }

            Console.WriteLine("\nГруппировка данных успешно завершена");
        }

        public static class Query
        {
            public static List<Animals> SearchAnimals(List<List<Animals>> list, string name)
            {
                return (from animal in GeneralList(list) where animal.Name == name select animal).ToList();
            }

            public static int CountAnimals(List<List<Animals>> list, string name)
            {
                return (from animal in GeneralList(list) where animal.Name == name select animal).Count();
            }

            public static List<string> ExpectAnimals(List<List<Animals>> list)
            {
                return list.Count == 0 ? default : (from animal in list[0] select animal.Name).Except(from animal in list[1] select animal.Name).ToList();
            }

            public static List<Animals> UnionAnimals(List<List<Animals>> list)
            {
                return list.Count == 0 ? default : (from animal in list[0] select animal).Union(from animal in list[1] select animal).ToList();
            }

            public static List<string> IntersectAnimals(List<List<Animals>> list)
            {
                return list.Count == 0 ? default : (from animal in list[0] select animal.Name).Intersect(from animal in list[1] select animal.Name).ToList();
            }

            public static double AverageWeight(List<List<Animals>> list)
            {
                return list.Count == 0 ? 0 : (from animal in GeneralList(list) select animal.Weight).Average();
            }

            public static List<IGrouping<string, Animals>> GroupAnimals(List<List<Animals>> list)
            {
                return (from animal in GeneralList(list) group animal by animal.Name).ToList();
            }
        }

        public static class Methods
        {
            public static List<Animals> SearchAnimals(List<List<Animals>> list, string name)
            {
                return GeneralList(list).Where(animal => animal.Name == name).ToList();
            }

            public static int CountAnimals(List<List<Animals>> list, string name)
            {
                return GeneralList(list).Count(animal => animal.Name == name);
            }

            public static List<string> ExpectAnimals(List<List<Animals>> list)
            {
                return list.Count == 0 ? default : list[0].Select(animal => animal.Name).Except(list[1].Select(animal => animal.Name)).ToList();
            }

            public static List<Animals> UnionAnimals(List<List<Animals>> list)
            {
                return list.Count == 0 ? default : list[0].Union(list[1]).ToList();
            }

            public static List<string> IntersectAnimals(List<List<Animals>> list)
            {
                return list.Count == 0 ? default : list[0].Select(animal => animal.Name).Intersect(list[1].Select(animal => animal.Name)).ToList();
            }

            public static double AverageWeight(List<List<Animals>> list)
            {
                return list.Count == 0 ? 0 : GeneralList(list).Average(animal => animal.Weight);
            }

            public static List<IGrouping<string, Animals>> GroupAnimals(List<List<Animals>> list)
            {
                return GeneralList(list).GroupBy(animal => animal.Name).ToList();
            }
        }

        public static List<Animals> GeneralList(List<List<Animals>> list)
        {
            return list.Aggregate(new List<Animals>(), (newList, el) => newList.Union(el).ToList());
        }

        private static int InputInt()
        {
            int number;
            bool check;
            do
            {
                Console.Write("Ввод: ");
                check = int.TryParse(Console.ReadLine(), out number) && number >= INT_MIN_VALUE && number <= INT_MAX_VALUE;
                if (!check) Console.WriteLine("Ошибка! Введите целое число пределах от {0} до {1} (включительно)", INT_MIN_VALUE, INT_MAX_VALUE);
            } while (!check);
            return number;
        }

        private static uint InputUint()
        {
            uint number;
            bool check;
            do
            {
                Console.Write("Ввод: ");
                check = uint.TryParse(Console.ReadLine(), out number) && number >= UINT_MIN_VALUE && number <= UINT_MAX_VALUE;
                if (!check) Console.WriteLine("Ошибка! Введите целое число пределах от {0} до {1} (включительно)", UINT_MIN_VALUE, UINT_MAX_VALUE);
            } while (!check);

            return number;
        }

        private static string InputString()
        {
            string input;
            bool check;
            do
            {
                Console.Write("Ввод: ");
                input = Console.ReadLine();
                check = input.Trim() != "";
                if (!check) Console.WriteLine("Ошибка! Введите не пустые данные");
            } while (!check);
            return input;
        }
    }
}
