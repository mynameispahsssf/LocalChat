////using System;
////using System.Threading;

////delegate void StringDelegate(string a);
////delegate void Dele();
////class PersonClass
////{
////    public static void SayHello(string HelloWerld)
////    {
////        Console.WriteLine(HelloWerld);
////    }
////    public static void VoidFunc()
////    {
////        Console.WriteLine("---you!!!---");
////    }
////}

////class Program
////{
////    public static void ThreadFunction(object a)
////    {
////        for (var f = 0; f < 10; f++) 
////        {
////            Console.WriteLine(f + "\t " + a);
////            Thread.Sleep(1000);
////        }
////    }

////    static void Main(string[] args)
////    {
////        PersonClass Andrey = new PersonClass();
////        StringDelegate AndreyHello = new StringDelegate(PersonClass.SayHello);
////        Dele yey = new Dele(PersonClass.VoidFunc);

////        AndreyHello("hi!");
////        yey();


////        Thread myThread = new Thread(new ParameterizedThreadStart(ThreadFunction));
////        myThread.Start("hello!");

////        Thread.Sleep(3000);

////        Thread myThread_1 = new Thread(new ParameterizedThreadStart(ThreadFunction));
////        myThread_1.Start("yo");





////        myThread_1.Join();
////        myThread.Join();

////        Console.ReadKey();
////    }
////}

//using System;
//using System.Threading;

//class MyAction
//{
//    public delegate void MethodContainer();

//    public event MethodContainer Oops;

//    public void StartWork()
//    {
//        for (var f = 0; f < 100; f++) 
//        {
//            Thread.Sleep(10);

//            if(f==95)
//            {
//                Oops();
//            }
//        }
//    }
//}

//class DoSomething
//{
//    public void Bob()
//    {
//        Console.WriteLine("Something is done");
//    }
//    static public void Pasha()
//    {
//        Console.WriteLine("thank you Bob");
//    }
//}


//class MyMain
//{
//    static void Main(string[] args)
//    {
//        MyAction doSomething = new MyAction();
//        DoSomething BOB = new DoSomething(); 

//        doSomething.Oops += BOB.Bob;
//        doSomething.Oops += DoSomething.Pasha;

//        doSomething.StartWork();

//        Console.ReadKey();
//    }
//}

