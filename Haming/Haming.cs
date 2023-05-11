namespace Haming;

public class Haming
{
    #region fields

    private List<char> _bites;

    #endregion

    #region constructor

    public Haming(List<char> bites)
    {
        _bites = bites;
    }

    #endregion

    #region publicMethods

    public IEnumerable<List<char>> CorrectCode(List<char> message)
    {
        var groups = GetGroupsByTen(message);
        var correctedMessage = new List<char>();

        foreach (var group in groups)
        {
            var firstEquationResult = GetResult(group[0], group[2], group[4], group[6], group[8]) == 0;
            var secondEquationResult = GetResult(group[1], group[2], group[5], group[6], group[9]) == 0;
            var thirdEquationResult = GetResult(group[3], group[4], group[5], group[6]) == 0;
            var fourthEquationResult = GetResult(group[7], group[8], group[9]) == 0;

            if (firstEquationResult && secondEquationResult && thirdEquationResult && fourthEquationResult)
            {
                Console.WriteLine("Ошибок нет");
            }
            else if (firstEquationResult && secondEquationResult && thirdEquationResult)
            {
                Console.WriteLine("Ошибка в y4");
                group[7] = ChangeChar(group[7]);
            }
            else if (secondEquationResult && thirdEquationResult && fourthEquationResult)
            {
                Console.WriteLine("Ошибка в y1");
                group[0] = ChangeChar(group[0]);
            }
            else if (firstEquationResult && thirdEquationResult && fourthEquationResult)
            {
                Console.WriteLine("Ошибка в y2");
                group[1] = ChangeChar(group[1]);
            }
            else if (firstEquationResult && secondEquationResult && fourthEquationResult)
            {
                Console.WriteLine("Ошибка в y3");
                group[3] = ChangeChar(group[3]);
            }
            else if (firstEquationResult && thirdEquationResult)
            {
                Console.WriteLine("Ошибка в x6");
                group[9] = ChangeChar(group[9]);
            }
            else if (firstEquationResult && fourthEquationResult)
            {
                Console.WriteLine("Ошибка в x3");
                group[5] = ChangeChar(group[5]);
            }
            else if (secondEquationResult && thirdEquationResult)
            {
                Console.WriteLine("Ошибка в x5");
                group[8] = ChangeChar(group[8]);
            }
            else if (secondEquationResult && fourthEquationResult)
            {
                Console.WriteLine("Ошибка в x2");
                group[4] = ChangeChar(group[4]);
            }
            else if (thirdEquationResult && fourthEquationResult)
            {
                Console.WriteLine("Ошибка в x1");
                group[2] = ChangeChar(group[2]);
            }
            else if (firstEquationResult is false && secondEquationResult is false && thirdEquationResult is false &&
                     fourthEquationResult)
            {
                Console.WriteLine("Ошибка в x4");
                group[6] = ChangeChar(group[6]);
            }
            else
            {
                Console.WriteLine("Допущено более одной ошибки");
            }
            
            correctedMessage.AddRange(new []{group[2], group[4], group[5], group[6], group[8], group[9]});
        }

        Console.WriteLine();
        Console.WriteLine($"Исправленное сообщение: {new string(correctedMessage.ToArray())}");
        
        return groups;
    }
    
    public IEnumerable<List<char>> GetGroups()
    {
        var groups = new List<List<char>>();

        while (_bites.Count >= 6)
        {
            groups.Add(FillWithControlBits(_bites.Take(6).ToList()));
            _bites = _bites.Skip(6).ToList();
        }

        switch (_bites.Count)
        {
            case <= 0:
                return groups;
            case < 6:
                _bites.AddRange(Enumerable.Repeat('0', 6 - _bites.Count));
                _bites.Reverse();
                break;
        }

        groups.Add(FillWithControlBits(_bites));

        return groups;
    }

    #endregion

    #region privateMethods

    private static IEnumerable<List<char>> GetGroupsByTen(List<char> chars)
    {
        var groups = new List<List<char>>();

        while (chars.Count >= 10)
        {
            groups.Add(chars.Take(10).ToList());
            chars = chars.Skip(10).ToList();
        }

        return groups;
    }

    private static List<char> FillWithControlBits(List<char> bytes)
    {
        var firstControlByte = CalculateControlBit(bytes[0], bytes[1], bytes[3], bytes[4]);
        var secondControlByte = CalculateControlBit(bytes[0], bytes[2], bytes[3], bytes[5]);
        var thirdControlByte = CalculateControlBit(bytes[1], bytes[2], bytes[3]);
        var fourthControlByte = CalculateControlBit(bytes[4], bytes[5]);

        bytes.Insert(0, firstControlByte);
        bytes.Insert(1, secondControlByte);
        bytes.Insert(3, thirdControlByte);
        bytes.Insert(7, fourthControlByte);

        return bytes;
    }

    private static char CalculateControlBit(params char[] chars)
    {
        return Convert.ToChar(Convert.ToString(chars.Sum(bit => Convert.ToInt32(bit == '1')) % 2));
    }

    private static int GetResult(params char[] chars)
    {
        return chars.Sum(bit => Convert.ToInt32(bit == '1')) % 2;
    }

    private static char ChangeChar(char ch)
    {
        return ch == '1' ? '0' : '1';
    }

    #endregion
}