using System;
// a class is the most common kind of reference type
// A constant is evaluated statically at compile time, and the compiler literally substitutes its value whenever used (rather like a macro in C++).
// A constant can be bool, char, string, any of the built-in numeric types, or an enum type.

/* A constant can serve a similar role to a static readonly field, but it is much more
restrictive—both in the types you can use and in field initialization semantics. A
constant also differs from a static readonly field in that the evaluation of the
constant occurs at compile time;  */

// a value should only be made constant if its value can be predetermined at compile time.
// a static readonly field's value can potentially differ every time the program is run

/*   A method that comprises a single expression, such as
int Foo (int x) { return x * 2; }
can be written more tersely as an expression-bodied method. A fat arrow replaces the
braces and return keyword:
int Foo (int x) => x * 2;
Expression-bodied functions can also have a void return type:
void Foo (int x) => Console.WriteLine (x);   */

// C# allows us to write methods within methods, this can't be done in C++ without lambdas

// When an expression references a readonly field, the value is not obtained until runtime. Change in the value is reflected immediately, the assembly does not need to be recompiled.

public class Test
{
    public const string Message = "Hello World!";  // this cannot be accessed via an instance
}

/* a const field is essentially static in its behavior. It belongs to the type, not to instances of the type. Therefore, const fields can be accessed by using the same ClassName.MemberName notation that's used for static fields  */
// Adding the static modifier to a local method (from C# 8) prevents it from seeing the local variables and parameters of the enclosing method.
// This helps to reduce coupling and prevents the local method from accidentally referring to variables in the containing method.
// top level methods are treated as local methods by the rest of the program

// the return type and prams modifier are not part of the methods signature. Pass by value or pass by reference is also part of the signature

// constructors can be overloaded and overloaded constructors can call each other

// like C++. For classes, the C# compiler automatically generates a parameterless public constructor if and only if you do not define any constructors. However, as soon as
//  you define at least one constructor, the parameterless constructor is no longer automatically generated.

// class field initialisations happen before the constructor is called.

// constructors do not need to be public.  A common reason to have a nonpublic constructor is to control instance creation via a static method call.

// C# also has destructors, which take member fields and assigns them to out parameters. This is the opposite of a constructor 

/*  You can use C#’s discard symbol (_) if you’re uninterested in one or more variables:
var (_, height) = p;
This better indicates your intention than declaring a variable that you never use.  */

// The this reference is valid only within nonstatic members of a class or struct

// properties look like fields from the outside but internally they contain logic like methods do.



public class Panda  // the class itself being static means the type can be accessed by any other code in the same assembly or another assembly that references it
{
    string name;
    int age;
    public Panda(string n) { name = n; }  // constructor
    public Panda(string n, int Age) : this(n) { age = Age; }  // called constructor using this keyword, called constructor executes first

    public void Deconstruct(out string Name, out int Age)  // deconstructor
    {
        Age = age;
        Name = name;
    }
}


// properties are declared like a field but with a get set block
public class Stock
{
    decimal currentPrice; // the private "backing" field, remember C# uses private by default

    public decimal CurrentPrice  // public property, decimal is a data type that is double the size and precision 
    {
        get { return currentPrice; }
        set { currentPrice = value; }  // get and set in the property can also be made private
    }
}

/* get and set denote property accessors. The get accessor runs when the property is read. It must return a value of the property’s type. The set accessor runs when
the property is assigned. It has an implicit parameter named value of the property’s type that you typically assign to a private field (in this case, currentPrice).

Although properties are accessed in the same way as fields, they differ in that they give the implementer complete control over getting and setting its value. This 
control enables the implementer to choose whatever internal representation is needed without exposing the internal details to the user of the property. 
In this example, the set method could throw an exception if value was outside a valid range of value  

 A property is read only if it only has an accessor, and is write only if it only has a setter  */

// indexers are similar to properties but they're for accessing elements in a class or struct that encapsulate a list or dictionary of values

/* class Sentence
{
 string[] words = "The quick brown fox".Split();
 public string this [int wordNum] // indexer
 {
 get { return words [wordNum]; }
 set { words [wordNum] = value; }
 }
}
Here’s how we could use this indexer:
Sentence s = new Sentence();
Console.WriteLine (s[3]); // fox
s[3] = "kangaroo";
Console.WriteLine (s[3]); // kangaroo */

public class Test_Static
{
    static Test_Static() { }  // static constructor, executes once per type rather than once per instance
}

// Static field initializers run just before the static constructor is called. If a type has no static constructor,
// static field initializers will execute just prior to the type being used or anytime earlier at the whim of the runtime.

// A class marked static cannot be instantiated or subclassed, and must be composed solely of static members.The System.Console and System.Math classes are good examples of static classes.

// in C#, a class can inherit from only a single other class but can be inherited by multiple different classes

public class Asset
{
    public string name;
}

public class Stocks : Asset  // Stocks inherits from Asset
{
    public long shares_owned;
}


static class Program {
    static void Main() 
    {
        Test t1 = new Test();
        Console.WriteLine(Test.Message); // accessed via class as its const

        Panda p = new Panda("Petey", 25);  // call constructor

        (string name, int age) = p;  // deconstruction
        Console.WriteLine(name);

        Stock st1 = new Stock();
        st1.CurrentPrice = 25; // this calls set
        Console.WriteLine(st1.CurrentPrice); // this calls get 






}
 
}

