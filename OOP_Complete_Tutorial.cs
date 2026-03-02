using System;
using System.Collections.Generic;

namespace OOPTutorial
{
    // ============================================================================
    // 1. ENCAPSULATION - Data Hiding and Access Control
    // ============================================================================
    
    /// <summary>
    /// Encapsulation: Bundling data and methods that work on that data within a class.
    /// Using access modifiers (private, public, protected) to hide internal details.
    /// </summary>
    public class BankAccount
    {
        // Private fields - hidden from outside access
        private string accountNumber;
        private decimal balance;
        private string accountHolderName;

        // Public property with validation (getter/setter)
        public string AccountNumber
        {
            get { return accountNumber; }
            private set { accountNumber = value; } // Private setter - can only be set internally
        }

        // Auto-implemented property
        public string AccountHolderName
        {
            get { return accountHolderName; }
            set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Account holder name cannot be empty");
                accountHolderName = value; 
            }
        }

        // Read-only property
        public decimal Balance
        {
            get { return balance; }
        }

        // Constructor
        public BankAccount(string accountNumber, string holderName, decimal initialBalance)
        {
            this.accountNumber = accountNumber;
            this.AccountHolderName = holderName;
            this.balance = initialBalance;
        }

        // Public methods to interact with private data
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive");
            
