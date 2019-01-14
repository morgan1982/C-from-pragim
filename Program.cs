using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class UserAllreadyLoggedInException : Exception
{
    public UserAllreadyLoggedInException() : base()
    {

    }
    public UserAllreadyLoggedInException(string message) :base(message)
    {

    }
    // constructor to catch inner exceptions
    public UserAllreadyLoggedInException(string message,  Exception innerException) :base(message, innerException)
    {

    }
    public UserAllreadyLoggedInException(SerializationInfo info, StreamingContext context)
    : base(info, context)
    {

    }

}

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            try{
                int fn = 4;
                int sn = 0;
                int res = fn / sn;
                System.Console.WriteLine(res);

            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex);
                throw new UserAllreadyLoggedInException("cannot login.allready logged in", ex);
            }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine("ex: {0}  inner: {1}",ex, ex.InnerException);
        }
    }
}