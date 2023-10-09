using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTraining
{
    class Program
    {
        private static Random rand = new Random();
        private static List<string> Names = new List<string>();
        private static List<string> AS_W = new List<string>();
        private static List<string> M_G = new List<string>();
        private static List<string> A_T = new List<string>();

        private static int company = 1;
        private static int newCompany = 10;

        static void SetNames()
        {
            StreamReader names = new StreamReader("first-names.txt", Encoding.Default);
            StreamReader as_w = new StreamReader("assault_weapons.txt", Encoding.Default);
            StreamReader m_g = new StreamReader("machine_guns.txt", Encoding.Default);
            StreamReader a_t = new StreamReader("anti_tank.txt", Encoding.Default);

            Set(names, Names);
            Set(as_w, AS_W);
            Set(m_g, M_G);
            Set(a_t, A_T);
        }

        static void Set(StreamReader sr, List<string> list)
        {
            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                list.Add(line);
            }
        }
        static int Rand(int min, int max)
        {
            int random = rand.Next(min, max);
            return random;
        }
        static void Main(string[] args)
        {
            SetNames();
            Soldiers sol = new Soldiers();

            while(true)
            {
                //Начала набора данных
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Write a Command");
                string command = Console.ReadLine();

                switch(command)
                {
                    case "STOP":
                        break;

                    case "ALL":
                        //Вывод нужной армии
                        bool enemyarmy = false;

                        Console.WriteLine("Какой армии O/E");
                        string En = Console.ReadLine();

                        if (En.Contains("O")) enemyarmy = false;
                        else if (En.Contains("E")) enemyarmy = true;

                        Soldiers.InfoAll(enemyarmy);
                        continue;

                    case "ADD":
                        //Число солдат и проверка на минус
                        Console.WriteLine("Количество Солдат");
                        int countOfSoldiers = int.Parse(Console.ReadLine());
                        if (countOfSoldiers < 0) break;

                        //Распределение армий
                        bool enemy = false;

                        Console.WriteLine("Какой армии O/E");
                        string E = Console.ReadLine();

                        if (E.Contains("O")) enemy = false;
                        else if (E.Contains("E")) enemy = true;

                        //Массив где будут записываться имена
                        string[] names = new string[countOfSoldiers];

                        //Добавление в names рандомных имён из файла с именами
                        for(int i = 0; i < countOfSoldiers; i++)
                        {
                            int r = rand.Next(0, Names.Count);
                            names[i] = Names[r];
                        }

                        //Создание солдат и вручение им Вооружения
                        for(int j = 0; j < countOfSoldiers; j++)
                        {
                            if(j > newCompany)
                            {
                                company++;
                                newCompany += 10;
                            }

                            if(!enemy) soldiers(enemy, names[j], "Private", 10, 100, company, AS_W[Rand(0, AS_W.Count)], 10);
                            else soldiers(enemy, names[j], "Private", 10, 100, company, AS_W[Rand(0, AS_W.Count)], 10);
                        }

                        //Присвоение оружия у Зваинй

                        //Гранатамётчик
                        List<Soldiers> S = new List<Soldiers>();
                        for(int i = 5; i < countOfSoldiers; i+=5)
                        {
                            if (!enemy) sol.Upgrade(Soldiers.soldiers, i, 100, 30, A_T[Rand(0, A_T.Count)]);
                            else sol.Upgrade(Soldiers.enemySoldiers, i, 100, 30, A_T[Rand(0, A_T.Count)]);
                        }
                        
                        //Пулемётчик
                        for (int i = 6; i < countOfSoldiers; i += 6)
                        {
                            if (!enemy) sol.Upgrade(Soldiers.soldiers, i, 100, 20, M_G[Rand(0, M_G.Count)]);
                            else sol.Upgrade(Soldiers.enemySoldiers, i, 100, 20, M_G[Rand(0, M_G.Count)]);
                        }

                        //Сержант
                        for (int i = 10; i < countOfSoldiers; i += 10)
                        {
                            if (!enemy) sol.Upgrade(Soldiers.soldiers, i, 150, 20, M_G[Rand(0, M_G.Count)], "Sergant");
                            else sol.Upgrade(Soldiers.enemySoldiers, i, 150, 20, M_G[Rand(0, M_G.Count)], "Sergant");
                        }

                        //Лейтенант
                        for (int i = 23; i < countOfSoldiers; i += 25)
                        {
                            if (!enemy) sol.Upgrade(Soldiers.soldiers, i, 200, 20, M_G[Rand(0, M_G.Count)], "Leutenant");
                            else sol.Upgrade(Soldiers.enemySoldiers, i, 200, 20, M_G[Rand(0, M_G.Count)], "Leutenant");
                        }

                        //Капитан
                        for (int i = 47; i < countOfSoldiers; i += 50)
                        {
                            if (!enemy) sol.Upgrade(Soldiers.soldiers, i, 300, 20, M_G[Rand(0, M_G.Count)], "Capitan");
                            else sol.Upgrade(Soldiers.enemySoldiers, i, 300, 20, M_G[Rand(0, M_G.Count)], "Capitan");
                        }

                        Console.WriteLine($"Added {countOfSoldiers} Soldiers");

                        continue;

                    case "SHOT":

                        int b = 0;
                        while(b < Soldiers.soldiers.Count)
                        {
                            //Проверка на количество вражеских солдат
                            if (Soldiers.enemySoldiers.Count <= 0) break;

                            //Входит если b меньше Количества Солдат, то есть своим ходом
                            if (b < Soldiers.enemySoldiers.Count)
                            {
                                int randomShot = rand.Next(1, 100);
                                Soldiers.soldiers[b].Shoot(Soldiers.enemySoldiers[b], randomShot);
                                b++;
                            }
                            //А тут если b уже больше количества врагов, но союзные войска остались, им же тоже нужно стрелять
                            //а то всё провернётся и в конце будет 1 на 1
                            else if(b >= Soldiers.enemySoldiers.Count)
                            {
                                int randomShot = rand.Next(1, 100);
                                Soldiers.soldiers[b].Shoot(Soldiers.enemySoldiers[rand.Next(0, Soldiers.enemySoldiers.Count)], randomShot);
                                b++;
                            }
                            else break;
                        }

                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("==============================");

                        int k = 0;
                        while (k < Soldiers.enemySoldiers.Count)
                        {
                            //Проверка на количество вражеских солдат
                            if (Soldiers.soldiers.Count <= 0) break;

                            //Входит если b меньше Количества Солдат, то есть своим ходом
                            if (k < Soldiers.soldiers.Count)
                            {
                                int randomShot = rand.Next(1, 100);
                                Soldiers.enemySoldiers[k].Shoot(Soldiers.soldiers[k], randomShot);
                                k++;
                            }
                            //А тут если b уже больше количества врагов, но союзные войска остались, им же тоже нужно стрелять
                            //а то всё провернётся и в конце будет 1 на 1
                            else if (k >= Soldiers.soldiers.Count)
                            {
                                int randomShot = rand.Next(1, 100);
                                Soldiers.enemySoldiers[k].Shoot(Soldiers.soldiers[rand.Next(0, Soldiers.soldiers.Count)], randomShot);
                                k++;
                            }
                            else break;
                        }

                        continue;

                    case "DEATHS":
                        foreach(Soldiers dS in Soldiers.diedSoldiers)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{dS.Rank} {dS.Name}, Our");
                        }
                        foreach (Soldiers dES in Soldiers.diedEnemySoldiers)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{dES.Rank} {dES.Name}, Enemy");
                        }
                        continue;

                }
            }

            Console.ReadLine();
        }

        static void soldiers(bool enemy, string name, string rank, int exp, int hp, int company, string weapon, int damage)
        {
            //Добавление нового солдата в лист
            Soldiers soldiers = new Soldiers();
            soldiers.AddSoldiers(enemy, name, rank, exp, hp, company, weapon, damage);
        }



        //ОСТАЛЬНОЕ

        static void Numbers(params int[] numbers)
        {
            foreach(int num in numbers)
            {
                Console.WriteLine(num);
            }
        }

        static void GetDamage()
        {
            Character character = new Character();

            Console.WriteLine("How to get damage?");
            int damage = int.Parse(Console.ReadLine());

            character.GetDamage(damage, out string result);
            Console.WriteLine(result);
        }

        static void RomNumbers(string Rom)
        {
            string[] rom = { "I", "V", "X", "L", "C", "D", "M" };
            int[] numbers = { 1, 5, 10, 50, 100, 500, 1000 };
        }

        static void Dict()
        {
            var peoples = new Dictionary<int, string>();

            while(true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("1 - Add People (Add) \n2 - Remove \n3 - View all keys (Keys) \n4 - View all Peoples (Peoples) \n5 - View all (All)");
                Console.ForegroundColor = ConsoleColor.White;
                string question = Console.ReadLine();

                if (question == "0") break;

                switch(question)
                {
                    case "1":
                    case "Add":
                        Console.WriteLine("Write a key");
                        int keyToAdd = int.Parse(Console.ReadLine());
                        Console.WriteLine("Write a name of people");
                        string name = Console.ReadLine();

                        peoples.Add(keyToAdd, name);

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Added {keyToAdd}:{name}");

                        break;

                    case "2":
                    case "Remove":
                        Console.WriteLine("Write a Key to remove");
                        int keyToRemove = int.Parse(Console.ReadLine());

                        peoples.Remove(keyToRemove);

                        break;

                    case "3":
                    case "Keys":
                        Dictionary<int, string>.KeyCollection peoplesKeys = peoples.Keys;

                        foreach(var item in peoplesKeys)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Key {item} | ");
                        }

                        break;

                    case "4":
                    case "Peoples":
                        Dictionary<int, string>.ValueCollection peoplesValue = peoples.Values;

                        foreach (var item in peoplesValue)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Key {item} | ");
                        }

                        break;

                    case "5":
                    case "All":

                        foreach(var pair in peoples)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Key: {pair.Key} | Value: {pair.Value}");
                        }

                        break;
                }


            }
        }

        static void Auth(string login, string password, int authCount)
        {
            while(authCount > 0)
            {
                string _login = Console.ReadLine();
                string _pass  = Console.ReadLine();

                if(_login.Equals(login) && _pass.Equals(password))
                {
                    Console.WriteLine("Авторизация Успешна");
                    break;
                }

                authCount--;
            }

            if (authCount == 0) Console.WriteLine("Ошибка");
        }

        static void Factorial(int number)
        {
            int num = number-1;
            while(num > 1)
            {
                number *= num;
                num--;
            }

            Console.WriteLine($"======Number = {number}");
        }

        static void GetAverage(int count)
        {
            int[] array = new int[count];

            int inputCount = 0;
            while(inputCount < count)
            {
                int input = int.Parse(Console.ReadLine());
                array[inputCount] = input;

                if (input == 0) break;
                inputCount++;
            }

            int sum = 0;
            int numsCount = 0;

            foreach (int num in array)
            {
                if(num > 0 && num % 3 == 0)
                {
                    sum += num;
                    numsCount++;
                }
            }
            double average = (double)sum / numsCount;
            Console.WriteLine(average);
        }

        static void Fibonnachi(int[] array, int count)
        {
            int number;
            int first  = 0;
            int second = 1;
            array[0] = first;
            array[1] = second;

            for(int i = 0; i < count; i++)
            {
                number = first + second;
                array[i] = number;

                first = second;
                second = number;
            }

            foreach(int num in array)
            {
                Console.WriteLine(num);
            }
        }

        static void GetTwoNumbers(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for(int j = i+1; j < array.Length; j++)
                {
                    int atI = array[i];
                    int atJ = array[j];

                    if(atI + atJ == 0)
                        Console.WriteLine($"NUMBERS: |{atI}:{atJ}| INDEX: |{i}:{j}|");
                }
            }
        }

        static void GetThreeNumbers(int[] array)
        {
            for (int i = 0; i < array.Length - 2; i++)
            {
                for (int j = i + 1; j < array.Length - 1; j++)
                {
                    for (int k = j + 1; k < array.Length; k++)
                    {
                        int atI = array[i];
                        int atJ = array[j];
                        int atK = array[k];

                        if (atI + atJ + atK == 0)
                            Console.WriteLine($"Numbers: |{atI}:{atJ}:{atK}| Index: |{i}:{j}:{k}|");
                    }
                }
            }
        }

        static void Profile()
        {
            Console.WriteLine("Write your name");
            string name = Console.ReadLine();

            Console.WriteLine("Write your Last Name");
            string lastName = Console.ReadLine();

            Console.WriteLine("Write your age");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("Write your weight (KG)");
            float weight = float.Parse(Console.ReadLine());

            Console.WriteLine("Write your height (M)");
            float height = float.Parse(Console.ReadLine());

            float BMI = weight / (height * height);

            string profile = $"Profile {Environment.NewLine}"
                + $"First, Last Name: {name} {lastName} {Environment.NewLine}"
                + $"Age: {age} {Environment.NewLine}"
                + $"Weight: {weight} {Environment.NewLine}"
                + $"Height: {height} {Environment.NewLine}"
                + $"Body Mass Index {BMI} {Environment.NewLine}";

            Console.WriteLine(profile);
        }

        static void GetTriangle(float a, float b, float c)
        {
            double p = (a + b + c) / 2;

            double d = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            Console.WriteLine($"Периметр Треугольника равен - {d}");
        }

        static void GetNums(int a, int b)
        {
            Console.WriteLine($"First: {a} | Second: {b}");

            int c = a;
            a = b;
            b = c;

            Console.WriteLine($"First: {a} | Second: {b}");
        }

        static void GetNumbersLength(int num)
        {
            string nums = num.ToString();
            Console.WriteLine(nums.Length);
        }
    }
}
