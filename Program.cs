using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading.Channels;

internal class Program
{
    static bool IsValidName(string name)
    {

        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        if (name.Length < 2 || name.Length > 50)
        {
            return false;
        }
        if (name.Any(char.IsDigit))
        {
            return false;
        }
        return true;
    }
        
    
    static bool CheckInputNumbers(string number, out int corrNumber)
    {
        if (int.TryParse(number, out int intNumber))
        {
            if (intNumber > 0 && intNumber < 120)
            {
                corrNumber = intNumber;
                return true;
            }
        }
        {
            corrNumber = 0;
            Console.WriteLine("Please use digits! ");
            return false;
        }   
    }

    static void PrintUserInfo()
    {
        var showInfo = UserInfo();
        Console.WriteLine("User:");
        Console.WriteLine($"{showInfo.Name} + {showInfo.LastName} + {showInfo.Age} years old");
        Console.WriteLine($"Pets: {showInfo.Pets}");
        Console.WriteLine($"Favorite colors: {showInfo.FavColors}");
    }
    //Деконструировать  или так оставить?
    
    static string [] UserPets ()
    {

        string petsNum; int numberPets;
        do
        {
            Console.Write("How many pets do you have?");
            petsNum = Console.ReadLine();
        }
        while (!(CheckInputNumbers(petsNum, out numberPets)));

        string [] pets = new string [numberPets];
       
        for (int i = 0; i < numberPets; i++)
        {
            Console.WriteLine("Write your pet's name:");
            pets[i] = Console.ReadLine();
        }
        return pets;
    }

    static string[] UserFavColors()
    {
        
        string strColor; int numberColor;
        do
        {
            Console.WriteLine("How many favorite colors do you have? ");
            strColor  = Console.ReadLine();
        }
        while(!(CheckInputNumbers( strColor, out numberColor)));

        string[] favcolors = new string[numberColor];
        

        for (int i = 0; i < favcolors.Length; i++)
        {
            Console.WriteLine("Write your {0} favorite color: ", i + 1);
            favcolors[i] = Console.ReadLine();
        }
        return favcolors;
    }
    static  (string Name, string LastName, int Age, string[] Pets, string [] FavColors) UserInfo()
    {

        (string Name, string LastName, int Age, string[] Pets, string[] FavColors) User;
        //Нужно ли тут через User работать или же достаточно было бы просто назначать в обычные поля данные ?
        User.Name = "";     User.LastName = "";

        do
        {
            Console.Write("Write your name: ");
            User.Name = Console.ReadLine();

            Console.Write("Write your last name: ");
            User.LastName = Console.ReadLine();

            if (IsValidName(User.Name) && IsValidName(User.LastName)==true)
            {
                var prevConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Okey, let's go futher!");
                Console.ForegroundColor = prevConsoleColor;
            }
            else
            {
                var prevConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: First and last name must contain only letters and be between 2 and 50 characters long.");
                Console.ForegroundColor = prevConsoleColor;
                
            }

        }
        while (!(IsValidName(User.Name) && IsValidName(User.LastName)));
        
        string age; int intAge;
        do
        {
            Console.WriteLine("Write your age: ");
            age = Console.ReadLine();
        }
        while (!(CheckInputNumbers(age, out intAge)));

        User.Age = intAge;

        Console.WriteLine("Do you have a pet? Write: [yes]  or [no] ");
        string hasPet = Console.ReadLine();

        if (hasPet.ToLower() == "yes")
        {
            User.Pets = UserPets();
        }
        else 
        {
            User.Pets = new string[1] { "You don't have pets" };
        }
        // Нужны ли такие проверки или достаточно  все вывести в значения кол-ва питомцев и если их нет,
        // то просить или присваивать 0 . А на вывод при условии 0 выдавать сообщение ?

        Console.WriteLine("Do you have a favor colors? Write: [yes] or [no] ");
        string hasFavColors = Console.ReadLine();

        if (hasFavColors.ToLower() == "yes")
        {
             User.FavColors = UserFavColors();
        }
        else
        {
            User.FavColors = new string[1] { "You don't have  favorite colors" };
        }

        return User;    
    }
    private static void Main(string[] args)
    {
        UserInfo();
        PrintUserInfo();
        Console.ReadKey();
    }
}