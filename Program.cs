using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexeso
{
    class Program
    {
        static HerniObrazovka herniObrazovka;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Vítejte v Pexesu!");
            Console.WriteLine("Stiskněte \"a\" pro standartní hru");
            Console.WriteLine("Stiskněte \"b\" pro vlastní hru");
            Console.WriteLine("Stiskněte cokoliv jiného pro ukončení");
            Console.ResetColor();
            ConsoleKeyInfo Klavesa = Console.ReadKey(true);
            switch(Klavesa.Key)
            {
                case ConsoleKey.A:
                    herniObrazovka = new HerniObrazovka();
                    break;
                case ConsoleKey.B:
                    Int16 zadej = 1;
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Zadejte prosím velikost herní plochy: ");
                        try
                        {
                            zadej = Convert.ToInt16(Console.ReadLine());
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Zadejte prosím číslo");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        finally
                        {
                            if (zadej > 10 || zadej < 2 || zadej % 2 != 0)
                            {
                                zadej = 1;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Pozor, číslo nesmí být větší než 10, menší než 2 a musí být sudé!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }

                    }
                    while (zadej % 2 != 0);
                    char rub = '#';
                    bool ex = true;
                    do
                    {
                        Console.WriteLine("Zadejte prosím znak pro rub");
                        try
                        {
                            rub = Convert.ToChar(Console.ReadLine());
                            ex = false;
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Toto není validní znak!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    } while (ex);
                    Console.ResetColor();
                    herniObrazovka = new HerniObrazovka(zadej, rub);
                    break;
                default:
                    return;
            }
            while(!herniObrazovka.VseOdhaleno())
            {
                herniObrazovka.VykresliPlochu();
                int x = -1, y = -1;
                do
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Zadejte souřadnice první hrací karty, každou na jeden řádek");
                    try
                    {
                        x = int.Parse(Console.ReadLine());
                        y = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Prosím zadávejte jen čísla!");
                        Console.ResetColor();
                    }
                    if (!herniObrazovka.ValidniKarta(x, y))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Chybně zadané souřadnice. Prosím zvolte neodkrytou kartu.");
                        Console.ResetColor();
                    }
                }
                while (!herniObrazovka.ValidniKarta(x,y));
                Console.ResetColor();
                herniObrazovka.VseOdhaleno();
                herniObrazovka.OtocPrvniKartu(x, y);
                x = -1;
                y = -1;
                do
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Zadejte souřadnice druhé hrací karty, každou na jeden řádek");
                    try
                    {
                        x = int.Parse(Console.ReadLine());
                        y = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Prosím zadávejte jen čísla!");
                        Console.ResetColor();
                    }
                    if (!herniObrazovka.ValidniKarta(x, y))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Chybně zadané souřadnice. Prosím zvolte neodkrytou kartu.");
                        Console.ResetColor();
                    }
                }
                while (!herniObrazovka.ValidniKarta(x, y));
                Console.ResetColor();
                herniObrazovka.OtocDruhouKartu(x,y);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Gratuluji, vyhrál jsi!");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
