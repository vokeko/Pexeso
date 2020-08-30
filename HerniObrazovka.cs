using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Pexeso
{
    class HerniObrazovka
    {
        private int Rozmer;
        //Je deklarovaná informacemi z hlavního programu
        private int OdhaleneKarty = 0, PocitadloTahu = 0;
        private char[,] HerniPole;
        private StavKarty[,] PoleStavu;
        //enumy mohou být pole!
        private char RubKarty;
        private Pozice OtocenaKarta;
        private char LicKarty = 'A';
        public HerniObrazovka(Int16 _rozmer=4, char _rubkarty='#')
        {
            RubKarty = _rubkarty;
            Rozmer = _rozmer;
            HerniPole = new char[Rozmer, Rozmer];
            PoleStavu = new StavKarty[Rozmer, Rozmer];
            RozdejKarty();
        }
        private void RozdejKarty()
        {
            ArrayList volnePozice = new ArrayList();
            for (int y = 0; y < Rozmer; y++)
            {
                for (int x = 0; x < Rozmer; x++)
                {
                    Pozice pozice = new Pozice(x, y);
                    //když vytvářím pozici, tak se spustí konstruktor, ve kterém se jí přiřadí tato čísla - x a y dle loopu
                    volnePozice.Add(pozice);
                    PoleStavu[x, y] = StavKarty.Skryta;
                }
            }
            while (volnePozice.Count >= 2)
            {
                Random generatorCisel = new Random();
                int cislo = generatorCisel.Next(volnePozice.Count);
                Pozice prvniKarta = (Pozice)volnePozice[cislo];
                volnePozice.Remove(prvniKarta);
                //dokud zbývají nepřiřazené dvojice, tak se vygeneruje náhodné číslo, které náhodně vybere jednu kartu. Stejně tak s druhou kartou.
                cislo = generatorCisel.Next(volnePozice.Count);
                Pozice druhaKarta = (Pozice)volnePozice[cislo];
                volnePozice.Remove(druhaKarta);

                HerniPole[prvniKarta.X, prvniKarta.Y] = LicKarty;
                HerniPole[druhaKarta.X, druhaKarta.Y] = LicKarty;
                //
                LicKarty++;
                //toto změní "A" na "B" atd. Prostě se vymění znak za jinou dvojici znaků.
            }
        }
        public void VykresliPlochu()
        {
            Console.Clear();

            for (int y = 0; y < Rozmer; y++)
            {
                for (int x = 0; x < Rozmer; x++)
                {
                    switch(PoleStavu[x,y])
                    {
                        case StavKarty.Skryta:
                            Console.Write(RubKarty);
                            break;
                        case StavKarty.Otocena:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(HerniPole[x, y]);
                            Console.ResetColor();
                            break;
                        case StavKarty.Odstranena:
                            Console.Write(" ");
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Tahy: " + PocitadloTahu);
            Console.ResetColor();
            //vykreslí se postupně všechny prvky herního pole
        }
        public bool VseOdhaleno()
        {
            return OdhaleneKarty == Rozmer * Rozmer;
            //jednoduchá funkce, která kontroluje, jestli byly všechny karty odhaleny
        }
        public bool ValidniKarta(int x, int y)
        {
            return (x >= 0 && x < Rozmer) && (y >= 0 && y < Rozmer) && (PoleStavu[x, y] == StavKarty.Skryta);
            //vrátí se true pokud - karta nemá větší rozměry x a y než daný rozměr a když je karta skrytá
        }
        public void OtocPrvniKartu(int x, int y)
        {
            //Spustí když správně zadáme souřadnice pro otočení první karty.
            OtocenaKarta = new Pozice(x, y);
            PoleStavu[x, y] = StavKarty.Otocena;
            PocitadloTahu++;
            VykresliPlochu();
        }
        public void OtocDruhouKartu(int x, int y)
        {
            //Spustí když správně zadáme souřadnice pro otočení druhé karty.
            PoleStavu[x, y] = StavKarty.Otocena;
            PocitadloTahu++;
            VykresliPlochu();
            Console.ReadKey(true);
            if (HerniPole[OtocenaKarta.X, OtocenaKarta.Y] == HerniPole[x, y])
            {
                //kontrola, jestli jsou obě karty stejné a jejich následné odstranění
                PoleStavu[OtocenaKarta.X, OtocenaKarta.Y] = StavKarty.Odstranena;
                PoleStavu[x, y] = StavKarty.Odstranena;
                OdhaleneKarty += 2;
            }
            else
            {
                PoleStavu[OtocenaKarta.X, OtocenaKarta.Y] = StavKarty.Skryta;
                PoleStavu[x, y] = StavKarty.Skryta;
            }
        }
        enum StavKarty
        {
            Skryta, Otocena, Odstranena
        }
        struct Pozice
        {
            private readonly int x;
            public int X
            {
                get
                {
                    return x;
                }
            }
            private readonly int y;
            public int Y
            {
                get
                {
                    return y;
                }
            }
            public Pozice(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            // jak to funguje - v loopu se vytvoří struct - ten zavolá konstruktor, který přiřadí k lokálním proměnnám x a y hodnoty x a y z loopu (pomocí this). To znamená, že x a y mohou být změněné POUZE v konstruktoru - kromě toho struktura navrací pouze get, nikoliv set.
            // Podobně jako se třídami, každý strukt uchovává jiné hodnoty podle jednoho vzoru
        }
    }
}
