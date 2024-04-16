// See https://aka.ms/new-console-template for more information
using ProjetTest;

Console.WriteLine("Hello, World!");

List<Arret> lesArrets = new List<Arret>
{
    new Arret("STADE DE L’EST", true, true, new List<string>{"15", "26", "27", "27A", "31", "33"}),
    new Arret("Parc des Expositions", true, false, new List<string> { "27", "27A", "33" }),
    new Arret("ZEC du Chaudron", true, false, new List <string> { "27", "27A", "33" }),
    new Arret("Manès", true, false, null),
    new Arret("Pierre Aubert", true, false, new List<string> { "27", "27A", "33" }),
    new Arret("Roger Payet", true, false, new List<string> { "27", "27A", "31", "33" }),
    new Arret("Station Chaudron", true, true, new List <string> { "15", "26", "27", "27A", "31", "33" }),
    new Arret("Mail du Chaudron", true, true, new List<string> { "1", "6", "8" }),
    new Arret("Lacroix", true, false, new List<string> { "1", "6", "8" }),
    new Arret("Sainte-Clotilde Centre", true, false, new List<string> { "1", "6", "7", "8" }),
    new Arret("Banian", true, false, new List<string> { "1", "6", "7", "8" }),
    new Arret("Deux Canons", true, false, new List<string> { "1", "6", "7", "8", "15" }),
    new Arret("Parc Aquatique", true, true, new List<string> { "1", "6", "7", "8", "10", "15", "19" }),
    new Arret("Butor", true, false, new List<string> { "1", "6", "7", "8" }),
    new Arret("Hôtel des Impôts", true, false, new List<string> { "1", "6", "7", "8" }),
    new Arret("Camp Jacquot", true, false, new List<string> { "1", "6", "7", "8" }),
    new Arret("Saint-Jacques", true, false, new List<string> { "1", "6", "7", "8" }),
    new Arret("Petit Marché", true, false, new List<string> { "6", "7", "8", "13", "14" }),
    new Arret("École Centrale", true, false, new List<string> { "6", "7", "8", "13", "14" }),
    new Arret("Rieul", true, false, new List<string> { "6", "7", "8", "13", "14" }),
    new Arret("HÔTEL DE VILLE DE SAINT-DENIS", true, true, new List<string> { "6", "7", "8", "10", "11", "12", "13", "14", "16", "19", "21", "22", "22A", "23" })
};


foreach(var arret in lesArrets)
{
    foreach (var correspondance in arret.Correspondances)
    {
        Console.WriteLine(correspondance.ToString());
    }
    Console.WriteLine(arret.Nom);
    Console.WriteLine(arret.EstCorrespondance);
    Console.WriteLine(arret.EstPoleEchange);
}

