// Gör att man inte behöver skriva System. framför varje klass från System-namespacet.
using System;

// Gör det möjligt att använda List och Dictionary.
using System.Collections.Generic;

// Gör det möjligt att använda metoder som .All() och .Sum().
using System.Linq;


// Skapar listor för varor, priser och lagerstatus.
// Två parallalla listor
List<string> varor = new List<string>();
List<int> priser = new List<int>();

//Loop som körs tills användaren väljer att avsluta 
while (true)
{
// Visa aktuell inköpslista
VisaLista(varor, priser);
Console.WriteLine("Skriv in ett varunamn för att lägga till,
ett nummer för att ta bort eller 'q' för att avsluta: ");
string input = Console.ReadLine()!;

if (input.ToLower() == "q")
{
    Console.WriteLine("Avslutar programmet...");
    break;
{
if (int.TryParse(input, out int index))
    TaBortVara(varor, priser, index);
}
else
{
    LäggTillVara(varor, priser, input);
}
}
static void VisaLista(List<string> varor, List<int> priser)
{
    Console.WriteLine("\n--- INKÖPSLISTA ---");
}
if (varor.Count == 0)
{
    Console.WriteLine("Inga varor i listan.");
    return;
}
int summa = 0;
for (int i = 0; i < varor.Count; i++)
{
    Console.WriteLine($"{i + 1}. {varor[i]} - {priser[i]} kr");
    summa += priser[i];
}
Console.WriteLine($"Totalt pris: {summa} kr");
}
int totalPris = priser.Sum();
Console.WriteLine($"Totalt pris: {totalPris} kr");
}

// Visar alternativ
Console.WriteLine("\nSkriv in ett varunamn för att lägga till");
Console.WriteLine("Skriv in ett nummer för att ta bort en vara.");
Console.WriteLine("Skriv 'dyrast' för att se den dyraste varan.");
Console.WriteLine("Skriv 'sortera'för att sortera listan (a = alfabetisk, p = pris).");
Console.WriteLine("Tryck 'Enter' för att avsluta.");

Console.Write("> ");
string? input = Console.ReadLine() ??"";

// Avslutar programmet om användaren trycker Enter
if (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("Avslutar programmet...");
        break;
    }

}
// Tar bort en vara om användaren skriver in ett nummer
if (int.TryParse(input, out int number))
{
    if (number >= 1 && number <= varor.Count)
    {
        varor.RemoveAt(number - 1);
        priser.RemoveAt(number - 1);
        Console.WriteLine("Varan togs bort."); 
    }
    else
    {
        Console.WriteLine("Ogiltigt nummer.");
    }
    Console.WriteLine("Tryck Enter för att fortsätta...");
    Console.ReadLine();
    continue;
    }
    // Extra: visa vilken som är den dyraste varan
    if (input.ToLower() == "dyrast")
    {
        if (varor.Count == 0)
        {
            Console.WriteLine("Inga varor i listan.");  
        }
else
{
int maxIndex = priser.IndexOf(priser.Max());
Console.WriteLine($"Den dyraste varan är 
{varor[maxIndex]} - {priser[maxIndex]} kr.");
Console.ReadLine();
continue;
}
//Extra: sortera listan efter pris
if (input.Equals("sortera", StringComparison.OrdinalIgnoreCase))
    var indexLista = Enumerable.Range(0, priser.Count)
    OrderBy(i => priser[i])
    .ToList();
    }
}
Console.WriteLine("\nProgrammet avslutades. Välkommer åter!");
{
    break;
}
else if (input.Equals("dyrast", StringComparison.OrdinalIgnoreCase))
{
    if (varor.Count == 0)
    {
        Console.WriteLine("Inga varor i listan.");
    }
    else
    {
        int dyrastIndex = priser.IndexOf(priser.Max());
        Console.WriteLine($"Den dyraste varan är {varor[dyrastIndex]} - {priser[dyrastIndex]} kr");
    }
}
else if (input.Equals("sortera", StringComparison.OrdinalIgnoreCase))
{
    if (varor.Count == 0)
    {
        Console.WriteLine("Inga varor i listan.");
    }
    else
    {
        List<(string, int)> varorOchPriser = varor.Zip(priser, (vara, pris) => (vara, pris)).ToList();
        varorOchPriser.Sort((x, y) => x.Item2.CompareTo(y.Item2));
        varor = varorOchPriser.Select(x => x.Item1).ToList();
        priser = varorOchPriser.Select(x => x.Item2).ToList();
        Console.WriteLine("Varorna har sorterats efter pris.");
    }
}
else if (int.TryParse(input, out int index))
{
    index--;
    if (index >= 0 && index < varor.Count)
    {
        Console.WriteLine($"Tar bort {varor[index]} - {priser[index]} kr");
        varor.RemoveAt(index);
        priser.RemoveAt(index);
    }
    else
    {
        Console.WriteLine("Ogiltigt nummer.");
    }
}
else
{
    Console.WriteLine("Skriv in priset för varan:");
    string? prisInput = Console.ReadLine();
    if (int.TryParse(prisInput, out int pris))
    {
        varor.Add(input);
        priser.Add(pris);
        Console.WriteLine($"Lade till {input} - {pris} kr");
    }
    else
    {
        Console.WriteLine("Ogiltigt pris.");
    }
}

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




