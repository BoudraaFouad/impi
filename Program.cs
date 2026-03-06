namespace impiccato
{
    internal class Program
    {
        // pia na parola a caso dall'array
        static string estraiParola(string[] par)
        {
            Random r = new Random();      // numero casuale
            int idx = r.Next(par.Length); // indice casuale
            return par[idx];
        }

        // mette tutti trattini al posto delle lettere
        static char[] trasforma(string par)
        {
            char[] indice = new char[par.Length]; // array de trattini
            for (int i = 0; i < par.Length; i++)
            {
                indice[i] = '_';
            }
            return indice;
        }

        // stampa la parola lettera pe lettera
        static void stampaParola(char[] indice)
        {
            for (int i = 0; i < indice.Length; i++)
            {
                Console.Write(indice[i] + " ");
            }
            Console.WriteLine();
        }

        // rivela na lettera casuale ancora nascosta
        static void usaJolly(string par, char[] indice)
        {
            for (int i = 0; i < par.Length; i++)
            {
                if (indice[i] == '_') // trovato ntrattino
                {
                    indice[i] = par[i]; // lo sostituisce
                    Console.WriteLine("jolly usato lettera '" + par[i] + "' rivelata.");
                    return;
                }
            }
           
        }

        // tutta la logica del gioco
        static void gioca(string parola, int maxTent, int maxMon, string cat)
        {
            char[] indice = trasforma(parola); // parola coi trattini
            int tent = 0;             // tentativi fatti
            int mon = maxMon;        // monete rimaste
            bool jolly = true;          // se ha ancora jolly
            bool haVinto = false;         // per vittoria

            Console.WriteLine("la parola ha " + parola.Length + " lettere.");
            Console.WriteLine("hai " + maxTent + " tentativi.");
            Console.WriteLine("monete: " + mon + " | jolly: disponibile");
            Console.WriteLine("");
            Console.WriteLine("comandi: J = jolly  1 = prima lettera (3 mon)  2 = ultima lettera (4 mon)");

            Console.WriteLine("");

            while (tent < maxTent)
            {
                stampaParola(indice);
                Console.WriteLine("tentativi rimasti: " + (maxTent - tent));
                if (jolly == true)
                {
                    Console.WriteLine("monete: " + mon + " | jolly: disponibile");
                }
                else
                {
                    Console.WriteLine("monete: " + mon + " | jolly: usato");
                }
                Console.Write("inserisci lettera (o J/1/2): ");

                string sc = Console.ReadLine(); // input del giocatore

                //  jolly 
                if (sc.ToLower() == "j")
                {
                    if (jolly == false)
                    {
                        Console.WriteLine("jolly gia' usato!\n");
                    }
                    else
                    {
                        usaJolly(parola, indice);
                        jolly = false;
                        tent++;
                        Console.WriteLine("");
                    }
                }


                // prima lettera - 3 monete 
                else if (sc == "1")
                {
                    if (mon < 3)
                    {
                        Console.WriteLine("monete insufficienti! (servono 3)\n");
                    }
                    else
                    {
                        mon -= 3;
                        indice[0] = parola[0]; // indice 0 = prima lettera
                        Console.WriteLine("prima lettera: " + parola[0]);
                        Console.WriteLine("monete rimaste: " + mon + "\n");
                        tent++;
                    }
                }

                //  ultima lettera - 4 monete 
                else if (sc == "2")
                {
                    if (mon < 4)
                    {
                        Console.WriteLine("monete insufficienti! (servono 4)\n");
                    }
                    else
                    {
                        mon -= 4;
                        indice[parola.Length - 1] = parola[parola.Length - 1]; // length-1 = ultimo indice
                        Console.WriteLine("ultima lettera: " + parola[parola.Length - 1]);
                        Console.WriteLine("monete rimaste: " + mon + "\n");
                        tent++;
                    }
                }

                // tentativo parola intera 
                else if (sc.Length > 1)
                {
                    tent++; 
                    if (sc.ToLower() == parola.ToLower()) // parola giusta?
                    {
                        for (int i = 0; i < parola.Length; i++)
                        {
                            indice[i] = parola[i]; // riempie tutto
                        }
                        Console.WriteLine("");
                        stampaParola(indice);
                        Console.WriteLine("hai vinto! la parola era: " + parola);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("parola sbagliata!\n");
                    }
                }

                // lettera normale 
                else
                {
                    char let = char.ToLower(sc[0]); // lettera in minuscolo

                    bool trovata = parola.ToLower().Contains(let);
                    // lettera trovata
                    if (trovata == true)
                    {
                        for (int i = 0; i < parola.Length; i++)
                        {
                            if (char.ToLower(parola[i]) == let)
                            {
                                indice[i] = parola[i]; // sostituisce ltrattino
                            }
                        }
                    }

                    tent++; // ogni mossa conta

                    if (trovata == true)
                    {
                        Console.WriteLine("lettera presente!\n");
                    }
                    else
                    {
                        Console.WriteLine("lettera non presente!\n");
                    }
                }

                // controlla se ha vinto
                haVinto = true;
                for (int i = 0; i < indice.Length; i++)
                {
                    if (indice[i] == '_')
                    {
                        haVinto = false; // ancora trattini, non ha vinto
                    }
                }

                if (haVinto == true)
                {
                    Console.WriteLine("");
                    stampaParola(indice);
                    Console.WriteLine("hai vinto la parola era: " + parola);
                    return;
                }
            }

            Console.WriteLine("hai perso la parola era: " + parola);
        }

        static void Main(string[] args)
        {
            //  serie tv
            string[] tvF = { "Friends", "Gomorra", "Lupin" };
            string[] tvM = { "BreakingBad", "StrangerThings", "TheCrown" };
            string[] tvD = { "PeakyBlinders", "BlackMirror", "BetterCallSaul" };

            //  citta 
            string[] citF = { "Roma", "Milano", "Parigi" };
            string[] citM = { "Barcellona", "Amsterdam", "Vienna" };
            string[] citD = { "Reykjavik", "Bratislava", "Ouagadougou" };

            //  corpo umano
            string[] corF = { "gomito", "piede", "testa" };
            string[] corM = { "pancreas", "ginocchio", "spalla" };
            string[] corD = { "diaframma", "sternocleidomastoideo", "ipotalamo" };

            int tF = 4;  // tentativi facile
            int tM = 7;  // tentativi media
            int tD = 10; // tentativi difficile

            int mF = 10; // monete facile
            int mM = 6;  // monete media
            int mD = 5;  // monete difficile

            Console.WriteLine("benvenuto al gioco dell'impiccato");
            Console.WriteLine("scegli una categoria");
            Console.WriteLine("----------------------");
            Console.WriteLine("1. serie tv");
            Console.WriteLine("2. citta");
            Console.WriteLine("3. corpo umano");
            Console.WriteLine("----------------------");
            int sc = Convert.ToInt32(Console.ReadLine()); // scelta categoria

            while (sc < 1 || sc > 3)
            {
                Console.WriteLine("scegli 1, 2 o 3:");
                sc = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("scegli la difficolta");
            Console.WriteLine("-----------------------");
            Console.WriteLine("1. facile     (" + tF + " tentativi, " + mF + " monete)");
            Console.WriteLine("2. media      (" + tM + " tentativi, " + mM + " monete)");
            Console.WriteLine("3. difficile  (" + tD + " tentativi, " + mD + " monete)");
            Console.WriteLine("-----------------------");
            int dif = Convert.ToInt32(Console.ReadLine()); // scelta difficolta

            while (dif < 1 || dif > 3)
            {
                Console.WriteLine("scegli 1, 2 o 3:");
                dif = Convert.ToInt32(Console.ReadLine());
            }

            if (sc == 1 && dif == 1)
            {
                gioca(estraiParola(tvF), tF, mF, "serie tv");
            }
            else if (sc == 1 && dif == 2)
            {
                gioca(estraiParola(tvM), tM, mM, "serie tv");
            }
            else if (sc == 1 && dif == 3)
            {
                gioca(estraiParola(tvD), tD, mD, "serie tv");
            }
            else if (sc == 2 && dif == 1)
            {
                gioca(estraiParola(citF), tF, mF, "citta");
            }
            else if (sc == 2 && dif == 2)
            {
                gioca(estraiParola(citM), tM, mM, "citta");
            }
            else if (sc == 2 && dif == 3)
            {
                gioca(estraiParola(citD), tD, mD, "citta");
            }
            else if (sc == 3 && dif == 1)
            {
                gioca(estraiParola(corF), tF, mF, "corpo umano");
            }
            else if (sc == 3 && dif == 2)
            {
                gioca(estraiParola(corM), tM, mM, "corpo umano");
            }
            else if (sc == 3 && dif == 3)
            {
                gioca(estraiParola(corD), tD, mD, "corpo umano");
            }
        }
    }
}