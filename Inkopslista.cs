// När man använder using System; så behöver inte koden
// specificera hela namnet på klassen, utan kan använda kortnamnet.
using System;

// Hjälper till att använda List<T> och Dictionary<TKey, TValue>.
using System.Collections.Generic;

// Behövs eftersom vi använder metoden All().
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

// Räknar ut totalpriset för varukorgen
int totalPris = 0;

// Loopar så länge det finns produkter kvar i lager
// eller något finns kvar i varukorgen.
while (!lager.All(antal => antal == 0) || varukorg.Count > 0)
{
    // Visar produkter med pris och lagerstatus
    Console.WriteLine("Produkter:");

    for (int i = 0; i < produkter.Count; i++)
    {
        if (lager[i] > 0)
        {
            // Visar produkten, priset och hur många som finns i lager
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

    // Visar varukorgen med produkter och antal
    if (varukorg.Count > 0)
    {
        Console.WriteLine("Varukorg:");
        foreach (var artikel in varukorg)
        {
            Console.WriteLine($"{artikel.Key} - {artikel.Value} st");
        }
    }


  // Frågar användaren vilken produkt som ska köpas med hjälp av en siffra eller bokstavera i menyn. 0 avslutar programmet.
    Console.WriteLine("Ange produktens namn, menynummer eller 'borttag' (Enter = avsluta programmet)");
    string? val = Console.ReadLine();

    //Om användaren inte köpter någon produkt och trycker på Enter avslutas programmet.
    if (string.IsNullOrEmpty(val))
    {
        break;
    }

    // Om användaren skriver "borttag" tas en produkt bort från varukorgen.
    if (val.Equals("borttag", StringComparison.OrdinalIgnoreCase))
    {
       if (varukorg.Count == 0)
        {
            // Felmeddelande om varukorgen är tom.
            Console.WriteLine("Varukorgen är tom. Ingen produkt att ta bort.");
            continue;
        }

Console.WriteLine("Vilken produkt vill du ta bort (namn eller nummer)? ");
Console.WriteLine("Ange produkten:"); //Användaren får ange vilken produkt som ska tas bort med nummer eller namn.
string? borttagVal = Console.ReadLine();

//Felhantering om användaren inte anger något.
if (string.IsNullOrEmpty(borttagVal))
{
    continue; //Gå tillbaka huvudmenyn 
}

//Söker efter produkten i varukorgen baserat på (namn eller nummer).
int borttagIndex = -1;
if (int.TryParse(borttagVal, out int borttagNummer))
{
    // Om användaren anger ett nummer, konvertera det till index.
    borttagIndex = borttagNummer - 1;
}
else
{
    // Om användaren anger ett namn, sök efter produkten i varukorgen.
    borttagIndex = produkter.FindIndex(p => p.Equals(borttagVal, StringComparison.OrdinalIgnoreCase));
}

//Kollar så att produkten finns i varukorgen innan den tas bort.
if (borttagIndex != -1)
{
    string borttagProdukt = produkter[borttagIndex];

    if (varukorg.ContainsKey(borttagProdukt))
    {
    //Tar bort produkten från varukorgen och lägger tillbaka den i lager.
if (varukorg[borttagProdukt] > 1)
{
    varukorg[borttagProdukt]--;
}
else
{
    varukorg.Remove(borttagProdukt);
}

//Lägger tillbaka produkten i lager.
lager[borttagIndex]++;
totalPris -= priser[borttagIndex];

Console.WriteLine($"Produkten {borttagProdukt} har tagits bort från varukorgen.");
}
else
{
    Console.WriteLine($"Produkten '{borttagVal}' hittades inte.");
}
continue; //Gå tillbaka till huvudmenyn
}


//Felhantering: först kolla vad som har angetts i som valet menynummer, annars som produktnamn. Om inget av dessa finns i listan så visas ett felmeddelande.
int produktIndex = -1;
if (int.TryParse(val, out int menyVal))
{
    if (menyVal >= 1 && menyVal <= produkter.Count)
    {
        produktIndex = menyVal - 1;
    }
else

{
    Console.WriteLine($"Felaktigt menyval. Ange ett giltigt nummer mellan 1 och {produkter.Count}.");
    continue; //Gå tillbaka till huvudmenyn
}
}
else
{
 //Om inte produkten anges med en siffra söks den upp i listan.
 index = produkter.FindIndex(p => p.Equals(val, StringComparison.OrdinalIgnoreCase));
 if (index == -1)
 {
     Console.WriteLine($"Produkten '{val}' hittades inte.");
     continue; //Gå tillbaka till huvudmenyn
 }
}

//if-satsen kollar om produkten finns i lager. Om den finns i lager läggs den till i varukorgen och lagret minskas med 1. Om produkten inte finns i lager visas ett felmeddelande.
if (lager[produktIndex] > 0)
{
    //Lägger till priset i totalsumman och minskar antal produkter i lager.
    totalPris += priser[produktIndex];
    lager[produktIndex]--;

    //Lägger till produkten i varukorgen. Om produkten redan finns i varukorgen ökas antalet med 1.
if (varukorg.ContainsKey(produkter[produktIndex]))
    {
        varukorg[produkter[produktIndex]]++;
    }
    else
    {
        varukorg.Add(produkter[produktIndex], 1);
    }

   Console.WriteLine($"{produkter[produktIndex]} köpt för {priser[produktIndex]} kr ({lager[produktIndex]} st kvar i lager).");
}
else
{
    Console.WriteLine($"Produkten {produkter[produktIndex]} är slut i lager.");
}
}

//Kvitto:visar varukorgen med produkter, antal och totalpris.
Console.WriteLine();
Console.WriteLine("Kvitto:");
if (varukorg.Count > 0)
{
    foreach (var artikel in varukorg) //Räknar upp det man köpt i varukorgen och visar priset för varje produkt.
    {
        //Söker upp priset för produkten i listan med priser.
        int prisIndex = produkter.IndexOf(artikel.Key);
        Console.WriteLine($"{artikel.Key} - {artikel.Value} st - {priser[prisIndex] * artikel.Value} kr");
    }
}
else
{
    Console.WriteLine("Inga produkter var köpta.");
}
Console.WriteLine($"Total antal produkter: {varukorg.Values.Sum()}");
Console.WriteLine($"Du betalade totalt: {totalPris} kr");   
Console.WriteLine("Tack för ditt köp!");