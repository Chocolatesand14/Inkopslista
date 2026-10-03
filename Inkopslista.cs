// Gör att man inte behöver skriva System. framför varje klass från System-namespacet.
using System;

// Gör det möjligt att använda List.
using System.Collections.Generic;

// Gör det möjligt att använda metoden .Sum().
using System.Linq;

// Två parallella listor
List<string> varor = new List<string>();
List<int> priser = new List<int>();

// Loop som körs tills användaren väljer att avsluta 
while (true)
{
    // Rubrik för inköpslistan.
   Console.WriteLine("==============================");
   Console.WriteLine("        INKÖPSLISTA");
   Console.WriteLine("==============================");
    
    // Visar alla varor som en numrerad lista
    for (int i = 0; i < varor.Count; i++)
{
    Console.WriteLine($"{i + 1}. {varor[i]} - {priser[i]} kr");
}
// Räknar ut totalsumman
int listaTotal = priser.Sum();
Console.WriteLine($"Totalt pris: {listaTotal} kr");

// Användaren skriver in en vara
Console.WriteLine("\nSkriv in en vara, numret på varan du vill ta bort, 'dyrast', 'sortera' eller 'klar': ");

string? nyVara = Console.ReadLine();

// Extra: Visar vilken vara som är dyrast
if (nyVara?.ToLower() == "dyrast")
{
    if (priser.Count > 0)
    {
        int högstaPris = priser.Max();   
        int index = priser.IndexOf(högstaPris);
        Console.WriteLine($"Den dyraste varan är: {varor[index]} - {priser[index]} kr");
    }
    else
    {
            Console.WriteLine("Inga varor i listan.");
    }
    continue;
}

// Extra: Sorterar varorna efter pris, lägst till högst.
if (nyVara?.ToLower() == "sortera")
{
    for (int i = 0; i < priser.Count - 1; i++)
    {
         for (int j = i + 1; j < priser.Count; j++)
          {
        
             if (priser[i] > priser[j])
             {
                 int tempPris = priser[i];
                 priser[i] = priser[j];
                 priser[j] = tempPris;

                 string tempVara = varor[i];
                 varor[i] = varor[j];
                 varor[j] = tempVara;
             }
         }
     }

     Console.WriteLine("Varorna har sorterats efter pris.");
     continue;
}

// Avslutar om användaren skriver "klar"
if (nyVara?.ToLower() == "klar")
{
    break;
}

// Tar bort en vara om användaren skriver in numret på varan
if (int.TryParse(nyVara, out int nummer))
    {
        int index = nummer - 1;

        if (index >= 0 && index < varor.Count)
        {
            varor.RemoveAt(index);
            priser.RemoveAt(index);
        }
        else
        {
            Console.WriteLine("Ogiltigt nummer.");
        }
        continue;
    }
// Användaren skriver in priset på varan
Console.WriteLine("Skriv in priset: ");
string? prisInput = Console.ReadLine();

if (!int.TryParse(prisInput, out int pris))
{
    Console.WriteLine("Ogiltigt pris.");
    continue;
}

// Lägger till namn och pris på varan i listorna
varor.Add(nyVara!);
priser.Add(pris);
}
