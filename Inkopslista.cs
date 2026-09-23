
using System;
using System.Collections.Generic;
using System.Linq;


// Varor, priser och lager
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


// Varukorg och totalpris
Dictionary<string, int> varukorg = new Dictionary<string, int>();
int totalPris = 0;


Console.WriteLine("==============================");
Console.WriteLine("        INKÖPSLISTA");
Console.WriteLine("==============================");


while (!lager.All(x => x == 0) || varukorg.Count > 0)
{
    Console.WriteLine("\nVaror:");

    for (int i = 0; i < varor.Count; i++)
    {
        string status = lager[i] > 0
            ? $"{lager[i]} st i lager"
            : "SLUT I LAGER";

        Console.WriteLine(
            $"{i + 1}. {varor[i]} - {priser[i]} kr - {status}"
        );
    }


    // Visar varukorgen
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
        Console.WriteLine("\nVarukorgen är tom.");
    }


    Console.WriteLine(
        "\nSkriv varans namn, nummer eller 'borttag'. Enter = avsluta"
    );

    string? val = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(val))
    {
        break;
    }


    // Ta bort en vara från varukorgen
    if (val.Equals("borttag", StringComparison.OrdinalIgnoreCase))
    {
        if (varukorg.Count == 0)
        {
            Console.WriteLine("Varukorgen är tom.");
            continue;
        }

        Console.Write("Vilken vara vill du ta bort? ");
        string? borttagVal = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(borttagVal))
        {
            continue;
        }

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


    // Hitta varan som användaren valt
    int varanIndex = -1;

    if (int.TryParse(val, out int valtNummer))
    {
        if (valtNummer >= 1 && valtNummer <= varor.Count)
        {
            varanIndex = valtNummer - 1;
        }
        else
        {
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


    // Köp varan
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


// ==============================
// KVITTO
// ==============================

int kvittoNummer = Random.Shared.Next(10000, 99999);
DateTime datum = DateTime.Now;

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
    Console.WriteLine("Inga varor var köpta.");
}


Console.WriteLine("------------------------------");
Console.WriteLine($"Antal varor: {varukorg.Values.Sum()}");
Console.WriteLine($"Totalt: {totalPris} kr");
Console.WriteLine("==============================");
Console.WriteLine("Öppet hela dygnet, alla dagar i veckan");
Console.WriteLine("Välkommen åter!");




