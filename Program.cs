using System;

interface I1
{
    void interMethod();
}

interface I2
{
    void interMethod();
}




public class Program : I1, I2
{

    void I1.interMethod()
    {
        System.Console.WriteLine("interface 1 method");
    }
    void I2.interMethod()
    {
        System.Console.WriteLine("interface 2 method");
    }
    public static void Main()
    {
        // When the class inherits from 2 interfaces and both intefaces have the same
        // method name explicit interface implementation is used to implement the method
        Program P = new Program();
        // if the P.Interface needs to be called -- default inteface implementation
        // change the first method to public void InterfaceMethod()
        ((I1)P).interMethod();
        ((I2)P).interMethod();        
    }
}
