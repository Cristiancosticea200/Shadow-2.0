using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace ShadowFN
{
    internal class Hybrid
    {
        private static void MainHybrid(string[] args)
        {
            Console.WriteLine("FortniteLauncher made by cristi");
            new Thread(new ThreadStart(Listener.Listen)).Start();
            if (!System.IO.File.Exists("Aurora.Runtime.dll"))
                new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/784567894347743252/848674876176859176/Aurora.Runtime.dll", "Aurora.Runtime.dll");
            if (!System.IO.File.Exists("DefaultGame.ini"))
                new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/812396824903155774/848319658187685949/DefaultGame-6.ini", "DefaultGame.ini");
            string str = "com.epicgames.launcher://apps/Fortnite?action=launch&silent=true";
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Fortnite_Launcher"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Fortnite_Launcher");
            if (!System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Fortnite_Launcher\\FortniteClient-Win64-Shipping_BE.exe"))
            {
                System.IO.File.Move(str + "\\FortniteClient-Win64-Shipping_BE.exe", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Fortnite_Launcher\\FortniteClient-Win64-Shipping_BE.exe");
                System.IO.File.Move(str + "\\FortniteClient-Win64-Shipping_EAC.exe", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Fortnite_Launcher\\FortniteClient-Win64-Shipping_EAC.exe");
            }
            new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/784567894347743252/848661120424214548/NeoSplash.exe", str + "\\FortniteClient-Win64-Shipping_EAC.exe");
            new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/784567894347743252/848661120424214548/NeoSplash.exe", str + "\\FortniteClient-Win64-Shipping_BE.exe");
            Process.Start("com.epicgames.launcher://apps/Fortnite?action=launch&silent=true");
            int num = (int)MessageBox.Show("Press ok when you're in fortnite lobby");
            Process process = Process.GetProcessesByName("FortniteClient-Win64-Shipping")[0];
            Injector.Inject(process.Id, Directory.GetCurrentDirectory() + "\\Aurora.Runtime.dll");
            process.WaitForExit();
            Thread.Sleep(7000);
            System.IO.File.Delete(str + "\\FortniteClient-Win64-Shipping_EAC.exe");
            System.IO.File.Delete(str + "\\FortniteClient-Win64-Shipping_BE.exe");
            System.IO.File.Move(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Fortnite_Launcher\\FortniteClient-Win64-Shipping_BE.exe"), str + "\\FortniteClient-Win64-Shipping_BE.exe");
            System.IO.File.Move(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Fortnite_Launcher\\FortniteClient-Win64-Shipping_EAC.exe"), str + "\\FortniteClient-Win64-Shipping_EAC.exe");
            System.IO.File.Delete("DefaultGame.ini");
            System.IO.File.Delete("Aurora.Runtime.dll");
            Environment.Exit(0);
        }
    }
}