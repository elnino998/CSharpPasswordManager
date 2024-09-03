using System;
using System.Collections.Generic;
using System.Text;
using PasswordFunctions; 

namespace Actions
{
    internal class Cases
    {
        public static int Intro() 
        {
            Console.WriteLine("What would you like to do?\n1. Generate a random password\n2. Add a new password entry\n3. Delete a password entry\n4. Read from a password file");
            string input = Console.ReadLine();
            int choice = Convert.ToInt32(input);
            return choice; 
        }
        public static void Outro()
        {
            Console.WriteLine("Would you like to do something else? y/n");
            char input = Console.ReadLine()[0];
            Repeat(input); 

        }
        public static void Repeat(char x)
        {

            if (x == 'y' || x == 'Y')
            {
                int y = Intro();
                Selection(y); 
            }
            else
            {
                return; 
            }
        }
        public static void Selection(int x)
        {
            switch (x)
            {
                case 1: 
                    PassFunctions.GeneratePassword();
                    break; 
                case 2:
                    PassFunctions.AddNewPassword();
                    break; 
                case 3:
                    PassFunctions.DeletePassword(); 
                    break;
                case 4:
                    PassFunctions.ReadPassword(); 
                    break;
            }
            Outro(); 
        }
        
    }
}
