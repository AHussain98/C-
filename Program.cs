using System;
using System.Reflection.Metadata;
using Animals;  // using the animals namespace, draw it into the program
using System.Collections;

// C# has a unified type system in which all types ultimately share a common base type. This means that all types, whether they represent
// business objects or are primitive types such as numbers, share the same basic functionality. For example, an instance of any type can be converted to a string by calling its ToString method.
// C# has interfaces which are similar to classes but they cannot hold data, they only define behaviour and can be multiple inherited
// C# is typesafe and statically typed, like C++ and unlike Python, strongly typed, no type conversions unless explicit

/* C# relies on the runtime to perform automatic memory management. The Com‐
mon Language Runtime has a garbage collector that executes as part of your
program, reclaiming memory for objects that are no longer referenced. This frees
programmers from explicitly deallocating the memory for an object, eliminating the
problem of incorrect pointers encountered in languages such as C++.
C# does not eliminate pointers: it merely makes them unnecessary for most pro‐
gramming tasks. For performance-critical hotspots and interoperability, pointers
and explicit memory allocation is permitted in blocks that are marked unsafe.

A Common Language Runtime (CLR) provides essential runtime services such as
automatic memory management and exception handling. (The word “common”
refers to the fact that the same runtime can be shared by other managed program
ming languages, such as F#, Visual Basic, and Managed C++.)
C# is called a managed language because it compiles source code into managed
code, which is represented in Intermediate Language (IL). The CLR converts the
IL into the native code of the machine, such as X64 or X86, usually just prior
to execution. This is referred to as Just-in-Time (JIT) compilation.

The C# compiler compiles source code (a set of files with the .cs extension) into
an assembly. An assembly is the unit of packaging and deployment in .NET. An
assembly can be either an application or a library. A normal console or Windows
application has an entry point, whereas a library does not. The purpose of a library
is to be called upon (referenced) by an application or by other libraries. .NET itself is
a set of libraries (as well as a runtime environment).

 */

// Statements in C# execute sequentially and are terminated by a semicolon

