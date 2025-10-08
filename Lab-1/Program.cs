using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Lab_1
{
    internal class Program
    {
        class Equipment : IDisposable
            {
            protected string name;
            bool dispose = false;
            float maxSpeed;
            protected float MaxSpeed
            {
                get { return maxSpeed; }
                set
                {
                    if (value >= 10)
                    {
                        maxSpeed = value;
                    }
                    else
                    {
                        Console.WriteLine("Wrong value of weight");
                    }
                }
            }
            float weight;
            protected float Weight
            {
                get { return weight; }
                set
                {
                    if(value >= 10 && value <= 200)
                    {
                        weight = value;
                    }    
                    else if( value >= 1000)
                    {
                        weight = value/ 1000;
                    }
                    else
                    {
                        Console.WriteLine("Wrong value of weight");
                    }
                }
            }
            float gun;
            protected float Gun
            {
                get
                {
                    return gun;
                }
                set
                {
                    if (value >= 12.7f && value <= 380)
                    {
                        gun = value;
                    }
                    else
                    {
                        Console.WriteLine("Wrong value of gun");
                    }
                }
            }

            public Equipment()
            {
                Console.WriteLine("Default constructor of Equipment");
            }
            public Equipment(string name, float maxSpeed, float weight, int gun)
            {
                this.name = name;
                MaxSpeed = maxSpeed;
                Weight = weight;
                this.gun = gun;
                Console.WriteLine("Parametric constructor of Equipment");
            }
            public virtual void Move()
            {
                Console.WriteLine("Standart move of equipment");
            }
            public virtual void Shot()
            {
                Console.WriteLine("Standart shot from equipment");
            }
            ~Equipment()
            {
                Console.WriteLine($"Destructor called for {name}");
                Dispose(false);
            }
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!dispose)
                {
                    if (disposing) { }
                    Console.WriteLine($"Disposed {name}");
                    dispose = true;
                }
            }
        }
        class Tank : Equipment
        {
            protected float armor;
            public Tank()
            {
                Console.WriteLine("Defaul constructor of tank");
            }
            public Tank(string name, float maxSpeed, float weight, int gun, float armor)
            {
                this.name = name;
                MaxSpeed = maxSpeed;
                Weight = weight;
                Gun = gun;
                this.armor = armor;
                Console.WriteLine("Parametric constructor of tank");
            }
            public override void Shot()
            {
                Console.WriteLine("Shot from tank");
            }
            public override void Move()
            {
                Console.WriteLine("Tank is moving fast enough");
            }
        }
        class  MBT : Tank
        {
            static int counterOfMBTs = 0;
            private MBT(string name, float maxSpeed, float weight, int gun, float armor)
            {
                this.name = "Oplot";
                MaxSpeed = maxSpeed;
                Weight = weight;
                Gun = gun;
                this.armor = armor;
                Console.WriteLine("Private constructor of MBT");
            }

            public static MBT CreateMBT(string name, float maxSpeed, float weight, int gun, float armor)
            {
                if(counterOfMBTs !=1)
                {
                    MBT Oplot = new MBT(name,maxSpeed,weight,gun,armor);
                    Console.WriteLine("Oplot was created");
                    counterOfMBTs++;
                    return Oplot;
                }
                else
                {
                    Console.WriteLine("Oplot already exists");
                    return null;
                }
            }

            public override void Move()
            {
                Console.WriteLine("Oplot is moving");
            }
            public override void Shot()
            {
                Console.WriteLine("Oplot is shooting from gun");
            }
        }
        class Aviation : Equipment
        {
            protected string addictionWeapon;
            public Aviation()
            {
                Console.WriteLine("Default constructor of aviation");
            }
            public Aviation(string name, float maxSpeed, float weight, int gun, string addictionWepon)
            {
                this.name = name;
                MaxSpeed = maxSpeed;
                Weight = weight;
                Gun = gun;
                this.addictionWeapon = addictionWepon;
                Console.WriteLine("Parametric constructor of aviation");
            }
            public override void Move()
            {
                Console.WriteLine("Aviation is flying faster than sound");
            }
            public override void Shot()
            {
                Console.WriteLine("Aviation uses their missels");
            }
        }
        class  Helicopter : Aviation
        {
            public Helicopter(string name, float maxSpeed, float weight, int gun, string addictionWepon)
            {
                this.name = name;
                MaxSpeed = maxSpeed;
                Weight = weight;
                Gun = gun;
                this.addictionWeapon = addictionWepon;
                Console.WriteLine("Parametric constructor of helicopter");
            }
            static Helicopter()
            {
                Console.WriteLine("Static constructor of helicopter");
            }
            public override void Move()
            {
                Console.WriteLine("Helicopter flyes very quite");
            }
            public override void Shot()
            {
                Console.WriteLine("Helicopter shot from their guns");
            }
        }
        static public void CreatePersons(bool isSuppressed, bool isreregistered)
        {
            Console.WriteLine("\n----------------------------");
            Equipment person = new Equipment("Den", 78, 55, 120);
            Equipment person2 = new Equipment("Don", 78, 55, 120);
            Tank mage = new Tank("Leopard", 78, 55, 120, 500);

            if (isSuppressed)
            {
                Console.WriteLine("\nSuppressed");
                GC.SuppressFinalize(person);
                GC.SuppressFinalize(person2);
            }

            if (isreregistered)
            {
                Console.WriteLine("\nReregistered");
                GC.ReRegisterForFinalize(person);
                GC.ReRegisterForFinalize(person2);
            }

            Console.WriteLine("\n----------------------------");
        }

        static void Main(string[] args)
        {
            CreatePersons(false,false);
            Helicopter helicopter = new Helicopter("Apache", 300, 15, 30, "Missels");

            Console.WriteLine("\nMemory before collecting = " + GC.GetTotalMemory(false));
            Console.WriteLine("\nOplot's generation before first collecting = " + GC.GetGeneration(helicopter));

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("\nMemory after collecting = " + GC.GetTotalMemory(false));
            Console.WriteLine("\nOplot's generation after first collecting = " + GC.GetGeneration(helicopter));

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("\nOplot's generation after second collecting = " + GC.GetGeneration(helicopter));

            CreatePersons(true,true);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            CreatePersons(true, true);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            using (Tank tank = new Tank("Abrams", 78,55,120,500))
            {

            }
        }
    }
}
