// See https://aka.ms/new-console-template for more information
bool run = true;
while (run)
{
    Console.WriteLine("Enter a set of bits: ");
    bool isInputting = true;
    List<string> characterBits = new List<string>();

    do 
    {
        characterBits.Add(Console.ReadLine());
        Console.WriteLine("Do you want to enter another set of bits? (y/n)");
        char answer = char.Parse(Console.ReadLine());
        if (answer == 'n')
        {
            isInputting = false;
        }
    }while(isInputting);
    
    if(characterBits.Count > 1)
    {
        Console.WriteLine("Actions:");
        Console.WriteLine("1. Calculate BCC");
        Console.WriteLine("2. Check Error with BCC");
        Console.WriteLine("3. Calculate BCC with Parity");
        Console.WriteLine("4. End program");
        int choice = int.Parse(Console.ReadLine());
        try
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine(CalculateBCC(characterBits));
                    break;

                case 2:
                    Console.WriteLine("Enter BCC:");
                    string bcc = Console.ReadLine();
                    if(CheckErrorWithBCC(characterBits, bcc))
                    {
                        Console.WriteLine("Wrong BCC!");
                    }
                    else
                    {
                        Console.WriteLine("BCC is correct!");
                    }
                    
                    break;
                case 3:
                    Console.WriteLine("is the parity EVEN? (y/n)");
                    if(Console.ReadLine() == "y")
                    {
                        Console.WriteLine(CalculateBCCWithParity(characterBits, true));
                    }
                    else
                    {
                        Console.WriteLine(CalculateBCCWithParity(characterBits, false));
                    }
                    break;
                case 4:
                    run = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                    break;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid choice.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    else
    {
        Console.WriteLine("Actions:");
        Console.WriteLine("1. Check Parity and Error");
        Console.WriteLine("2. End program");
        int choice = int.Parse(Console.ReadLine());
        try
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("is the parity EVEN? (y/n)");
                    if (Console.ReadLine() == "y")
                    {   
                        if(CheckParityAndError(characterBits, true))
                        {
                            Console.WriteLine("Inputted bits is not even!");
                        }
                        else
                        {
                            Console.WriteLine("Inputted bits is even!");
                        }
                    }
                    else
                    {
                        if (CheckParityAndError(characterBits, false))
                        {
                            Console.WriteLine("Inputted bits is not odd!");
                        }
                        else
                        {
                            Console.WriteLine("Inputted bits is odd!");
                        }
                    }
                    break;

                case 2:
                    run = false;
                    break;
                   
                default:
                    Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                    break;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid choice.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

}

static bool CheckParityAndError(List<string> inputBits, bool isEvenParity)
{
    int countOfOnes = CountOnes(inputBits[0]);
    return (isEvenParity && countOfOnes % 2 != 0) || (!isEvenParity && countOfOnes % 2 == 0);
}

static string CalculateBCC(List<string> characterBits)
{
    int bitLength = characterBits[0].Length;
    string bcc = "";

    for (int i = 0; i < bitLength; i++)
    {
        int countOfOnes = 0;

        foreach (string bits in characterBits)
        {
            if (bits[i] == '1')
            {
                countOfOnes++;
            }
        }

        bcc += (countOfOnes % 2 == 0) ? "0" : "1";
    }

    return bcc;
}

static bool CheckErrorWithBCC(List<string> characterBits, string bcc)
{
    string calculatedBCC = CalculateBCC(characterBits);
    return calculatedBCC != bcc;
}

static string CalculateBCCWithParity(List<string> characterBits, bool isEvenParity)
{
    string bcc = CalculateBCC(characterBits);
    return isEvenParity ? AddEvenParity(bcc) : AddOddParity(bcc);
}


static int CountOnes(string binaryString)
{
    int count = 0;
    foreach (char bit in binaryString)
    {
        if (bit == '1')
        {
            count++;
        }
    }
    return count;
}

static string AddEvenParity(string input)
{
    return input + (CountOnes(input) % 2 == 0 ? "0" : "1");
}

static string AddOddParity(string input)
{
    return input + (CountOnes(input) % 2 == 0 ? "1" : "0");
}