
using System;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

public class example  // we can't have free functions in C#, they must be part of a class
{

    //  a delegate is an object that knows how to call a method
    public delegate int Transformer(int x);  // decleration of a delegate, compatible with any method with an int parameter and int return type
                                      // When an instance method is assigned to a delegate object, the latter maintains a reference not only to the method but also to the instance to which the method belongs.
                                      // delegates can also be generic
    public static int Square(int x)
    {
        return x * x;
    }

     public static void Transform(int[] values, Transformer t)  // higher order function, pass in the array and the delegate, static method so doesn't need an instance
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = t(values[i]);  // apply the function for each value in the array
        }
    }
}
/* 
 * C# is statically typed and strongly typed
 * Strongly typed means that there are restrictions between conversions between types. Strong typing means that variables do have a type and that the type matters when performing operations on a variable. 
 * Statically typed means that the types are not dynamic - you can not change the type of a variable once it has been created. (unless using the dynamic keyword)
 * 
 * 
 * 
 * If you call a method, you might get back a type that is more specific than what you
asked for. This is ordinary polymorphic behavior. For the same reason, a delegate’s
target method might return a more specific type than described by the delegate.
This is called covariance:
ObjectRetriever o = new ObjectRetriever (RetriveString);
object result = o();
Console.WriteLine (result); // hello
string RetriveString() => "hello";
delegate object ObjectRetriever();
ObjectRetriever expects to get back an object, but an object subclass will also do:
delegate return types are covariant.

When you call a method, you can supply arguments that have more specific types
than the parameters of that method. This is ordinary polymorphic behavior. For
the same reason, a delegate can have more specific parameter types than its method
target. This is called contravariance. Here’s an example:
StringAction sa = new StringAction (ActOnObject);
sa ("hello");
void ActOnObject (object o) => Console.WriteLine (o); // hello
delegate void StringAction (string s);
(As with type parameter variance, delegates are variant only for reference
conversions.)
A delegate merely calls a method on someone else’s behalf. In this case, the String
Action is invoked with an argument of type string. When the argument is then
relayed to the target method, the argument is implicitly upcast to an object.

For historical reasons, array types support covariance. This means that B[] can be
cast to A[] if B subclasses A (and both are reference types):
Bear[] bears = new Bear[3];
Animal[] animals = bears; // OK
The downside of this reusability is that element assignments can fail at runtime:
animals[0] = new Camel(); // Runtime error

 */


// all delegates have multicast capability, they can refer to a list of target methods.
// then when you invoke the delegate, they will they call the functions in the order that they were 
// delegates can also be generic


// when using delegates, two roles commonly appear, broadcaster and subscriber
// broadcaster is the type that contains a delegate field and chooses when to broadcast by invoking the delegate
// subscriber is the method targeted by the delegate
// Events are a language feature that formalise this pattern. An event is a construct that exposes just the subset of delegate features required for the broadcaster/subscriber model.
// The main purpose of events is to prevent subscribers from intefering with one another

// example: stock class fires its PriceChanged event every time the Price of the Stock changes:

public delegate void PriceChangedHandler (decimal oldPrice, decimal newPrice);  // declare the delegate

public class Stock
{  // code within stock has full access to PriceChanged and can treat it as a delegate, Code outside of Broadcaster can perform only += and -= operations on the PriceChanged event.

    string symbol;
    decimal price;

    public Stock (string symbol) { this.symbol = symbol; }

    public event PriceChangedHandler PriceChanged;  // event keyword, you declare an event by putting the event keyword in front of a delegate member

    public decimal Price  // Price property
    {
        get => price;
        set
        {
            if (price == value) return;  // exit if nothing changed
            decimal oldPrice = price;
            price = value; // The value keyword is used to define the value being assigned by the set or init accessor. 
            if (PriceChanged != null) PriceChanged(oldPrice, price);  // if the invocation list is not empty, fire the event

        }
    }
    // Events are a special kind of multicast delegate that can only be invoked from within the class (or derived classes) or struct where they are declared (the publisher class).
    // If other classes or structs subscribe to the event, their event handler methods will be called when the publisher class raises the event. 
    /* These operators are used for subscription and unsubscription:

    +=: Adds a delegate instance to the invocation list.
    -=: Removes a delegate instance from the invocation list. 

     Like methods, events can be virtual, overridden, abstract, or sealed. Events can also be static:*/
}

