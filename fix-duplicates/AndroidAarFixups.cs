using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace AarLibsFix
{
    public static class AndroidAarFixups
    {
        public static void FixupAarClass(string filename, string artName)
        {
            using (var fileStream = new FileStream(filename, FileMode.Open))
            using (var zipArchive = new System.IO.Compression.ZipArchive(fileStream, ZipArchiveMode.Update, true))
            {
                var entryNames = zipArchive.Entries.Select(zae => zae.FullName).ToList();

                Console.WriteLine("Found {0} entries in {1}", entryNames.Count, filename);

                foreach (var entryName in entryNames)
                {
                    var newName = entryName;

                    // Open the old entry
                    var oldEntry = zipArchive.GetEntry(entryName);
                    // We are only re-adding non empty folders, otherwise we end up with a corrupt zip in mono
                    if (!string.IsNullOrEmpty(oldEntry.Name))
                    {

                        // UGLY WORKAROUND
                        // In the some of the native Huawei libraries, there exist multiple .aar files which have a libs/r-classes.jar file.
                        // In Xamarin.Android, there is a Task "CheckDuplicateJavaLibraries" which inspects jar files being pulled in from .aar files
                        // in assemblies to see if there exist any files with the same name but different content, and will throw an error if it finds any.
                        // However, for us, it is perfectly valid to have this scenario and we should not see an error.
                        var newFile = Path.GetFileName(newName);
                        var newDir = Path.GetDirectoryName(newName);

                        if (newFile.StartsWith("r", StringComparison.InvariantCulture))
                            newName = newDir + "/" + "r-" + artName + ".jar";

                        Console.WriteLine("Renaming: {0} to {1}", entryName, newName);

                        // Create a new entry based on our new name
                        var newEntry = zipArchive.CreateEntry(newName);


                        // Copy file contents over if they exist
                        if (oldEntry.Length > 0)
                        {
                            using (var oldStream = oldEntry.Open())
                            using (var newStream = newEntry.Open())
                            {
                                oldStream.CopyTo(newStream);
                            }
                        }

                    }

                    // Delete the old entry regardless of if it's a folder or not
                    oldEntry.Delete();
                }

            }
        }


        public static void FixupAarResource(string filename, string artName)
        {
            using (var fileStream = new FileStream(filename, FileMode.Open))
            using (var zipArchive = new System.IO.Compression.ZipArchive(fileStream, ZipArchiveMode.Update, true))
            {
                var entryNames = zipArchive.Entries.Select(zae => zae.FullName).ToList();

                Console.WriteLine("Found {0} entries in {1}", entryNames.Count, filename);

                foreach (var entryName in entryNames)
                {
                    var newName = entryName;

                    // Open the old entry
                    var oldEntry = zipArchive.GetEntry(entryName);
                    // We are only re-adding non empty folders, otherwise we end up with a corrupt zip in mono
                    if (!string.IsNullOrEmpty(oldEntry.Name))
                    {
                        var newFile = Path.GetFileName(newName);
                        var newDir = Path.GetDirectoryName(newName);

   
                        //Fix R.text in different .aar
                        if (newFile.Contains("R.txt"))
                            newName = newDir + "/" + "R-" + artName + ".txt";

                        Console.WriteLine("Renaming: {0} to {1}", entryName, newName);

                        // Create a new entry based on our new name
                        var newEntry = zipArchive.CreateEntry(newName);


                        // Copy file contents over if they exist
                        if (oldEntry.Length > 0)
                        {
                            using (var oldStream = oldEntry.Open())
                            using (var newStream = newEntry.Open())
                            {
                                oldStream.CopyTo(newStream);
                            }
                        }

                    }

                    // Delete the old entry regardless of if it's a folder or not
                    oldEntry.Delete();
                }

            }
        }
    }
}