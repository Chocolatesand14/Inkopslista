// När man använder using System; så behöver inte koden
// specificera hela namnet på klassen, utan kan använda kortnamnet.
using System;

// Hjälper till att använda List<T> och Dictionary<TKey, TValue>.
using System.Collections.Generic;

// Behövs eftersom vi använder metoderna All() och Sum().
using System.Linq;


// Lista med produkter
List<string> produkter = new List<string>
{
    "Mjölk",
    "Grötbröd",
    "Goudaost",
};

// Lista med priser (i kronor)
List<int> priser = new List<int>
{
    15,
    28,
    113,
};

// Lista på lagerstatus (hur många som finns i lager)
List<int> lager = new List<int>
{
    12,
    9,
    8,
};

// Varukorg håller koll på vilken produkt som köpts
// och antalet av den produkten.
Dictionary<string, int> varukorg = new Dictionary<string, int>();

// Räknar ut totalpriset för varukorgen.
int totalPris = 0;


// Loopar så länge det finns produkter kvar i lager
// eller något finns kvar i varukorgen.
while (!lager.All(antal => antal == 0) || varukorg.Count > 0)
{
    // Visar produkter med pris och lagerstatus.
    Console.WriteLine("Produkter:");

    for (int i = 0; i < produkter.Count; i++)
    {
        if (lager[i] > 0)
        {
            // Visar produkten, priset och hur många som finns i lager.
            Console.WriteLine(
                $"{i + 1}. {produkter[i]} - {priser[i]} kr - {lager[i]} st i lager"
            );
        }
        else
        {
            // Om en produkt är slut i lager visas detta istället.
            Console.WriteLine(
                $"{i + 1}. {produkter[i]} - {priser[i]} kr - SLUT I LAGER"
            );
        }
    }


    // Visar varukorgen med produkter och antal.
    if (varukorg.Count > 0)
    {
        Console.WriteLine("\nVarukorg:");

        foreach (var artikel in varukorg)
        {
            Console.WriteLine($"{artikel.Key} - {artikel.Value} st");
        }
    }
    else
    {
        Console.WriteLine("\nVarukorgen är tom.");
    }


    // Frågar användaren vilken produkt som ska köpas.
    // Användaren kan ange produktens namn, nummer eller "borttag".
    // Om användaren trycker Enter avslutas programmet.
    Console.WriteLine(
        "\nAnge produktens namn, menynummer eller 'borttag' (Enter = avsluta programmet)"
    );

    string? val = Console.ReadLine();


    // Om användaren trycker Enter avslutas programmet.
    if (string.IsNullOrEmpty(val))
    {
        break;
    }


    // Om användaren skriver "borttag" tas en produkt bort från varukorgen.
    if (val.Equals("borttag", StringComparison.OrdinalIgnoreCase))
    {
        // Kontrollerar om varukorgen är tom.
        if (varukorg.Count == 0)
        {
            Console.WriteLine(
                "Varukorgen är tom. Ingen produkt att ta bort."
            );

            continue;
        }


        // Frågar vilken produkt användaren vill ta bort.
        Console.WriteLine(
            "Vilken produkt vill du ta bort (namn eller nummer)?"
        );

        string? borttagVal = Console.ReadLine();


        // Felhantering om användaren inte anger något.
        if (string.IsNullOrEmpty(borttagVal))
        {
            continue;
        }


        // Söker efter produkten baserat på namn eller nummer.
        int borttagIndex = -1;

        if (int.TryParse(borttagVal, out int borttagNummer))
        {
            // Om användaren anger ett nummer konverteras det till index.
            borttagIndex = borttagNummer - 1;
        }
        else
        {
            // Om användaren anger ett namn söker vi efter produkten.
            borttagIndex = produkter.FindIndex(
                p => p.Equals(
                    borttagVal,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }


        // Kontrollerar att produkten finns.
        if (borttagIndex >= 0 && borttagIndex < produkter.Count)
        {
            string borttagProdukt = produkter[borttagIndex];


            // Kontrollerar att produkten faktiskt finns i varukorgen.
            if (varukorg.ContainsKey(borttagProdukt))
            {
                // Om det finns flera av produkten minskas antalet med 1.
                if (varukorg[borttagProdukt] > 1)
                {
                    varukorg[borttagProdukt]--;
                }
                else
                {
                    // Om det bara finns en tas produkten bort helt.
                    varukorg.Remove(borttagProdukt);
                }


                // Lägger tillbaka produkten i lagret.
                lager[borttagIndex]++;

                // Tar bort produktens pris från totalsumman.
                totalPris -= priser[borttagIndex];


                Console.WriteLine(
                    $"Produkten {borttagProdukt} har tagits bort från varukorgen."
                );
            }
            else
            {
                Console.WriteLine(
                    $"Produkten '{borttagProdukt}' finns inte i varukorgen."
                );
            }
        }
        else
        {
            Console.WriteLine(
                $"Produkten '{borttagVal}' hittades inte."
            );
        }


        // Går tillbaka till huvudmenyn.
        continue;
    }


    // Först kontrolleras om användaren har angett ett menynummer.
    // Om inte söks produkten efter med namn.
    int produktIndex = -1;

    if (int.TryParse(val, out int menyVal))
    {
        if (menyVal >= 1 && menyVal <= produkter.Count)
        {
            // Gör om användarens nummer till listans index.
            produktIndex = menyVal - 1;
        }
        else
        {
            Console.WriteLine(
                $"Felaktigt menyval. Ange ett giltigt nummer mellan 1 och {produkter.Count}."
            );

            continue;
        }
    }
    else
    {
        // Om produkten inte anges med en siffra söks den upp med namn.
        produktIndex = produkter.FindIndex(
            p => p.Equals(
                val,
                StringComparison.OrdinalIgnoreCase
            )
        );


        // Om produkten inte finns visas ett felmeddelande.
        if (produktIndex == -1)
        {
            Console.WriteLine(
                $"Produkten '{val}' hittades inte."
            );

            continue;
        }
    }


    // Kontrollerar om produkten finns i lager.
    if (lager[produktIndex] > 0)
    {
        // Lägger till priset i totalsumman.
        totalPris += priser[produktIndex];

        // Minskar antalet produkter i lager.
        lager[produktIndex]--;


        // Lägger till produkten i varukorgen.
        // Om produkten redan finns ökas antalet med 1.
        if (varukorg.ContainsKey(produkter[produktIndex]))
        {
            varukorg[produkter[produktIndex]]++;
        }
        else
        {
            varukorg.Add(produkter[produktIndex], 1);
        }


        Console.WriteLine(
            $"{produkter[produktIndex]} köpt för {priser[produktIndex]} kr ({lager[produktIndex]} st kvar i lager)."
        );
    }
    else
    {
        Console.WriteLine(
            $"Produkten {produkter[produktIndex]} är slut i lager."
        );
    }
}


// Kvitto: visar varukorgen med produkter, antal och totalpris.
Console.WriteLine("==============================");
Console.WriteLine("          KVITTO");
// Lagt till en rad med streck för att separera kvittot från totalen.
Console.WriteLine("==============================");

if (varukorg.Count > 0)
{
    foreach (var artikel in varukorg)
    {
        // Söker upp priset för produkten i listan med priser.
        int prisIndex = produkter.IndexOf(artikel.Key);

        Console.WriteLine(
            $"{artikel.Key} - {artikel.Value} st - {priser[prisIndex] * artikel.Value} kr"
        );
    }
}
else
{
    Console.WriteLine("Inga produkter var köpta.");
}

// Visar totalt antal köpta produkter.
Console.WriteLine(
    $"Antal produkter: {varukorg.Values.Sum()}"
);

// Har lagt till en rad med streck för att separera kvittot från totalen.
Console.WriteLine("-------------------------------");

// Visar totalpriset.
Console.WriteLine(
    $"Total: {totalPris} kr"
);
Console.WriteLine("==============================");
Console.WriteLine("Öppet hela dygnet, alla dagar i veckan");
Console.WriteLine("Välkommen åter!");

