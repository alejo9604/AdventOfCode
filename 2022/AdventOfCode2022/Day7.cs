using System;
using System.Collections.Generic;
using System.Text;

namespace AllieJoe.AdventOfCode2022
{
    public class Day7
    {
        public void Run(string input)
        {
            string[] lines = input.Split("\n");
            
            Folder filesystem = new Folder("/"); 
            ProcessInput(lines, filesystem);

            //Console.WriteLine(filesystem.Print());
            
            filesystem.CalculateSize();
            
            Part2(filesystem);
        }

        private void ProcessInput(string[] lines, Folder filesystem)
        {
            const string START_COMMAND_CHAR = "$";
            const string COMMAND_MOVE = "cd";
            const string COMMAND_FILE_INFO = "ls";
            const string COMMAND_FOLDER_PARENT = "..";
            const string FOLDER_DIRECTIVE = "dir";

            Folder currentFolder = filesystem;
            
            for (int i = 0; i < lines.Length; i++)
            {
                //Console.WriteLine(lines[i]);
                string[] commands = lines[i].Split(' ');
                
                if (commands[0] == START_COMMAND_CHAR)
                {
                    if (commands[1] == COMMAND_MOVE)
                    {
                        string folderName = commands[2];
                        if (folderName == COMMAND_FOLDER_PARENT)
                        {
                            currentFolder = currentFolder.parentFolder;
                        }
                        else
                        {
                            currentFolder = currentFolder.GetFolder(folderName);
                        }
                    }
                    else if (commands[1] == COMMAND_FILE_INFO)
                    {
                        //Ignore - we're gonna add files. Maybe we can handle that in a inner loop?
                    }
                }
                else if (commands[0] == FOLDER_DIRECTIVE)
                {
                    currentFolder.AddFolder(commands[1]);
                }
                else if (int.TryParse(commands[0], out int fileSize))
                {
                    currentFolder.AddFile(commands[1], fileSize);
                }
            }
        }

        private void Part1(Folder filesystem)
        {
            Folder currentFolder = null;
            long totalSize = 0;
            
            Queue<Folder> foldersToCheck = new Queue<Folder>();
            foldersToCheck.Enqueue(filesystem);
            while (foldersToCheck.Count > 0)
            {
                currentFolder = foldersToCheck.Dequeue();
            
                if (currentFolder.parentFolder != null && currentFolder.totalSize < 100000)
                {
                    totalSize += currentFolder.totalSize;
                }
                
                foreach (Folder subFolder in currentFolder.subFolders.Values)
                {
                    foldersToCheck.Enqueue(subFolder);
                }
            }
            
            Console.WriteLine(totalSize);
        }

        private void Part2(Folder filesystem)
        {
            int totalAvailableSpace = 70000000;
            int freeSpace = totalAvailableSpace - filesystem.totalSize;
            int requiredSpace = 30000000 - freeSpace;
            //Console.WriteLine(requiredSpace); //5,174,025
            
            Folder currentFolder = null;
            int spaceToRemove = int.MaxValue;
            
            Queue<Folder> foldersToCheck = new Queue<Folder>();
            foldersToCheck.Enqueue(filesystem);
            while (foldersToCheck.Count > 0)
            {
                currentFolder = foldersToCheck.Dequeue();
            
                if (currentFolder.parentFolder != null && currentFolder.totalSize >= requiredSpace && currentFolder.totalSize < spaceToRemove)
                {
                    spaceToRemove = currentFolder.totalSize;
                }
                
                foreach (Folder subFolder in currentFolder.subFolders.Values)
                {
                    foldersToCheck.Enqueue(subFolder);
                }
            }
            
            Console.WriteLine(spaceToRemove);
        }
        
        class Folder
        {
            public string name;
            public Dictionary<string, int> files;
            public Dictionary<string, Folder> subFolders;
            public Folder parentFolder;
            public int totalSize;

            public Folder(string name)
            {
                this.name = name;
                files = new Dictionary<string, int>();
                subFolders = new Dictionary<string, Folder>();
                parentFolder = null;
            }
            
            public Folder(string name, Folder parentFolder)
            {
                this.name = name;
                files = new Dictionary<string, int>();
                subFolders = new Dictionary<string, Folder>();
                this.parentFolder = parentFolder;
            }

            public void AddFile(string fileName, int size)
            {
                files[fileName] = size;
            }

            public void AddFolder(string folderName)
            {
                if (subFolders.ContainsKey(folderName))
                {
                    Console.WriteLine($"SubFolder with the same name: {folderName}");
                }

                subFolders[folderName] = new Folder(folderName, this);
            }

            public Folder GetFolder(string folderName)
            {
                if(subFolders.ContainsKey(folderName))
                    return subFolders[folderName];
                
                return this;
            }

            public void CalculateSize()
            {
                totalSize = 0;
                foreach (int size in files.Values)
                {
                    totalSize += size;
                }
                
                foreach (Folder subFolder in subFolders.Values)
                {
                    subFolder.CalculateSize();
                    totalSize += subFolder.totalSize;
                }
            }

            public string Print(int level = 0)
            {
                string tab = "";
                for (int i = 0; i < level; i++)
                {
                    tab += "  ";
                }
                string txt = $"{tab}- {name} (dir)";

                foreach (Folder subFolder in subFolders.Values)
                {
                    txt += "\n";
                    txt += subFolder.Print(level + 1);
                }

                // foreach (string fileName in files.Keys)
                // {
                //     txt += $"\n{tab}  - {fileName} (file, size={files[fileName]})";
                // }

                return txt;
            }
        }
    }
}