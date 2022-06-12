
class Program
{
    static void Main(string[] args)
    {
        bool exit = false;
         
            string dirpath = "";
            dirpath = inputpath(args);
            while (!pathvalid(dirpath))
            {
                Console.WriteLine("the directory:" + dirpath + " is invalid.");
                dirpath = inputpath(args);
            }
            DateTime compareDate = Convert.ToDateTime("01 Jan 1900");
            bool datevalid = false;
            while (!datevalid)
            {
                try
                {
                    compareDate = DateTime.Parse(inputdate());
                    datevalid = true;
                }
                catch (Exception ex)
                {
                    DateTime newdate = DateTime.Now.AddMonths(-3);
                    Console.WriteLine("you have inputted invalid date, would you use " + newdate.ToString() + "instead? (Y/N)");
                    String answer = Console.ReadLine().ToUpper();
                    if (answer == "Y")
                    {
                        compareDate = newdate;
                        datevalid = true;
                    }
                    else if (answer == "N")
                    {
                        Console.WriteLine("allright.");
                        datevalid = false;
                    }
                    else
                    {
                        Console.WriteLine("unrecognized command");
                        datevalid = false;
                    }

                }
            }
            int datacount = files(dirpath, compareDate, "display");
            if (datacount > 0)
            {
                Console.WriteLine("there are " + datacount.ToString() + " File(s). are you sure want to delete these file(s)?(Y/N)");
                var deleteanswer = Console.ReadLine().ToUpper();
                if (deleteanswer == "Y")
                {
                    files(dirpath, compareDate, "delete");
                }
                else if (deleteanswer == "N")
                {
                    Console.WriteLine("allright.");
                }
                else
                {
                    Console.WriteLine("unrecognized command");
                }
            }
            else
            {
                Console.WriteLine("there are no files;");
        }
        Console.WriteLine("press any key to exit.......");
        Console.ReadKey(); 
        

    }
    static int files(string dirpath, DateTime compareDate, string action)
    {

        string[] filelist = Directory.GetFiles(dirpath, "*.txt");
        string filepath;
        int datafound = 0;
        int datadeleted = 0;
        if (filelist.Count() == 0)
        {
            Console.WriteLine("No file found with filter: [*.txt] AND CreationDate < " + compareDate.ToString() + "");
        }
        else
        {
            for (int i = 0; i < filelist.Count(); i++)
            {
                FileInfo fi = new FileInfo(filelist[i]);
                if (fi.CreationTime < compareDate && fi.Extension == ".txt")
                {
                    datafound = datafound + 1;
                    if (action == "display")
                    {
                        Console.WriteLine("[creation date:" + fi.CreationTime.ToString() + "]" + fi.FullName);
                    }
                    if (action == "delete")
                    {
                        File.Delete(filelist[i]);
                        try
                        {
                            File.Delete(filelist[i]);
                            Console.WriteLine(fi.FullName + ": Deleted");
                            datadeleted = datadeleted + 1;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(fi.FullName + ": Error occured.");
                        }

                    }
                }
            }
            Console.WriteLine(datafound.ToString() + " File(s) Found. " + datadeleted.ToString() + " File(s) Deleted.");
        }
        return datafound;
    }

    static string inputdate()
    { 
        Console.WriteLine("please enter a date[dd/mm/yyyy], any text files older than this date will be deleted:");
        var dateentered = Console.ReadLine();  
        return dateentered.ToString(); 
    }
    static string inputpath(string[] args)
    {
        //args = new string[] { null, "f:\\luminarytest\\" };
        args = new string[] { null, null };
        string result = "";
        if (args[1] == null)
        {
            Console.WriteLine("please insert a directory path:");
            result = Console.ReadLine();
        }
        else
        {
            result = args[1];
        }
        return result;
    }
    static bool pathvalid(string path)
    {
        bool result = false;
        DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath(path));
        if (dir.Exists)
        {
            return true;

        }
        return result;
    }

}