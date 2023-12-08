using System;
using System.Security.Cryptography.X509Certificates;
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

    public decimal CurrentPrice  // public property, decimal is a data type that is double the size and precision of double
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
Console.WriteLine (s[3]); // kangaroo 

 
 apply the readonly modifier to a struct’s functions, ensuring that if the
function attempts to modify any field, a compile-time error is generated:
struct Point
{
 public int X, Y;
 public readonly void ResetX() => X = 0; // Error!
} 
If a readonly function calls a non-readonly function, the compiler generates a
warning (and defensively copies the struct to avoid the possibility of a mutation).

 */

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

    public void display()
    {
        Console.WriteLine(this.name);   // will print the name of whichever object calls it, can be used by derived classes, this is polymorphism
    }

    public virtual Asset Clone() { return new Asset { name = name }; }  //  virtual function of Asset class 

    public virtual string brag() { return "I'm an Asset!"; }

    public string value() { return "Asset price could be anything!"; }
}

public class Stocks : Asset  // Stocks inherits from Asset
{
    public long shares_owned;
}


// C# also has destructors called with ~, these are called finalisers

// partial classes and methods can be defined over multiple source files

public class House : Asset
{
    public long Mortgage;

    public override House Clone()
    {
        return new House { name = name, Mortgage = Mortgage };  // covariant return type, overridden virtual function, covariant return types allow us to override a method such that it returns a more derived type
    }

    public override string brag() { return "I'm a House!"; }

    public string value() { return "House prices are high right now!"; }  // this will hide the function in the base class
}

// a function marked as virtual can be overriden by subclasses wanting to provide a specialized implementation. 

// A class declared as abstract can never be instantiated. Instead, only its concrete subclasses can be instantiated.
// Abstract classes are able to define abstract members.Abstract members are like virtual members except that they don’t provide a default implementation.
// That implementation must be provided by the subclass unless that subclass is also declared abstract:

public abstract class abstract_asset {
    public abstract decimal value { get; }  // abstract property
}

public class stock_from_abstract : abstract_asset
{
    public override decimal value { get { return value; } }  // overriden the abstract property from the abstract class
}

// An overridden function member can seal its implementation with the sealed keyword to prevent it from being overridden by further subclasses. 
// sealed means it can't be overriden further


// importantly, base class constructors always execute first 

public class BaseClass {

    public int X;
    public BaseClass() { }
    public BaseClass(int num) { X = num;  }


}

public class SubClass :  BaseClass
{
    int X;
    public SubClass(int X) : base(X) { this.X = X;  Console.WriteLine(X);  Console.WriteLine(base.X); }  // you call the baseclass constructors via the base keyword
}

// object (System.Object) is the ultimate base class for all types. Any type can be upcast to object.
// object is a reference type, by virtue of being a class. Despite this, value types, such as int, can also be cast to and from object
// C# programs are type-checked both statically (at compile time) and at runtime (by the CLR).






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

        string s = null;
        Console.WriteLine(s?[5]);  // ? is the null operator, this means this writes nothing and will not produce an error

        House h = new House();
        Stocks st = new Stocks();

        h.name = "Yard";  // assign to the public name field
        st.name = "ADS";

        h.display();  // inherited method
        st.display();  // inherited method

        Asset a = h; // upcast a to the house object h, a is only able to access the elements from h that come from the Asset class, this is object slicing
        
        House h2 = (House)a; // downcast from a to make it a house object and then point reference h2 to that house object

        h2.name = "New Yard"; 
        Console.WriteLine(h.name);  // now h and h2 are references to the same object

        // the as operator performs a downcast that returns null if the downcast fails, this can be useful for tests

        Asset b = new Asset();

        b = st as Asset;  // upcast stocks st to House using as, this will return null if it fails

        Console.WriteLine(b == null);  // False, upcast is successfull

        // the is keyword tests if a reference conversion would succeed

        Console.WriteLine(h2 is House); //True


        Asset c = h; // point the Asset reference c to the house object
        Console.WriteLine(c.brag());  // returns the House version of the function! virtual allows us to access the derived version of the function using a base class pointer

        Asset d = new Asset();
        House h3 = new House();

        Console.WriteLine(d.value());
        Console.WriteLine(h3.value());  // if h3 was an asset pointer pointed to a house class, it would print the house version of value(). This can be avoided via a virtual function with an override.

        SubClass sc1 = new SubClass(10);

        // objects have a default, built in implementation of the ToString method, which returns the type name, this can also be overwritten in the class definition

        Console.WriteLine(h.ToString());  // prints house


}
 
}
