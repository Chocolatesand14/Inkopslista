// När man använder using System; så behöver inte koden
// specificera hela namnet på klassen, utan kan använda kortnamnet.
using System;

// Hjälper till att använda List<T> och Dictionary<TKey, TValue>.
using System.Collections.Generic;

// Behövs eftersom vi använder metoderna All() och Sum().
using System.Linq;


// Lista med varor (produkter) som finns i butiken.
List<string> varor = new List<string>
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

// Lista på lagerstatus (Lista på hur många varor som finns i lager)
List<int> lager = new List<int>
{
    12,
    9,
    8,
};

// Varukorg håller koll på vilken vara som köpts
// och antalet av den varan.
Dictionary<string, int> varukorg = new Dictionary<string, int>();

// Räknar ut totalpriset för varukorgen.
int totalPris = 0;


// Loopar så länge det finns varor kvar i lager
// eller något finns kvar i varukorgen.
while (!lager.All(antal => antal == 0) || varukorg.Count > 0)
{
    // Visar varor med pris och lagerstatus.
    Console.WriteLine("Varor:");

    for (int i = 0; i < varor.Count; i++)
    {
        if (lager[i] > 0)
        {
            // Visar varan, priset och hur många som finns i lager.
            Console.WriteLine(
                $"{i + 1}. {varor[i]} - {priser[i]} kr - {lager[i]} st i lager"
            );
        }
        else
        {
            // Om en vara är slut i lager visas detta istället.
            Console.WriteLine(
                $"{i + 1}. {varor[i]} - {priser[i]} kr - SLUT I LAGER"
            );
        }
    }


    // Visar varukorgen med varor och antal.
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
        "\nAnge varans namn, menynummer eller 'borttag' (Enter = avsluta programmet)"
    );

    string? val = Console.ReadLine();


    // Om användaren trycker Enter avslutas programmet.
    if (string.IsNullOrEmpty(val))
    {
        break;
    }


    // Om användaren skriver "borttag" tas en vara bort från varukorgen.
    if (val.Equals("borttag", StringComparison.OrdinalIgnoreCase))
    {
        // Kontrollerar om varukorgen är tom.
        if (varukorg.Count == 0)
        {
            Console.WriteLine(
                "Varukorgen är tom. Ingen vara att ta bort."
            );

            continue;
        }


        // Frågar vilken vara användaren vill ta bort.
        Console.WriteLine(
            "Vilken vara vill du ta bort (namn eller nummer)?"
        );

        string? borttagVal = Console.ReadLine();


        // Felhantering om användaren inte anger något.
        if (string.IsNullOrEmpty(borttagVal))
        {
            continue;
        }


        // Söker efter varan baserat på namn eller nummer.
        int borttagIndex = -1;

        if (int.TryParse(borttagVal, out int borttagNummer))
        {
            // Om användaren anger ett nummer konverteras det till index.
            borttagIndex = borttagNummer - 1;
        }
        else
        {
            // Om användaren anger ett namn söker vi efter varan.
            borttagIndex = varor.FindIndex(
                p => p.Equals(
                    borttagVal,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }


        // Kontrollerar att varan finns.
        if (borttagIndex >= 0 && borttagIndex < varor.Count)
        {
            string borttagVaran = varor[borttagIndex];


            // Kontrollerar att varan faktiskt finns i varukorgen.
            if (varukorg.ContainsKey(borttagVaran))
            {
                // Om det finns flera av varan minskas antalet med 1.
                if (varukorg[borttagVaran] > 1)
                {
                    varukorg[borttagVaran]--;
                }
                else
                {
                    // Om det bara finns en tas varan bort helt.
                    varukorg.Remove(borttagVaran);
                }


                // Lägger tillbaka varan i lagret.
                lager[borttagIndex]++;

                // Tar bort varans pris från totalsumman.
                totalPris -= priser[borttagIndex];


                Console.WriteLine(
                    $"Varan {borttagVaran} har tagits bort från varukorgen."
                );
            }
            else
            {
                Console.WriteLine(
                    $"Varan '{borttagVaran}' finns inte i varukorgen."
                );
            }
        }
        else
        {
            Console.WriteLine(
                $"Varan '{borttagVal}' hittades inte."
            );
        }


        // Går tillbaka till huvudmenyn.
        continue;
    }


    // Först kontrolleras om användaren har angett ett menynummer.
    // Om inte söks varan efter med namn.
    int varanIndex = -1;

    if (int.TryParse(val, out int menyVal))
    {
        if (menyVal >= 1 && menyVal <= varor.Count)
        {
            // Gör om användarens nummer till listans index.
            varanIndex = menyVal - 1;
        }
        else
        {
            Console.WriteLine(
                $"Felaktigt menyval. Ange ett giltigt nummer mellan 1 och {varor.Count}."
            );

            continue;
        }
    }
    else
    {
        // Om varan inte anges med en siffra söks den upp med namn.
        varanIndex = varor.FindIndex(
            p => p.Equals(
                val,
                StringComparison.OrdinalIgnoreCase
            )
        );


        // Om varan inte finns visas ett felmeddelande.
        if (varanIndex == -1)
        {
            Console.WriteLine(
                $"Varan '{val}' hittades inte."
            );

            continue;
        }
    }


    // Kontrollerar om varan finns i lager.
    if (lager[varanIndex] > 0)
    {
        // Lägger till priset i totalsumman.
        totalPris += priser[varanIndex];

        // Minskar antalet varor i lager.
        lager[varanIndex]--;


        // Lägger till varan i varukorgen.
        // Om varan redan finns ökas antalet med 1.
        if (varukorg.ContainsKey(varor[varanIndex]))
        {
            varukorg[varor[varanIndex]]++;
        }
        else
        {
            varukorg.Add(varor[varanIndex], 1);
        }


        Console.WriteLine(
            $"{varor[varanIndex]} köpt för {priser[varanIndex]} kr ({lager[varanIndex]} st kvar i lager)."
        );
    }
    else
    {
        Console.WriteLine(
            $"Varan {varor[varanIndex]} är slut i lager."
        );
    }
}

// Skapar ett kvittonummer.
int kvittoNummer = Random.Shared.Next(10000, 99999);

// Hämtar dagens datum och tid.
DateTime datum = DateTime.Now;

// Kvitto: visar varukorgen med varor, antal och totalpris.
Console.WriteLine("==============================");
//Ställer in kvittot i mitten av konsolen.
Console.WriteLine("          KVITTO");
Console.WriteLine("==============================");
Console.WriteLine($"Kvittonummer: {kvittoNummer}");
Console.WriteLine($"Datum: {datum:yyyy-MM-dd HH:mm}");
Console.WriteLine("------------------------------");

if (varukorg.Count > 0)
{
    foreach (var artikel in varukorg)
    {
        // Söker upp priset för varan i listan med priser.
        int prisIndex = varor.IndexOf(artikel.Key);

        Console.WriteLine(
            $"{artikel.Key} - {artikel.Value} st - {priser[prisIndex] * artikel.Value} kr"
        );
    }
}
else
{
    Console.WriteLine("Inga varor var köpta.");
}

// Visar totalt antal köpta varor.
Console.WriteLine(
    $"Antal varor: {varukorg.Values.Sum()}"
);

// Har lagt till en rad med streck för att separera kvittot från totalpris.
Console.WriteLine("-------------------------------");

// Visar totalpriset.
Console.WriteLine(
    $"Totalt: {totalPris} kr"
);
Console.WriteLine("==============================");
// Avslutar programmet med ett meddelande.
Console.WriteLine("Öppet hela dygnet, alla dagar i veckan");
Console.WriteLine("Välkommen åter!");