            balance += amount;
            Console.WriteLine($"Deposited: ${amount}. New Balance: ${balance}");
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be positive");
                return false;
            }

            if (amount > balance)
            {
                Console.WriteLine("Insufficient funds");
                return false;
            }

            balance -= amount;
            Console.WriteLine($"Withdrawn: ${amount}. New Balance: ${balance}");
            return true;
        }

        public void DisplayAccountInfo()
        {
            Console.WriteLine($"Account: {accountNumber}, Holder: {accountHolderName}, Balance: ${balance}");
        }
    }

    // ============================================================================
    // 2. INHERITANCE - Code Reusability and IS-A Relationship
    // ============================================================================
    
    /// <summary>
    /// Base class representing a generic Person
    /// </summary>
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public Person(string name, int age, string address)
        {
            Name = name;
            Age = age;
            Address = address;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}, Address: {Address}");
        }

        public void Sleep()
        {
            Console.WriteLine($"{Name} is sleeping...");
        }
    }

    /// <summary>
    /// Derived class - Student inherits from Person
    /// </summary>
    public class Student : Person
    {
        public string StudentId { get; set; }
        public string Major { get; set; }
        public double GPA { get; set; }

        // Constructor calling base class constructor
        public Student(string name, int age, string address, string studentId, string major, double gpa)
            : base(name, age, address)
        {
            StudentId = studentId;
            Major = major;
            GPA = gpa;
        }

        // Method specific to Student
        public void Study()
        {
            Console.WriteLine($"{Name} is studying {Major}");
        }

        // Overriding base class method
        public override void DisplayInfo()
        {
            base.DisplayInfo(); // Call parent method
            Console.WriteLine($"Student ID: {StudentId}, Major: {Major}, GPA: {GPA}");
        }
    }

    /// <summary>
    /// Another derived class - Teacher inherits from Person
    /// </summary>
    public class Teacher : Person
    {
        public string EmployeeId { get; set; }
        public string Subject { get; set; }
        public decimal Salary { get; set; }

        public Teacher(string name, int age, string address, string employeeId, string subject, decimal salary)
            : base(name, age, address)
        {
            EmployeeId = employeeId;
            Subject = subject;
            Salary = salary;
        }

        public void Teach()
        {
            Console.WriteLine($"{Name} is teaching {Subject}");
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Employee ID: {EmployeeId}, Subject: {Subject}, Salary: ${Salary}");
        }
    }

    // ============================================================================
    // 3. POLYMORPHISM - One Interface, Multiple Forms
    // ============================================================================
    
    // A. Method Overloading (Compile-time Polymorphism)
    public class Calculator
    {
        // Same method name, different parameters
        public int Add(int a, int b)
        {
            return a + b;
        }

        public double Add(double a, double b)
        {
            return a + b;
        }

        public int Add(int a, int b, int c)
        {
            return a + b + c;
        }

        public string Add(string a, string b)
        {
            return a + b;
        }
    }

    // B. Method Overriding (Runtime Polymorphism)
    public class Shape
    {
        public string Name { get; set; }

        public Shape(string name)
        {
            Name = name;
        }

        // Virtual method can be overridden
        public virtual double CalculateArea()
        {
            return 0;
        }

        public virtual void Draw()
        {
            Console.WriteLine($"Drawing a {Name}");
        }
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius) : base("Circle")
        {
            Radius = radius;
        }

        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }

        public override void Draw()
        {
            Console.WriteLine($"Drawing a circle with radius {Radius}");
        }
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height) : base("Rectangle")
        {
            Width = width;
            Height = height;
        }

        public override double CalculateArea()
        {
            return Width * Height;
        }

        public override void Draw()
        {
            Console.WriteLine($"Drawing a rectangle {Width}x{Height}");
        }
    }

    public class Triangle : Shape
    {
        public double Base { get; set; }
        public double Height { get; set; }

        public Triangle(double baseLength, double height) : base("Triangle")
        {
            Base = baseLength;
            Height = height;
        }

        public override double CalculateArea()
        {
            return 0.5 * Base * Height;
        }

        public override void Draw()
        {
            Console.WriteLine($"Drawing a triangle with base {Base} and height {Height}");
        }
    }

    // ============================================================================
    // 4. ABSTRACTION - Hiding Implementation Details
    // ============================================================================
    
    // A. Abstract Class - Cannot be instantiated
    public abstract class Vehicle
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public Vehicle(string brand, string model, int year)
        {
            Brand = brand;
            Model = model;
            Year = year;
        }

        // Abstract method - must be implemented by derived classes
        public abstract void Start();
        public abstract void Stop();
        public abstract double CalculateFuelEfficiency();

        // Concrete method - shared by all vehicles
        public void DisplayInfo()
        {
            Console.WriteLine($"{Year} {Brand} {Model}");
        }
    }

    public class Car : Vehicle
    {
        public int NumberOfDoors { get; set; }

        public Car(string brand, string model, int year, int doors)
            : base(brand, model, year)
        {
            NumberOfDoors = doors;
        }

        public override void Start()
        {
            Console.WriteLine($"{Brand} {Model} car is starting with key ignition...");
        }

        public override void Stop()
        {
            Console.WriteLine($"{Brand} {Model} car is stopping...");
        }

        public override double CalculateFuelEfficiency()
        {
            return 25.5; // Miles per gallon
        }
    }

    public class Motorcycle : Vehicle
    {
        public bool HasSideCar { get; set; }

        public Motorcycle(string brand, string model, int year, bool hasSideCar)
            : base(brand, model, year)
        {
            HasSideCar = hasSideCar;
        }

        public override void Start()
        {
            Console.WriteLine($"{Brand} {Model} motorcycle is starting with kick start...");
        }

        public override void Stop()
        {
            Console.WriteLine($"{Brand} {Model} motorcycle is stopping...");
        }

        public override double CalculateFuelEfficiency()
        {
            return 45.0; // Miles per gallon
        }
    }

    // B. Interface - Pure abstraction (contract)
    public interface IPayable
    {
        void ProcessPayment(decimal amount);
        decimal GetTotalPaid();
    }

    public interface IPrintable
    {
        void Print();
    }

    // Class implementing multiple interfaces
    public class Invoice : IPayable, IPrintable
    {
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        private decimal amountPaid;

        public Invoice(string invoiceNumber, decimal totalAmount)
        {
            InvoiceNumber = invoiceNumber;
            TotalAmount = totalAmount;
            amountPaid = 0;
        }

        public void ProcessPayment(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Payment amount must be positive");
                return;
            }

            amountPaid += amount;
            Console.WriteLine($"Payment of ${amount} processed for invoice {InvoiceNumber}");
        }

        public decimal GetTotalPaid()
        {
            return amountPaid;
        }

        public void Print()
        {
            Console.WriteLine("=== INVOICE ===");
            Console.WriteLine($"Invoice #: {InvoiceNumber}");
            Console.WriteLine($"Total Amount: ${TotalAmount}");
            Console.WriteLine($"Amount Paid: ${amountPaid}");
            Console.WriteLine($"Balance Due: ${TotalAmount - amountPaid}");
            Console.WriteLine("===============");
        }
    }

    // ============================================================================
    // 5. COMPOSITION - Strong "Has-A" Relationship
    // ============================================================================
    
    /// <summary>
    /// Composition: Strong relationship where contained objects cannot exist without container
    /// If the container is destroyed, the contained objects are also destroyed
    /// Example: A House HAS Rooms - Rooms cannot exist without the House
    /// </summary>
    
    // Component classes (these are part of the container)
    public class Engine
    {
        public string Type { get; set; }
        public int Horsepower { get; set; }

        public Engine(string type, int horsepower)
        {
            Type = type;
            Horsepower = horsepower;
        }

        public void Start()
        {
            Console.WriteLine($"{Type} engine ({Horsepower}HP) is starting...");
        }

        public void Stop()
        {
            Console.WriteLine($"{Type} engine is stopping...");
        }
    }

    public class Wheel
    {
        public int Size { get; set; }
        public string Brand { get; set; }

        public Wheel(int size, string brand)
        {
            Size = size;
            Brand = brand;
        }

        public void Display()
        {
            Console.WriteLine($"{Size}\" {Brand} wheel");
        }
    }

    // Container class that OWNS its components
    public class CarComposition
    {
        public string Model { get; set; }
        private Engine engine;  // Composition: Car HAS-A Engine
        private List<Wheel> wheels;  // Composition: Car HAS Wheels

        // Constructor creates the components - they are born with the car
        public CarComposition(string model, string engineType, int horsepower)
        {
            Model = model;
            
            // Creating components inside the constructor (Composition)
            engine = new Engine(engineType, horsepower);
            wheels = new List<Wheel>
            {
                new Wheel(18, "Michelin"),
                new Wheel(18, "Michelin"),
                new Wheel(18, "Michelin"),
                new Wheel(18, "Michelin")
            };

            Console.WriteLine($"Car '{Model}' created with engine and wheels");
        }

        public void StartCar()
        {
            Console.WriteLine($"Starting {Model}...");
            engine.Start();
        }

        public void StopCar()
        {
            Console.WriteLine($"Stopping {Model}...");
            engine.Stop();
        }

        public void DisplayCarInfo()
        {
            Console.WriteLine($"Car Model: {Model}");
            Console.Write("Engine: ");
            Console.WriteLine($"{engine.Type}, {engine.Horsepower}HP");
            Console.WriteLine("Wheels:");
            foreach (var wheel in wheels)
            {
                Console.Write("  - ");
                wheel.Display();
            }
        }

        // When car is destroyed, engine and wheels are also destroyed (garbage collected)
    }

    // Another Composition Example: House and Rooms
    public class Room
    {
        public string Name { get; set; }
        public double Area { get; set; }

        public Room(string name, double area)
        {
            Name = name;
            Area = area;
        }

        public void Display()
        {
            Console.WriteLine($"  {Name}: {Area} sq ft");
        }
    }

    public class House
    {
        public string Address { get; set; }
        private List<Room> rooms;  // Composition: House HAS Rooms

        public House(string address)
        {
            Address = address;
            rooms = new List<Room>();  // Rooms are created and managed by House
            
            // Creating rooms as part of house construction
            rooms.Add(new Room("Living Room", 300));
            rooms.Add(new Room("Kitchen", 150));
            rooms.Add(new Room("Bedroom 1", 200));
            rooms.Add(new Room("Bedroom 2", 180));
            rooms.Add(new Room("Bathroom", 80));
        }

        public void AddRoom(string name, double area)
        {
            rooms.Add(new Room(name, area));
        }

        public void DisplayHouseInfo()
        {
            Console.WriteLine($"House at: {Address}");
            Console.WriteLine("Rooms:");
            foreach (var room in rooms)
            {
                room.Display();
            }
            Console.WriteLine($"Total Rooms: {rooms.Count}");
        }
    }

    // ============================================================================
    // 6. AGGREGATION - Weak "Has-A" Relationship
    // ============================================================================
    
    /// <summary>
    /// Aggregation: Weak relationship where contained objects can exist independently
    /// If the container is destroyed, the contained objects continue to exist
    /// Example: A Department HAS Professors - Professors can exist without Department
    /// </summary>
    
    // Independent class that can exist on its own
    public class Professor
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public int YearsOfExperience { get; set; }

        public Professor(string name, string subject, int experience)
        {
            Name = name;
            Subject = subject;
            YearsOfExperience = experience;
        }

        public void Teach()
        {
            Console.WriteLine($"Prof. {Name} is teaching {Subject}");
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"  - Prof. {Name} ({Subject}), Experience: {YearsOfExperience} years");
        }
    }

    // Container class that USES independent objects
    public class Department
    {
        public string Name { get; set; }
        private List<Professor> professors;  // Aggregation: Department HAS Professors

        public Department(string name)
        {
            Name = name;
            professors = new List<Professor>();
        }

        // Professors are passed in from outside (Aggregation)
        public void AddProfessor(Professor professor)
        {
            professors.Add(professor);
            Console.WriteLine($"Prof. {professor.Name} joined {Name} department");
        }

        public void RemoveProfessor(Professor professor)
        {
            professors.Remove(professor);
            Console.WriteLine($"Prof. {professor.Name} left {Name} department");
        }

        public void DisplayDepartmentInfo()
        {
            Console.WriteLine($"Department: {Name}");
            Console.WriteLine($"Professors ({professors.Count}):");
            foreach (var prof in professors)
            {
                prof.DisplayInfo();
            }
        }

        // When department is destroyed, professors still exist independently
    }

    // Another Aggregation Example: Library and Books
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }

        public Book(string title, string author, string isbn)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"  - '{Title}' by {Author} (ISBN: {ISBN})");
        }
    }

    public class Library
    {
        public string Name { get; set; }
        private List<Book> books;  // Aggregation: Library HAS Books

        public Library(string name)
        {
            Name = name;
            books = new List<Book>();
        }

        // Books exist independently and are added to library
        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine($"Book '{book.Title}' added to {Name}");
        }

        public void RemoveBook(Book book)
        {
            books.Remove(book);
            Console.WriteLine($"Book '{book.Title}' removed from {Name}");
        }

        public void DisplayLibraryInfo()
        {
            Console.WriteLine($"Library: {Name}");
            Console.WriteLine($"Total Books: {books.Count}");
            foreach (var book in books)
            {
                book.DisplayInfo();
            }
        }

        // When library closes, books still exist and can be moved to another library
    }

    // ============================================================================
    // COMPARISON: Composition vs Aggregation
    // ============================================================================
    
    /// <summary>
    /// Key Differences:
    /// 
    /// COMPOSITION (Strong relationship):
    /// - Parts cannot exist without the whole
    /// - Whole manages lifecycle of parts
    /// - Parts are created inside the whole
    /// - Example: Car-Engine, House-Rooms
    /// - "Death relationship" - parts die with the whole
    /// 
    /// AGGREGATION (Weak relationship):
    /// - Parts can exist independently
    /// - Parts are passed to the whole from outside
    /// - Parts can be shared between wholes
    /// - Example: Department-Professors, Library-Books
    /// - Parts survive even if the whole is destroyed
    /// </summary>

    // ============================================================================
    // 7. GENERICS - Type-Safe Reusable Code
    // ============================================================================
    
    // A. Generic Class
    public class Box<T>
    {
        private T item;

        public void Pack(T item)
        {
            this.item = item;
            Console.WriteLine($"Packed: {item}");
        }

        public T Unpack()
        {
            Console.WriteLine($"Unpacked: {item}");
            return item;
        }

        public void DisplayType()
        {
            Console.WriteLine($"Box contains type: {typeof(T).Name}");
        }
    }

    // B. Generic Class with Constraints
    public class Repository<T> where T : class
    {
        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
            Console.WriteLine($"Added item to repository. Total items: {items.Count}");
        }

        public T Get(int index)
        {
            if (index >= 0 && index < items.Count)
                return items[index];
            return null;
        }

        public List<T> GetAll()
        {
            return new List<T>(items);
        }

        public int Count()
        {
            return items.Count;
        }
    }

    // C. Generic Methods
    public class GenericMethods
    {
        // Generic method to swap two values
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        // Generic method to find maximum
        public static T FindMax<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

        // Generic method to display array
        public static void DisplayArray<T>(T[] array)
        {
            Console.Write("Array: ");
            foreach (T item in array)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }
    }

    // D. Generic Interface
    public interface IDataService<T>
    {
        void Save(T item);
        T Load(int id);
        List<T> LoadAll();
        void Delete(int id);
    }

    // E. Multiple Generic Type Parameters
    public class KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public void Display()
        {
            Console.WriteLine($"Key: {Key}, Value: {Value}");
        }
    }

    // ============================================================================
    // MAIN PROGRAM - Demonstrating All Concepts
    // ============================================================================
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# OOP TUTORIAL: FOUR PILLARS + COMPOSITION + AGGREGATION + GENERICS ===\n");

            // ========== 1. ENCAPSULATION ==========
            Console.WriteLine("--- 1. ENCAPSULATION ---");
            BankAccount account = new BankAccount("ACC001", "John Doe", 1000);
            account.DisplayAccountInfo();
            account.Deposit(500);
            account.Withdraw(200);
            // account.balance = 5000; // ERROR: Can't access private field
            Console.WriteLine();

            // ========== 2. INHERITANCE ==========
            Console.WriteLine("--- 2. INHERITANCE ---");
            Student student = new Student("Alice Smith", 20, "123 College St", "S001", "Computer Science", 3.8);
            student.DisplayInfo();
            student.Study();
            student.Sleep(); // Inherited from Person
            Console.WriteLine();

            Teacher teacher = new Teacher("Bob Johnson", 45, "456 Faculty Ave", "T001", "Mathematics", 65000);
            teacher.DisplayInfo();
            teacher.Teach();
            Console.WriteLine();

            // ========== 3. POLYMORPHISM ==========
            Console.WriteLine("--- 3. POLYMORPHISM ---");
            
            // A. Compile-time Polymorphism (Method Overloading)
            Console.WriteLine("Compile-time Polymorphism:");
            Calculator calc = new Calculator();
            Console.WriteLine($"Add(5, 3) = {calc.Add(5, 3)}");
            Console.WriteLine($"Add(5.5, 3.2) = {calc.Add(5.5, 3.2)}");
            Console.WriteLine($"Add(1, 2, 3) = {calc.Add(1, 2, 3)}");
            Console.WriteLine($"Add(\"Hello\", \" World\") = {calc.Add("Hello", " World")}");
            Console.WriteLine();

            // B. Runtime Polymorphism (Method Overriding)
            Console.WriteLine("Runtime Polymorphism:");
            List<Shape> shapes = new List<Shape>
            {
                new Circle(5),
                new Rectangle(4, 6),
                new Triangle(3, 8)
            };

            foreach (Shape shape in shapes)
            {
                shape.Draw();
                Console.WriteLine($"Area: {shape.CalculateArea():F2}");
                Console.WriteLine();
            }

            // ========== 4. ABSTRACTION ==========
            Console.WriteLine("--- 4. ABSTRACTION ---");
            
            // Abstract Class
            Console.WriteLine("Abstract Class:");
            Vehicle car = new Car("Toyota", "Camry", 2023, 4);
            car.DisplayInfo();
            car.Start();
            Console.WriteLine($"Fuel Efficiency: {car.CalculateFuelEfficiency()} MPG");
            car.Stop();
            Console.WriteLine();

            Vehicle motorcycle = new Motorcycle("Harley Davidson", "Street 750", 2022, false);
            motorcycle.DisplayInfo();
            motorcycle.Start();
            Console.WriteLine($"Fuel Efficiency: {motorcycle.CalculateFuelEfficiency()} MPG");
            motorcycle.Stop();
            Console.WriteLine();

            // Interface
            Console.WriteLine("Interface:");
            Invoice invoice = new Invoice("INV-001", 1000);
            invoice.Print();
            invoice.ProcessPayment(400);
            invoice.ProcessPayment(300);
            invoice.Print();
            Console.WriteLine();

            // ========== 5. COMPOSITION ==========
            Console.WriteLine("--- 5. COMPOSITION (Strong Has-A Relationship) ---");
            
            // Composition Example 1: Car and Engine/Wheels
            Console.WriteLine("Example 1: Car Composition");
            CarComposition myCar = new CarComposition("Tesla Model 3", "Electric", 283);
            myCar.DisplayCarInfo();
            myCar.StartCar();
            myCar.StopCar();
            // If myCar is destroyed, the engine and wheels are also destroyed
            Console.WriteLine();

            // Composition Example 2: House and Rooms
            Console.WriteLine("Example 2: House Composition");
            House myHouse = new House("123 Main Street");
            myHouse.DisplayHouseInfo();
            myHouse.AddRoom("Home Office", 120);
            Console.WriteLine("\nAfter adding a room:");
            myHouse.DisplayHouseInfo();
            // If myHouse is destroyed, all rooms are also destroyed
            Console.WriteLine();

            // ========== 6. AGGREGATION ==========
            Console.WriteLine("--- 6. AGGREGATION (Weak Has-A Relationship) ---");
            
            // Aggregation Example 1: Department and Professors
            Console.WriteLine("Example 1: Department Aggregation");
            
            // Create professors independently (they exist on their own)
            Professor prof1 = new Professor("Dr. Smith", "Computer Science", 15);
            Professor prof2 = new Professor("Dr. Johnson", "Mathematics", 12);
            Professor prof3 = new Professor("Dr. Williams", "Physics", 10);

            // Create department and add professors
            Department csDepartment = new Department("Computer Science");
            csDepartment.AddProfessor(prof1);
            csDepartment.AddProfessor(prof2);
            Console.WriteLine();
            csDepartment.DisplayDepartmentInfo();
            
            // Professor can be removed from department but still exists
            Console.WriteLine();
            csDepartment.RemoveProfessor(prof2);
            Console.WriteLine("After removing a professor:");
            csDepartment.DisplayDepartmentInfo();
            
            // Professor still exists and can join another department
            Console.WriteLine();
            Console.WriteLine("Removed professor still exists:");
            prof2.DisplayInfo();
            Console.WriteLine();

            // Aggregation Example 2: Library and Books
            Console.WriteLine("Example 2: Library Aggregation");
            
            // Create books independently
            Book book1 = new Book("Clean Code", "Robert C. Martin", "978-0132350884");
            Book book2 = new Book("Design Patterns", "Gang of Four", "978-0201633610");
            Book book3 = new Book("The Pragmatic Programmer", "Hunt & Thomas", "978-0135957059");

            // Create library and add books
            Library cityLibrary = new Library("City Central Library");
            cityLibrary.AddBook(book1);
            cityLibrary.AddBook(book2);
            cityLibrary.AddBook(book3);
            Console.WriteLine();
            cityLibrary.DisplayLibraryInfo();
            
            // Book can be removed but still exists
            Console.WriteLine();
            cityLibrary.RemoveBook(book1);
            
            // Book can be added to a different library
            Library universityLibrary = new Library("University Library");
            universityLibrary.AddBook(book1);
            Console.WriteLine();

            Console.WriteLine("KEY DIFFERENCE:");
            Console.WriteLine("- COMPOSITION: If Car is destroyed, Engine dies too (strong coupling)");
            Console.WriteLine("- AGGREGATION: If Library closes, Books still exist (weak coupling)");
            Console.WriteLine();

            // ========== 7. GENERICS ==========
            Console.WriteLine("--- 7. GENERICS ---");
            
            // Generic Class
            Console.WriteLine("Generic Box:");
            Box<int> intBox = new Box<int>();
            intBox.Pack(42);
            intBox.DisplayType();
            int value = intBox.Unpack();
            Console.WriteLine();

            Box<string> stringBox = new Box<string>();
            stringBox.Pack("Hello Generics!");
            stringBox.DisplayType();
            string text = stringBox.Unpack();
            Console.WriteLine();

            // Generic Repository
            Console.WriteLine("Generic Repository:");
            Repository<Student> studentRepo = new Repository<Student>();
            studentRepo.Add(student);
            studentRepo.Add(new Student("Charlie Brown", 22, "789 Campus Rd", "S002", "Physics", 3.5));
            Console.WriteLine($"Total students in repository: {studentRepo.Count()}");
            Console.WriteLine();

            // Generic Methods
            Console.WriteLine("Generic Methods:");
            int x = 10, y = 20;
            Console.WriteLine($"Before swap: x={x}, y={y}");
            GenericMethods.Swap(ref x, ref y);
            Console.WriteLine($"After swap: x={x}, y={y}");
            Console.WriteLine();

            Console.WriteLine($"Max of 15 and 25: {GenericMethods.FindMax(15, 25)}");
            Console.WriteLine($"Max of 'apple' and 'banana': {GenericMethods.FindMax("apple", "banana")}");
            Console.WriteLine();

            int[] numbers = { 1, 2, 3, 4, 5 };
            GenericMethods.DisplayArray(numbers);
            string[] words = { "Hello", "World", "Generics" };
            GenericMethods.DisplayArray(words);
            Console.WriteLine();

            // Multiple Type Parameters
            Console.WriteLine("Multiple Generic Type Parameters:");
            KeyValuePair<int, string> kvp1 = new KeyValuePair<int, string>(1, "One");
            kvp1.Display();
            KeyValuePair<string, double> kvp2 = new KeyValuePair<string, double>("Pi", 3.14159);
            kvp2.Display();
            Console.WriteLine();

            Console.WriteLine("=== END OF TUTORIAL ===");
            Console.ReadLine();
        }
    }
}