// A lambda expression is an unnamed method written in place of a delegate instance.

/* In the following example, x => x * x is a lambda expression:
 
Transformer sqr = x => x * x;
Console.WriteLine (sqr(3)); // 9
delegate int Transformer (int i);

Internally, the compiler resolves lambda expressions of this type by writing a private method */

// each parameter of the lambda expression corresponds to a delegate parameter

/* When you capture local variables, parameters, instance fields, or the this reference,
the compiler may need to create and instantiate a private class to store a reference to
the captured data. This incurs a small performance cost, because memory must be
allocated (and subsequently collected). In situations where performance is critical,
one micro-optimization strategy is to minimize the load on the garbage collector by
ensuring that code hot paths incur few or no allocations.


From C# 9, you can ensure that a lambda expression, local function, or anonymous
method doesn’t capture state by applying the static keyword. This can be useful
in micro-optimization scenarios to prevent unintentional memory allocations. For
example, we can apply the static modifier to a lambda expression as follows:

Func<int, int> multiplier = static n => n * 2;
If we later try to modify the lambda expression such that it captures a local variable,
the compiler will generate an error:

int factor = 2;
Func<int, int> multiplier = static n => n * factor; // will not compile */

// A lambda expression can reference any variables that are accessible where the lambda expression is defined. These are called outer variables, and can include local variables, parameters, and fields

// c# try block must be followed by a catch block or a finally block (executes after the catch block or try block to perform cleanup code)

// Only one catch clause executes for a given exception. If you want to include a safety net to catch more general exceptions (such as System.Exception), you must put the more-specific handlers first.

// A finally block always executes—regardless of whether an exception is thrown and whether the try block runs to completion. You typically use finally blocks for cleanup code.
//A finally block executes after any of the following:
//• A catch block finishes (or throws a new exception).
//• The try block finishes (or throws an exception for which there’s no catch block).
//• Control leaves the try block because of a jump statement (e.g., return or goto).

// the throw keyword lets us throw exceptions

// a using {} statement is the same as doing try{} finally {}, the resource will be closed after the completion of the statement

/*
 
If you omit the brackets and statement block following a using statement (C# 8+),
it becomes a using declaration. The resource is then disposed when execution falls
outside the enclosing statement block:
if (File.Exists ("file.txt"))
{
 using var reader = File.OpenText ("file.txt"); 
 Console.WriteLine (reader.ReadLine());
 ...
}
In this case, reader will be disposed when execution falls outside the if statement
block.



 An enumerator is a read-only, forward-only cursor over a sequence of values. C#
treats a type as an enumerator if it does any of the following:

• Has a public parameterless method named MoveNext and property called
Current
• Implements System.Collections.Generic.IEnumerator<T>
• Implements System.Collections.IEnumerator

The foreach statement iterates over an enumerable object. An enumerable object is
the logical representation of a sequence. It is not itself a cursor, but an object that
produces cursors over itself. C# treats a type as enumerable if it does any of the
following (the check is performed in this order):

• Has a public parameterless method named GetEnumerator that returns an
enumerator
• Implements System.Collections.Generic.IEnumerable<T>
• Implements System.Collections.IEnumerable
• (From C# 9) Can bind to an extension method named GetEnumerator that
returns an enumerator
 
 
Whereas a foreach statement is a consumer of an enumerator, an iterator is a
producer of an enumerator. In this example, we use an iterator to return a sequence
of Fibonacci numbers (where each number is the sum of the previous two):
using System;
using System.Collections.Generic;
foreach (int fib in Fibs(6))
 Console.Write (fib + " ");
}
IEnumerable<int> Fibs (int fibCount)
{
 for (int i = 0, prevFib = 1, curFib = 1; i < fibCount; i++)
 {
 yield return prevFib;
 int newFib = prevFib+curFib;
 prevFib = curFib;
 curFib = newFib;
 }
}
OUTPUT: 1 1 2 3 5 8
Whereas a return statement expresses, “Here’s the value you asked me to return
from this method,” a yield return statement expresses, “Here’s the next element
you asked me to yield from this enumerator.” On each yield statement, control is
returned to the caller, but the callee’s state is maintained so that the method can
continue executing as soon as the caller enumerates the next element. The lifetime
of this state is bound to the enumerator such that the state can be released when the
caller has finished enumerating
 
 */


