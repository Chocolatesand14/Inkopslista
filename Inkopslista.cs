//Raden hjälper till med att koden inte behöver specifiera ett systemnamn varje gång man använder en klass från System namespace.
using System;

//Raden hjälper för att skapa en inköpslista med varor, priser och lagerstatus. 
using System.Collections.Generic;

//Raden hjälper till med att använda metoder som t.ex. .All() och .Sum() för att arbeta med listor.
using System.Linq;


//Skapar listor för varor, priser och lagerstatus.
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


//Varukorg med vilken vara som köpts och hur många.
Dictionary<string, int> varukorg = new Dictionary<string, int>();
int totalPris = 0;

//Rubrik för inköpslistan.
Console.WriteLine("==============================");
Console.WriteLine("        INKÖPSLISTA");
Console.WriteLine("==============================");

//Loopar tills alla varor är slut i lager och varukorgen är tom.
while (!lager.All(x => x == 0) || varukorg.Count > 0)
{

//Visar alla varor, priser och lagerstatus
    Console.WriteLine("\nVaror:");

    for (int i = 0; i < varor.Count; i++)
    {

//Om varan skulle vara slutsåld så visas detta istället.
        string status = lager[i] > 0
            ? $"{lager[i]} st i lager"
            : "SLUT I LAGER";

        Console.WriteLine(
            $"{i + 1}. {varor[i]} - {priser[i]} kr - {status}"
        );
    }


    //Visar vad som ligger i den aktuella varukorgen.
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
//Visas om varukorgen skulle vara tom
        Console.WriteLine("\nVarukorgen är tom.");
    }

//Låter kunden navigera i menyn med en siffra eller bokstavera menyvalet.
    Console.WriteLine(
        "\nSkriv varans namn, nummer eller 'borttag'. Enter = avsluta"
    );

//Felhantering för tomma strängar och null.
    string? val = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(val))
    {
        break;
    }


//Borttag tar bort en vara i varukorgen och den läggs tillbaka i lager.
    if (val.Equals("borttag", StringComparison.OrdinalIgnoreCase))
    {
        if (varukorg.Count == 0)
        {
//Om varukorgen är tom och det inte heller går att ta bort en vara visas den här felhanteringen.
            Console.WriteLine("Varukorgen är tom.");
            continue;
        }

//Användaren kan välja att ta bort en vara med nummer eller namn på varan.
        Console.Write("Vilken vara vill du ta bort? ");
        string? borttagVal = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(borttagVal))
        {
            continue; //Åter till huvudmenyn
        }

//Letar upp varan som ska tas bort.
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


    //Hitta varan som kunden valt
    int varanIndex = -1;

    if (int.TryParse(val, out int valtNummer))
    {
        if (valtNummer >= 1 && valtNummer <= varor.Count)
        {
            varanIndex = valtNummer - 1;
        }
        else
        {
            //Felhantering om kunden råkar välja ett nummer som inte finns i menyn.
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


    //Kunden ska köpa och betala för sina varor. 
    //Om varan skulle vara slut i lager visas det som en felhantering med ett meddelande.
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


//Om kunden har köpt och betalt för sina varor syns det i kvittot med namnet på varan, antal varor och vad totalpriset blev.
//Om kunden inte har köpt något, så visas det som att inga varor var köpta.

int kvittoNummer = Random.Shared.Next(10000, 99999);
DateTime datum = DateTime.Now;

//Efter har kollat på ett kvitto hemma, valde jag att piffa upp mitt kvitto lite med vad som finns på ett riktigt kvitto.
//Det jag la till var kvittonummer, dagens datum och vilken tid som köpet gjordes.
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
    //Felhantering om inga varor var köpta.
    Console.WriteLine("Inga varor var köpta.");
}


Console.WriteLine("------------------------------");
Console.WriteLine($"Antal varor: {varukorg.Values.Sum()}");
Console.WriteLine($"Totalt: {totalPris} kr");
Console.WriteLine("==============================");
//Valde att avsluta kvitto med öppettider och ett välkommer åter.
Console.WriteLine("Öppet hela dygnet, alla dagar i veckan");
Console.WriteLine("Välkommen åter!");