// in c#, types are organised into namespaces
/* using directive allows us to avoid clutter by importing the namespace

A variable denotes a storage location that can contain different values over time. In
contrast, a constant always represents the same value (more on this later):
const int y = 360;
All values in C# are instances of a type. The meaning of a value and the set of
possible values a variable can have are determined by its type

C# uses the new operator to instantiate an object

Data members and function members that don’t operate on the instance of the type
can be marked as static. To refer to a static member from outside its type, you
specify its type name rather than an instance. An example is the WriteLine method
of the Console class. Because this is static, we call Console.WriteLine() and not
new Console().WriteLine().

Console.Writeline() automatically moves output to the new line

Top level statements allow us to write code without a main() function
They enable us to write small programs and scripts easily 
Without top level statements, C# looks for a static void main() method in the program class

As a program can have only one entry point, there can be at most one file with
top-level statements in a C# project.

int x = 12345; // int is a 32-bit integer
long y = x; // Implicit conversion to 64-bit integer
short z = (short)x; // Explicit conversion to 16-bit integer

Implicit conversions are allowed when both of the following are true:
• The compiler can guarantee that they will always succeed.
• No information is lost in conversion.

Conversely, explicit conversions are required when one of the following is true:
• The compiler cannot guarantee that they will always succeed.
• Information might be lost during conversion.


All C# types fall into the following categories:
• Value types
• Reference types
• Generic type parameters
• Pointer types

Value types comprise most built-in types (specifically, all numeric types, the char type, and the bool type) as well as custom struct and enum types.

Reference types comprise all class, array, delegate, and interface types. (This includes the predefined string type.)
The fundamental difference between value types and reference types is how they are handled in memory.

The content of a value type variable or constant is simply a value. For example, the content of the built-in value type, int, is 32 bits of data.

A reference type is more complex than a value type, having two parts: an object and the reference to that object. The content of a reference-type variable or constant is a reference to an object that contains the value.

classes are reference type and structs are value type

Assigning a reference-type variable copies the reference, not the object instance.
This allows multiple variables to refer to the same object—something not ordinarily
possible with value types.

Value-type instances occupy precisely the memory required to store their fields. In this example, Point takes 8 bytes of memory as it has two int members (4 bytes each)

Reference types require separate allocations of memory for the reference and object.
The object consumes as many bytes as its fields, plus additional administrative
overhead. The precise overhead is intrinsically private to the implementation of
the .NET runtime, but at minimum, the overhead is 8 bytes, used to store a key
to the object’s type as well as temporary information such as its lock state for
multithreading and a flag to indicate whether it has been fixed from movement by
the garbage collector. Each reference to an object requires an extra 4 or 8 bytes,
depending on whether the .NET runtime is running on a 32- or 64-bit platform.

The predefined types in C# are as follows:

Value types
• Numeric
— Signed integer (sbyte, short, int, long)
— Unsigned integer (byte, ushort, uint, ulong)
— Real number (float, double, decimal)
• Logical (bool)
• Character (char)

Reference types
• String (string)
• Object (object)

Predefined types in C# alias .NET types in the System namespace. There is only a
syntactic difference between these two statements:
int i = 5;
System.Int32 i = 5;

Division operations on integral types always eliminate the remainder (round toward zero).

At runtime, arithmetic operations on integral types can overflow. By default, this happens silently — no exception is thrown

static bool UseUmbrella (bool rainy, bool sunny, bool windy)
{
 return !windy && (rainy || sunny);
}
The && and || operators short-circuit evaluation when possible. In the preceding example, if it is windy, the expression (rainy || sunny) is not even evaluated.
The & and | operators also test for and and or conditions: return !windy & (rainy | sunny);
The difference is that they do not short-circuit. For this reason, they are rarely used in place of conditional operators.

The conditional operator (more commonly called the ternary operator because it’s
the only operator that takes three operands) has the form q ? a : b; thus, if
condition q is true, a is evaluated, otherwise b is evaluated:
static int Max (int a, int b)
{
 return (a > b) ? a : b;
}

string is a reference type rather than a value type. Its equality
operators, however, follow value-type semantics:
string a = "test";
string b = "test";
Console.Write (a == b); // True

C# allows verbatim string literals. A verbatim string literal
is prefixed with @ and does not support escape sequences. The following verbatim
string is identical to the preceding one:
string a2 = @"\\server\fileshare\helloworld.cs";

The System.Collection namespace and subnamespaces provide higher-level data structures, such as dynamically sized arrays and dictionaries.

*/





namespace Animals  // wrap the panda class in a namespace
{
    public class panda
    {

        public string Name; // instance field
        public static int Population; // static so belongs to the class, not an instance 

        public panda(string name) // constructor
        {
            Name = name;
            Population += 1;
        }
    }

}

public struct Point  // structs are value type
{
    public int x, y;  // Point is a valuetype, contains other value types
}

public class refPoint  // classes are reference type
{
    public int x, y;
}

static class Program
{