// an iterator is a method, property or indexer that contains one or more yield statements
// A return statement is illegal in an iterator block; instead you must use the yield break statement to indicate that the iterator block should exit early, without returning more elements.

/*
 An iterator is a method, property, or indexer that contains one or more yield
statements. An iterator must return one of the following four interfaces (otherwise,
the compiler will generate an error):
// Enumerable interfaces
System.Collections.IEnumerable
System.Collections.Generic.IEnumerable<T>
// Enumerator interfaces
System.Collections.IEnumerator
System.Collections.Generic.IEnumerator<T>
An iterator has different semantics, depending on whether it returns an enumerable
interface or an enumerator interface. We describe this in Chapter 7.
Multiple yield statements are permitted:
foreach (string s in Foo())
 Console.WriteLine(s); // Prints "One","Two","Three"
IEnumerable<string> Foo()
{
 yield return "One";
 yield return "Two";
 yield return "Three";
}
 */


// a record is a special kind of class or struct that's designed to work well with immutable (read-only) data.    
// Records are purely a C# compile-time construct. At runtime, the CLR sees them just as classes or structs (with a bunch of extra “synthesized” members added by the compiler).

record Point { 

    public Point (double x, double y)  // records can have constructors
    {
        X = x;
        Y = y;
    }

    public double X { get; init; }  // init only properties
    public double Y { get; init; }  // The init keyword creates so called Init Only Setters.
                                    // They add the concept of init only properties and indexers to C#.
                                    // These properties and indexers can be set at the point of object creation but become effectively get only once object creation has completed.

}

/* Upon compilation, C# transforms the record definition into a class (or struct) and
performs the following additional steps:

• It writes a protected copy constructor (and a hidden Clone method) to facilitate
nondestructive mutation.
• It overrides/overloads the equality-related functions to implement structural
equality.
• It overrides the ToString() method (to expand the record’s public properties,
as with anonymous types).  */



// Patterns test that a value has a certain shape, and can extract information from the value when it has the matching shape.
// a pattern checks if a value has a certain shape, and if that’s the case, the pattern matches – that’s why it’s called pattern matching.
// A pattern can also extract information that you can use in your code.

// Patterns are supported in the following contexts:
// after the is operator (variable is pattern)
// in switch statements
// in switch expressions

// the type pattern:
// if (obj is string { Length:4 })
//    Console.WriteLine("A string with 4 characters");



// modifiers are things like the ref and virtual keywords
// attributes are an extensible mechanism for adding custom information to code elements
// a good scenario for attributes is serialzation, the process of converting arbitrary objects to and from a particular format for storage or transmission.
// In this scenario, an attribute on a field can specify the translation between C#’s representation of the field and the format’s representation of the field. 
// An attribute is defined by a class that inherits (directly or indirectly) from the abstract class System.Attribute.

// to attach an attribute to a class or code element, specify the attributes name in square brackets before the code element
[ObsoleteAttribute]  // the class foo now has the ObsoleteAttribute attached
class foo { }
// This particular attribute is recognized by the compiler and will cause compiler warnings if a type or member marked as obsolete is referenced.
// By convention, all attribute types end in the word Attribute. C# recognizes this and allows you to omit the suffix when attaching an attribute, so [Obsolete] would work as well
// attributes can also have parameters, and you can specify multiple attributes at a time


// Dynamic binding defers binding - the proces of resolving types, members and operators- from compile time to runtime.
// Dynamic binding is useful when at compile time, you know that a certain function, member or operation exists but the compiler does not

/*
 A dynamic type is like object—it’s equally nondescriptive about a type. The dif‐
ference is that it lets you use it in ways that aren’t known at compile time. A
dynamic object binds at runtime based on its runtime type, not its compile-time
type. When the compiler sees a dynamically bound expression (which in general is
an expression that contains any value of type dynamic), it merely packages up the
expression such that the binding can be done later at runtime.
 
 
 * The var and dynamic types bear a superficial resemblance, but the difference is
deep:
• var says, “Let the compiler figure out the type.”
• dynamic says, “Let the runtime figure out the type.”
To illustrate:
dynamic x = "hello"; // Static type is dynamic; runtime type is string
var y = "hello"; // Static type is string; runtime type is string
int i = x; // Runtime error (cannot convert string to int)
int j = y; // Compile-time error (cannot convert string to int)
The static type of a variable declared with var can be dynamic:
dynamic x = "hello";
var y = x; // Static type of y is dynamic
int z = y; // Runtime error (cannot convert string to int)


c# also supports operator ovrloading. These methods must be markd static and public for the type
This is static typing:

string foo = "bar";
foo is now a string, so this will cause a compile time error:

foo = 1;
Even if you use var, it's still statically typed:

var foo = "bar";     // foo is now a string
foo = 1;             // still a compile time error
Using the dynamic keyword, means the type won't be static and can be changed, so now you can do this:

dynamic foo = "bar";   
foo = 1;              // this is now fine.



we can use pointers, dereference pointers and use references by using keywords like * & and -> in C# but only in blocks markd unsafe
*/

