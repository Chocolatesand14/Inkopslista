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
Console.WriteLine("\nSkriv in en vara, numret på varan du vill ta bort, 'dyrast','kvitto' eller 'klar': ");

string? nyVara = Console.ReadLine();

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

if (nyVara?.ToLower() == "kvitto")
{
    
// Visar ett kvitto med köpta varor, antal och totalpris.
// Om inga varor har köpts visas ett meddelande om detta.
    int kvittoNummer = Random.Shared.Next(10000, 99999);
    DateTime datum = DateTime.Now;

// Efter att ha tittat på ett kvitto hemma valde jag att piffa upp mitt kvitto
// med sådant som finns på ett riktigt kvitto.
// Det jag la till var kvittonummer, dagens datum och vilken tid som köpet gjordes.
    Console.WriteLine();
    Console.WriteLine("==============================");
    Console.WriteLine("           KVITTO");
    Console.WriteLine("==============================");
    Console.WriteLine($"Kvittonummer: {kvittoNummer}");
    Console.WriteLine($"Datum: {datum:yyyy-MM-dd HH:mm}");
    Console.WriteLine("------------------------------");

    for (int i = 0; i < varor.Count; i++)
    {
        Console.WriteLine($"{varor[i]} - {priser[i]} kr");
    }
    Console.WriteLine("------------------------------");
    Console.WriteLine($"Antal varor: {varor.Count}");
    Console.WriteLine($"Totalt: {priser.Sum()} kr");
    Console.WriteLine("==============================");
    // Valde att avsluta kvittot med öppettider och "Välkommen åter".
    Console.WriteLine("Öppet 24/7");
    Console.WriteLine("Välkommen åter!");
    break;
}

    // Avslutar om användaren skriver "klar"
if (nyVara?.ToLower() == "klar")
{
    break;
}

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
