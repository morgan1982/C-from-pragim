using System;
using System.IO;



class Program
{
    public static void Main(string[] args)
    {
        StreamReader streamReader = null;
        try{
            streamReader = new StreamReader("./text12.txt");
            Console.WriteLine(streamReader.ReadToEnd());
        }
        catch(FileNotFoundException ex)
        {
            System.Console.WriteLine("Make sure the file {0} exist", ex.FileName);
        }
        // put the general exception to the bottom of the block
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        // if there is an exception the streamReader will not close and will not release the resources
        // thus the streamReader is terminated inside the finnaly block
        finally {
            if (streamReader != null)
            {
                streamReader.Close();
            }
        }



    }



}