static class Program
{

    static void Main()
    {
        example.Transformer t = example.Square; // assign the delegate to the function, this creates a delegate instance
        int answer = t(5);  // answer is 25

        Console.WriteLine(answer);  // delegates variables are assigned a method at runtime
        int[] values = { 1, 4, 9 };
        example.Transform(values, t);  // call the Transform method of the example class

        foreach (int x in values)  // iterates items in a collection
        {
            Console.WriteLine(x);
        }

        int factor = 2;
        Func<int, int> multiplier = n => n * factor;  // lambda using captured variable
        // The Func delegate points to a method that accepts parameters and returns a value; the Action delegate points to a method that accepts parameters but does not return a value
        factor = 10;
        Console.WriteLine(multiplier(3));

        try
        {
            int y = 0;
            // Console.WriteLine(y / 0);
        }
        catch (Exception ex)  // this will throw the divide by zero exception
        {
            Console.WriteLine(ex.ToString());

        }
        finally  // will execute regardless
        {
            Console.WriteLine("This is the finally block");
        }


        // reference types can be null, value types cannot unless explicitly made nullable

        int? value = null;  // acceptable as nullable type used

        // An anonymous type is a simple class created by the compiler on the fly to store a set of values.
        // To create an anonymous type, use the new keyword followed by an object initializer, specifying the properties and values the type will contain; for example:
        var dude = new { Name = "Bob", Age = 23 };

        Console.WriteLine(dude.Name);

        // like anonymous types, tuples provide a safe way to store a set of values.
        // the main purpose of tuples is to safeky return multiple values from a method without resorting to out parameters

        var bob = ("Bob", 23); // allow compiler to infer the tuple type

        // tuples are value types with mutable (read/write) elements

        (string name, int age) me = (name: "Bob", age: 23); // explicitly defined tuple, with named elements

        Console.WriteLine(me.age);  // tuples can also be broken down, similar to deconstructors



        // The below is an example of the var pattern:
        // The var pattern is a variation of the type pattern whereby you replace the type name with the var keyword.
        // The conversion always succeeds, so its purpose is merely to let you reuse the variable that follows

        string _name = "Janet";

        bool isTheNameCorrect(string name) => name.ToUpper() is var upper && (upper == "JANET" || upper == "JANE");  // var upper is reused
        // the ability to introduce and reuse an intermediate variable (upper in this case) in an expression-bodied method is convenient

        // C# allows functions to be written in Main()


        // the constant pattern lets you match directly to a constant 
        void Foo(object obj)
        {
            if (obj is 3)
            {
                Console.WriteLine("3");
            }
        }

        Foo(3);  // call the function 

        decimal bmi = 25;

        string GetWeightCategory(decimal bmi)
        {

            switch (bmi)
            {
                case < 18.5m: return "underweight";  // relational pattern used in a switch statement
                case < 25m: return "normal";
                case < 30m: return "overweight";
                default:
                    return "invalid weight";

            };

        }

        Console.Write(GetWeightCategory(bmi));

        // we can also use the and, or or not keywords to combine patterns

        dynamic d = "hello";  // this is a statically typed string with a dynamic runtime type
        d = 1; // this will still work because d is dynamic, its type is not resolved until runtime

        // string _s = "yo!";  // throws an error, not dynamic
        // _s = 12;
        // this binding is done by the compiler, static binding and therefore the runtime (dynamic) type of _s is also s string, therefore exception thrown
        // the dynamic keyword tells the compiler to relax, and that d has a dynamic runtime type, which means it will be resolved at aruntime
    
    
    }

}
