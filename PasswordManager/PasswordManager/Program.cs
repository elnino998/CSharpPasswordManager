using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using Actions;

namespace password_manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"
                                                       _                                             
                                                      | |                                            
             _ __   __ _ ___ _____      _____  _ __ __| |  _ __ ___   __ _ _ __   __ _  __ _  ___ _ __ 
            | '_ \ / _` / __/ __\ \ /\ / / _ \| '__/ _` | | '_ ` _ \ / _` | '_ \ / _` |/ _` |/ _ \ '__|
            | |_) | (_| \__ \__ \\ V  V / (_) | | | (_| | | | | | | | (_| | | | | (_| | (_| |  __/ |   
            | .__/ \__,_|___/___/ \_/\_/ \___/|_|  \__,_| |_| |_| |_|\__,_|_| |_|\__,_|\__, |\___|_|   
            | |                                                                       __ / /          
            |_|                                                                       |___/           
                ");

            int choice = Cases.Intro();
            Cases.Selection(choice);
        }
    }
}