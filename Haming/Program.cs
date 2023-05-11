Console.Write("Введите отправленное бинарное сообщение: ");
var bites = Console.ReadLine();

ArgumentNullException.ThrowIfNull(bites);

var haming = new global::Haming.Haming(bites.ToCharArray().ToList());

Console.WriteLine("| Y1 | Y2 | X1 | Y3 | X2 | X3 | X4 | Y4 | X5 | X6 |");
foreach (var group in haming.GetGroups())
{
    Console.Write("|");
    foreach (var ch in group)
    {
        Console.Write($"  {ch} |");
    }
    Console.WriteLine();
}

Console.WriteLine();
Console.Write("Введите полученное бинарное сообщение: ");
var message = Console.ReadLine();
Console.WriteLine();

ArgumentNullException.ThrowIfNull(message);

var groups = haming.CorrectCode(message.ToCharArray().ToList());
Console.WriteLine();
Console.WriteLine("| Y1 | Y2 | X1 | Y3 | X2 | X3 | X4 | Y4 | X5 | X6 |");

foreach (var group in groups)
{
    Console.Write("|");
    foreach (var ch in group)
    {
        Console.Write($"  {ch} |");
    }
    Console.WriteLine();
}
