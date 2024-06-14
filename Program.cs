using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите задачу для выполнения:");
            Console.WriteLine("1. Вывод элементов коллекции через поток");
            Console.WriteLine("2. Игра 'успел, не успел'");
            Console.WriteLine("3. Вывод времени с использованием таймера");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Некорректный ввод. Введите число от 1 до 3:");
            }

            switch (choice)
            {
                case 1:
                    PrintCollectionItems();
                    break;
                case 2:
                    PlayReactionGame();
                    break;
                case 3:
                    PrintTimeUsingTimer();
                    break;
            }
        }
        static void PrintCollectionItems()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            Thread thread = new Thread(() =>
            {
                foreach (var number in numbers)
                {
                    string result = number.ToString();
                    Console.WriteLine(result);
                    Thread.Sleep(1000);
                }
            });

            thread.Start();
            thread.Join();
        }

        static void PlayReactionGame()
        {
            Random random = new Random();
            char[] symbols = { 'a', 'b', 'c', 'd', 'e' };

            Console.WriteLine("Игра 'успел, не успел' начинается!");

            while (true)
            {
                char symbol = symbols[random.Next(symbols.Length)];

                Console.WriteLine($"Нажмите клавишу: '{symbol}'");

                DateTime startTime = DateTime.Now;
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                DateTime endTime = DateTime.Now;

                if (keyInfo.KeyChar == symbol)
                {
                    TimeSpan reactionTime = endTime - startTime;
                    Console.WriteLine($"\nВаше время реакции: {reactionTime:mm\\:ss\\.fff}");
                }
                else
                {
                    Console.WriteLine("\nВы нажали неверную клавишу. Попробуйте снова!");
                }

                Console.WriteLine("Для продолжения нажмите Enter, для выхода нажмите Esc.");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    break;

                Console.Clear();
            }
        }

        static void PrintTimeUsingTimer()
        {
            TimerCallback timerCallback = new TimerCallback(PrintTime);
            Timer timer = new Timer(timerCallback, null, 3000, 1000); 

            Console.WriteLine("Нажмите любую клавишу для выхода.");
            Console.ReadKey();

            timer.Dispose(); 
        }

        static void PrintTime(object state)
        {
            DateTime currentTime = DateTime.Now;
            Console.WriteLine($"Текущее время: {currentTime:T}");
        }
    }
}
