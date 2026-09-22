using System;
using System.Collections.Generic;
using System.Linq;

// Produkter
List<string> varor = new List<string>
{
    "Mjölk",
    "Grötbröd",
    "Goudaost"
};

// Priser
List<int> priser = new List<int>
{
    15,
    28,
    113
};

// Lager
List<int> lager = new List<int>
{
    12,
    9,
    8
};

// Varukorg
Dictionary<string, int> varukorg = new Dictionary<string, int>();

int totalPris = 0;

while (!lager.All(x => x == 0) || varukorg.Count > 0)
{
    Console.WriteLine("\n--- VAROR ---");

    for (int i = 0; i < varor.Count; i++)
    {
        Console.WriteLine(
            $"{i + 1}. {varor[i]} - {priser[i]} kr - " +
            (lager[i] > 0 ? $"{lager[i]} st i lager" : "SLUT I LAGER")
        );
    }

    Console.WriteLine("\n--- VARUKORG ---");

    if (varukorg.Count == 0)
    {
        Console.WriteLine("Varukorgen är tom.");
    }
    else
    {
        foreach (var vara in varukorg)
        {
            Console.WriteLine($"{vara.Key} - {vara.Value} st");
        }
    }

    Console.WriteLine(
        "\nAnge produktens namn, nummer eller 'borttag'. " +
        "Tryck Enter för att avsluta:"
    );

    string? val = Console.ReadLine();

    if (string.IsNullOrEmpty(val))
        break;

    // Ta bort en vara
    if (val.Equals("borttag", StringComparison.OrdinalIgnoreCase))
    {
        if (varukorg.Count == 0)
        {
            Console.WriteLine("Varukorgen är tom.");
            continue;
        }

        Console.Write("Vilken vara vill du ta bort? ");
        string? borttag = Console.ReadLine();

        if (string.IsNullOrEmpty(borttag))
            continue;

        int index = -1;

        if (int.TryParse(borttag, out int nummer))
        {
            index = nummer - 1;
        }
        else
        {
            index = varor.FindIndex(
                x => x.Equals(borttag, StringComparison.OrdinalIgnoreCase)
            );
        }

        if (index < 0 || index >= varor.Count)
        {
            Console.WriteLine("Varan hittades inte.");
            continue;
        }

        string vara = varor[index];

        if (!varukorg.ContainsKey(vara))
        {
            Console.WriteLine("Varan finns inte i varukorgen.");
            continue;
        }

        varukorg[varan]--;

        if (varukorg[varan] == 0)
            varukorg.Remove(varan);

        lager[index]++;
        totalPris -= priser[index];

        Console.WriteLine($"{vara} togs bort från varukorgen.");
        continue;
    }

    // Hitta vald vara
    int varanIndex = -1;

    if (int.TryParse(val, out int menyNummer))
    {
        varanIndex = menyNummer - 1;
    }
    else
    {
        varanIndex = varor.FindIndex(
            x => x.Equals(val, StringComparison.OrdinalIgnoreCase)
        );
    }

    if (varanIndex < 0 || varanIndex >= varor.Count)
    {
        Console.WriteLine("Varan hittades inte.");
        continue;
    }

    // Köp vara
    if (lager[varanIndex] == 0)
    {
        Console.WriteLine($"{varor[varanIndex]} är slut i lager.");
        continue;
    }

    string valdVara = varor[varanIndex];

    lager[varanIndex]--;
    totalPris += priser[varanIndex];

    if (varukorg.ContainsKey(valdVara))
        varukorg[valdVara]++;
    else
        varukorg.Add(valdVara, 1);

    Console.WriteLine(
        $"{valdVara} köptes för {priser[varanIndex]} kr."
    );
}

// KVITTO
int kvittoNummer = Random.Shared.Next(10000, 99999);
DateTime datum = DateTime.Now;

Console.WriteLine("\n==============================");
Console.WriteLine("             KVITTO");
Console.WriteLine("==============================");
Console.WriteLine($"Kvittonummer: {kvittoNummer}");
Console.WriteLine($"Datum: {datum:yyyy-MM-dd HH:mm}");
Console.WriteLine("------------------------------");

foreach (var vara in varukorg)
{
    int index = varor.IndexOf(vara.Key);
    int summa = priser[index] * vara.Value;

    Console.WriteLine(
        $"{vara.Key} - {vara.Value} st - {summa} kr"
    );
}

if (varukorg.Count == 0)
    Console.WriteLine("Inga varor var köpta.");

Console.WriteLine("------------------------------");
Console.WriteLine($"Antal varor: {varukorg.Values.Sum()}");
Console.WriteLine($"Totalt: {totalPris} kr");
Console.WriteLine("==============================");
Console.WriteLine("Öppet hela dygnet, alla dagar i veckan");
Console.WriteLine("Välkommen åter!");



