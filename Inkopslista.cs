// Gör att man inte behöver skriva System. framför varje klass från System-namespacet.
using System;

// Gör det möjligt att använda List och Dictionary.
using System.Collections.Generic;

// Gör det möjligt att använda metoder som .All() och .Sum().
using System.Linq;


// Skapar listor för varor, priser och lagerstatus.
List<string> varor = new List<string>
{
    "Mjölk",
    "Grötbröd",
    "Goudaost"
};

List<int> priser = new List<int>
{
    15,
    28,
    113
};

List<int> lager = new List<int>
{
    12,
    9,
    8
};


// Skapar en varukorg som håller reda på vilka varor som köpts och antal.
Dictionary<string, int> varukorg = new Dictionary<string, int>();
int totalPris = 0;

// Rubrik för inköpslistan.
Console.WriteLine("==============================");
Console.WriteLine("        INKÖPSLISTA");
Console.WriteLine("==============================");

// Loopar tills alla varor är slut i lager och varukorgen är tom.
while (!lager.All(x => x == 0) || varukorg.Count > 0)
{

// Visar alla varor, priser och lagerstatus.
    Console.WriteLine("\nVaror:");

    for (int i = 0; i < varor.Count; i++)
    {

// Om varan skulle vara slutsåld så visas detta istället.
        string status = lager[i] > 0
            ? $"{lager[i]} st i lager"
            : "SLUT I LAGER";

        Console.WriteLine(
            $"{i + 1}. {varor[i]} - {priser[i]} kr - {status}"
        );
    }


    // Visar vad som ligger i den aktuella varukorgen.
    if (varukorg.Count > 0)
    {
        Console.WriteLine("\nVarukorg:");

        foreach (var vara in varukorg)
        {
            Console.WriteLine($"{vara.Key} - {vara.Value} st");
        }
    }
    else
    {
// Visas om varukorgen skulle vara tom.
        Console.WriteLine("\nVarukorgen är tom.");
    }

// Låter kunden välja en vara med en siffra eller skriva varans namn.
    Console.WriteLine(
        "\nSkriv varans namn, nummer eller 'borttag'. Enter = avsluta"
    );

// Om användaren trycker Enter avslutas programmet.
    string? val = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(val))
    {
        break;
    }


// Borttag tar bort en vara i varukorgen och den läggs tillbaka i lager.
    if (val.Equals("borttag", StringComparison.OrdinalIgnoreCase))
    {
        if (varukorg.Count == 0)
        {
// Visar ett felmeddelande om varukorgen är tom.
            Console.WriteLine("Varukorgen är tom.");
            continue;
        }

// Användaren kan välja att ta bort en vara med nummer eller namn på varan.
        Console.Write("Vilken vara vill du ta bort? ");
        string? borttagVal = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(borttagVal))
        {
            continue; //Åter till huvudmenyn.
        }

// Letar upp varan som ska tas bort.
        int index = -1;
        if (int.TryParse(borttagVal, out int nummer))
        {
            if (nummer >= 1 && nummer <= varor.Count)
            {
                index = nummer - 1;
            }
        }
        else
        {
            index = varor.FindIndex(
                vara => vara.Equals(
                    borttagVal,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }

        if (index == -1)
        {
            Console.WriteLine("Varan hittades inte.");
            continue;
        }

        string vara = varor[index];

        if (!varukorg.ContainsKey(vara))
        {
            Console.WriteLine($"{vara} finns inte i varukorgen.");
            continue;
        }

        varukorg[vara]--;

        if (varukorg[vara] == 0)
        {
            varukorg.Remove(vara);
        }

        lager[index]++;
        totalPris -= priser[index];

        Console.WriteLine($"{vara} har tagits bort.");

        continue;
    }


    // Hittar varan som kunden valt.
    int varanIndex = -1;

    if (int.TryParse(val, out int valtNummer))
    {
        if (valtNummer >= 1 && valtNummer <= varor.Count)
        {
            varanIndex = valtNummer - 1;
        }
        else
        {
            // Felhantering om kunden råkar välja ett nummer som inte finns i menyn.
            Console.WriteLine("Ogiltigt nummer.");
            continue;
        }
    }
    else
    {
        varanIndex = varor.FindIndex(
            vara => vara.Equals(
                val,
                StringComparison.OrdinalIgnoreCase
            )
        );

        if (varanIndex == -1)
        {
            Console.WriteLine($"Varan '{val}' hittades inte.");
            continue;
        }
    }


// Lägger den valda varan i varukorgen.
// Om varan är slut i lager visas ett felmeddelande.
    if (lager[varanIndex] > 0)
    {
        string valdVara = varor[varanIndex];

        lager[varanIndex]--;
        totalPris += priser[varanIndex];

        if (varukorg.ContainsKey(valdVara))
        {
            varukorg[valdVara]++;
        }
        else
        {
            varukorg[valdVara] = 1;
        }

        Console.WriteLine(
            $"{valdVara} köpt för {priser[varanIndex]} kr."
        );
    }
    else
    {
        Console.WriteLine(
            $"{varor[varanIndex]} är slut i lager."
        );
    }
}


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


foreach (var artikel in varukorg)
{
    int index = varor.IndexOf(artikel.Key);
    int pris = priser[index] * artikel.Value;

    Console.WriteLine(
        $"{artikel.Key} - {artikel.Value} st - {pris} kr"
    );
}


if (varukorg.Count == 0)
{
    // Felhantering om inga varor var köpta.
    Console.WriteLine("Inga varor var köpta.");
}


Console.WriteLine("------------------------------");
Console.WriteLine($"Antal varor: {varukorg.Values.Sum()}");
Console.WriteLine($"Totalt: {totalPris} kr");
Console.WriteLine("==============================");
// Valde att avsluta kvittot med öppettider och "Välkommen åter".
Console.WriteLine("Öppet hela dygnet, alla dagar i veckan");
Console.WriteLine("Välkommen åter!");