    static void Main()
    {
        Console.WriteLine("Hello, World!");

        panda p1 = new panda("p1");
        panda p2 = new panda("p2");
        Console.WriteLine(panda.Population);  // print the population of the panda class
        int x = 12 * 20; // variable x is assigned to and type is 32 bit integer
        Console.WriteLine(x);  //  calls the WriteLine method on a class called Console, which is defined in a namespace called System. This prints the variable x to a text window on the screen.

        // remember that the content of a value type variable or constant is simply a value. For example, the content of the built-in value type, int, is 32 bits of data.
        // Assignment of a value type instance always copies the instance

        Point p3 = new Point();
        p3.y = 20;
        p3.x = 10;

        Point p4 = p3;  // p4 is a copy of p3
        p4.x = 1000;
        Console.WriteLine(p4.x);
        // p4 and p3 have independent storage

        refPoint p5 = new refPoint();
        p5.y = 17;

        refPoint p6 = p5;
        p6.y = 99;

        Console.WriteLine(p5.y);  // p5 has had its y value changed because p6 is a refrence to the same object as p5

        refPoint p7 = null;  // references can be assigned the null object, this indicates the reference points to no object
        // a value type cannot have a null value

        char a = 'a';  // c# has a single char data type, unlike python

        string name = "Asad"; // String is an immutable type within System, strings are reference types
        string name2 = "Asad";
        Console.WriteLine(name == name2);  // true

        // strings can be concatenated 

        Console.WriteLine(name + " " + name2);  // concatenation
        Console.WriteLine(name + $"starts with {a}");  // strings can be interpolated with a dollar sign, as in python, this is done by calling the ToString method of the passed in object

        char[] vowels = new char[] { 'A', 'E', 'I', 'O', 'U' };  // C# array

        Console.WriteLine(vowels); // print the whole array
        Console.WriteLine(vowels[0]); // array indexing
        Console.WriteLine(vowels.Length);  // Length is an attribute of the array class

        char[] first_name = { 'A', 'S', 'A', 'D' }; // another way of defining a fixed array using initialisation

        /* Whether an array element type is a value type or a reference type has important performance implications. When the element type is a value type, each element
       value is allocated as part of the array, as shown here:
       Point[] a = new Point[1000];
       int x = a[500].X; // 0

       public struct Point { public int X, Y; }

       Had Point been a class, creating the array would have merely allocated 1,000 null references:
       Point[] a = new Point[1000];
       int x = a[500].X; // Runtime error, NullReferenceException

       public class Point { public int X, Y; }

       To avoid this error, we must explicitly instantiate 1,000 Points after instantiating the array:
       Point[] a = new Point[1000];
       for (int i = 0; i < a.Length; i++) // Iterate i from 0 to 999
        a[i] = new Point(); // Set array element i with new point  */

        // indices allow us to access the elements from the end of the array
        Console.WriteLine(vowels[^1]);  // prints the last element of the vowel array
        // ranges allow us to slice arrays
        Console.WriteLine(vowels[..2]);  // print everything before index 2

        // you can also combine ranges and indices

        // rectangular arrays are declared using commas to seperate each dimension
     /*   int[,] matrix = new int[3, 3]; // rectangular 3x3 matrix 2d array
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = i * 3 + j;
            }
        }
        Console.WriteLine(matrix);*/

        // Jagged arrays are declared using successive square brackets to represent each dimension.

        int[][] ints = new int[3][];
        // The inner dimensions aren’t specified in the declaration because, unlike a rectangular array, each inner array can be an arbitrary length.Each inner array is implicitly
        // initialized to null rather than an empty array.You must manually create each inner array
/*
        for (int i = 0; i < ints.GetLength(0); i++)
        {
            ints[i] = new int[3]; // create an inner array
            for (int j = 0; j < ints[i].Length; j++)
            {
                ints[i][j] = i * 3 + j;
            }
        }

        Console.WriteLine(ints);*/

        /* simplified array declerations:
    char[] vowels = {'a','e','i','o','u'};
    int[,] rectangularMatrix =
    {
      {0,1,2},
      {3,4,5},
      {6,7,8}
    };
    int[][] jaggedMatrix =
    {
     new int[] {0,1,2},
     new int[] {3,4,5},
     new int[] {6,7,8,9}
     };  */

        // var keyword means implicitly type a local variable

        var i = 15; // i is implicitly an integer
        var s = "Hussain";

        /* A variable represents a storage location that has a modifiable value. A variable can
           be a local variable, parameter (value, ref, out, or in), field (instance or static), or array element.
        
        The stack is a block of memory for storing local variables and parameters. The stack logically grows and shrinks as a method or function is entered and exited. 
        
        The heap is the memory in which objects (i.e., reference-type instances) reside.
        Whenever a new object is created, it is allocated on the heap, and a reference to that
        object is returned. During a program’s execution, the heap begins filling up as new
        objects are created. The runtime has a garbage collector that periodically deallocates
        objects from the heap, so your program does not run out of memory. An object is
        eligible for deallocation as soon as it’s not referenced by anything that’s itself “alive.”

        The heap also stores static fields. Unlike objects allocated on the heap (which can be garbage-collected), these live until the process ends.
         
         */

        // C# has definite assignment semantics, you can't access uninitialised memory
        // local variables must be assigned a value before they can be read
        // Function arguments must be supplied unless optional and all other variables such as fields nd array elements are automatically initialised by the runtime

        int u;
        //   Console.WriteLine(u); // not allowed, compile time error

        /* Fields and array elements are automatically initialized with the default values for
           their type. The following code outputs 0 because array elements are implicitly
           assigned to their default values:

           int[] ints = new int[2];
           Console.WriteLine (ints[0]); // 0
           The following code outputs 0, because fields are implicitly assigned a default value
           (whether instance or static):
           Console.WriteLine (Test.X); // 0

           class Test { public static int X; } // field 
        
         By default, arguments in C# are pased by value, which is passing a copy of the value to the passed method

         Data members and function members that don’t operate on the instance of the type
can be marked as static. To refer to a static member from outside its type, you
specify its type name rather than an instance. A
         */

        static void Foo(int i)
        {
            i = i + 1;
        }
        int _x = 8;
        Foo(_x);
        Console.WriteLine(_x);  // still 8 as a copy of _x is passed to the Foo function

        // Passing a reference-type argument by value copies the reference but not the object

        // to pass by refrence, C# provides the ref parameter modifier

        static void ref_Foo(ref int i)
        {
            i += 10;
        }

        ref_Foo(ref _x); // call with the ref keyword
        Console.WriteLine(_x);  // now the value at the memory address _x has changed

        // a parameter can be passed by reference or by value, regardless of whether the parameter type is a reference type or a value type.

/*An out argument is like a ref argument except for the following:
• It need not be assigned before going into the function.
• It must be assigned before it comes out of the function.
The out modifier is most commonly used to get multiple return values back from a
method; for example: */

        static void bar(out int y)
        {
            y = 1;
        }

        bar(out _x); // _x value is now 1

        Console.WriteLine(_x);

        // When you pass an argument by reference, you alias the storage location of an existing variable rather than create a new storage location.
        // the out parameter is passed by reference

/* An in parameter is similar to a ref parameter except that the argument’s value
    cannot be modified by the method (doing so generates a compile-time error). This
    modifier is most useful when passing a large value type to the method because it
    allows the compiler to avoid the overhead of copying the argument prior to passing
    it in while still protecting the original value from modification.

    Overloading solely on the presence of in is permitted:
    void Foo ( SomeBigStruct a) { ... }
    void Foo (in SomeBigStruct a) { ... }

    To call the second overload, the caller must use the in modifier:
    SomeBigStruct x = ...;
    Foo (x); // Calls the first overload
    Foo (in x); // Calls the second overload

    When there’s no ambiguity,
    void Bar (in SomeBigStruct a) { ... }
    use of the in modifier is optional for the caller:
    Bar (x); // OK (calls the 'in' overload)
    Bar (in x); // OK (calls the 'in' overload)  */


      // C# allows named arguments
        void Func(int x, int y, int z = 0)  // importantly, z is an optional argument because we assign to it, we don't need to pass it in for the function to still execute
        {
            Console.WriteLine(x); Console.WriteLine(y);
        }

        Func(x: 5, y: 10);  // named args

        // you can also mix and match positional and named args like with Python

        // The ?? operator is the null-coalescing operator. It says, “If the operand to the left is non - null, give it to me; otherwise, give me another value.” For example:
        string s1 = null;
        string s2 = s1 ?? "nothing"; // s2 evaluates to "nothing"

        /*  The ?. operator is the null-conditional or “Elvis” operator (after the Elvis emoticon).
        It allows you to call methods and access members just like the standard dot operator
        except that if the operand on the left is null, the expression evaluates to null instead
        of throwing a NullReferenceException:
        System.Text.StringBuilder sb = null;
        string s = sb?.ToString(); // No error; s instead evaluates to null  */

        // C# allows A variable’s scope extends in both directions throughout its code block, unlike c++

        // C# uses else if, not elif
        // C# also includes the switch statement
        // you can switch on a type aswell as a value
        // C# also has switch expressions

        // The foreach statement iterates over each element in an enumerable object. Most of the.NET types that represent a set or list of elements are enumerable. For example, both an array and a string are enumerable.
        foreach (char c in s)
        {
            Console.WriteLine(c); 
        }

        // C# jump statements are break, continue, return, throw and goto
        // all same as C++ except goto

        int p = 1;
        startloop:  // label
        if (i <= 5)
        {
            Console.WriteLine(i + " ");
            i++;
            goto startloop;  // goto is used to switch execution to a label
        }

        // Names declared in outer namespaces can be used unqualified within inner namespaces.
        // using keyword allows us to alias namespaces
        // names in inner namespaces hide names in outer namespaces
    }


}

