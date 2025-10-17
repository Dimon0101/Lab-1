using System;

namespace Lab_1
{
    internal class Program
    {
        delegate string Penetration(Equipment WhoShot, Equipment InWhoShot);
        interface IEquipment
        {
            void Move();
            void Shot();
        }
        interface ITransport
        {
            void Move();
        }
        abstract class Equipment : IDisposable, IEquipment, ITransport
        {
            string name;
            bool dispose = false;
            float maxSpeed;
            float weight;
            float gun;
            float armor;
            float penetration;
            protected string Name
            {
                get { return name; }
                set
                {
                    if (value.Length > 0)
                    {
                        name = value;
                    }
                    else
                    {
                        Console.WriteLine("Wrong name");
                    }
                }
            }
            protected float MaxSpeed
            {
                get { return maxSpeed; }
                set
                {
                    if (value > 0)
                    {
                        maxSpeed = value;
                    }
                    else
                    {
                        Console.WriteLine("Wrong value of MaxSpeed");
                    }
                }
            }
            protected float Weight
            {
                get { return weight; }
                set
                {
                    if (value >= 10 && value <= 200)
                    {
                        weight = value;
                    }
                    else if (value >= 1000)
                    {
                        weight = value / 1000;
                    }
                    else
                    {
                        Console.WriteLine("Wrong value of weight");
                    }
                }
            }
            public float Armor
            {
                get { return armor; }
                set
                {
                    if(value >= 0)
                    {
                        armor = value;
                    }
                    else
                    {
                        Console.WriteLine("Wrong value of armor");
                    }
                }
            }
            public float Penetration
            {
                get { return penetration; }
                set
                {
                    if(value >=50 && value <= 1200)
                    {
                        penetration = value;
                    }
                    else
                    {
                        Console.WriteLine("Wrong value of penetration");
                    }
                }
            }
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
                Name = "Default tank";
                maxSpeed = 30;
                weight = 10;
                gun = 12.7f;
                Console.WriteLine("Default constructor of Equipment");
            }
            public Equipment(string name, float maxSpeed, float weight, int gun, float armor, float penetration)
            {
                Name = name;
                MaxSpeed = maxSpeed;
                Weight = weight;
                Gun = gun;
                Armor = armor;
                Penetration = penetration;
                Console.WriteLine($"Created {Name}, {MaxSpeed} , {Weight} , {Gun}");
            }
            public abstract void Move();
            public abstract void Shot();
            void ITransport.Move()
            {
                Console.WriteLine("Logistic move");
            }
            public static string Penetrate(Equipment WhoShot, Equipment InWhoShot)
            {
                if (WhoShot.Penetration > InWhoShot.Armor)
                {
                    return "Penetration success";
                }
                else
                {
                    return "Penetration failed";
                }
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
                    if (disposing)
                    {
                        Console.WriteLine($"Equipment {name} is deliting");
                    }
                    Console.WriteLine($"Disposed {name}");
                    dispose = true;
                }
            }
        }
        class Tank : Equipment
        {
            public Tank()
            {
                Armor = 10;
                Console.WriteLine($"Defaul constructor of tank{Name}, {MaxSpeed}, {Weight}, {Gun}");
            }
            public Tank(string name, float maxSpeed, float weight, int gun, float armor, float penetration) : base(name, maxSpeed, weight, gun,armor, penetration)
            {
                Console.WriteLine("Parametric constructor of tank");
            }
            public override void Shot()
            {
                Console.WriteLine($"Shot from {Name}");
            }
            public override void Move()
            {
                Console.WriteLine("Tank is moving fast enough");
            }
        }
        class  MBT : Tank
        {
            static MBT alreadyExists;
            private MBT(string name, float maxSpeed, float weight, int gun, float armor, float penetarion) : base(name, maxSpeed, weight, gun, armor, penetarion)
            {
                Console.WriteLine("Private constructor of MBT");
            }

            public static MBT CreateMBT(string name, float maxSpeed, float weight, int gun, float armor, float penetarion)
            {
                if(alreadyExists == null)
                {
                    MBT.alreadyExists = new MBT(name,maxSpeed,weight,gun,armor, penetarion);
                    Console.WriteLine("Oplot was created"); 
                    return alreadyExists;
                }
                else
                {
                    Console.WriteLine("Oplot already exists");
                    return alreadyExists;
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
            public Aviation()
            {
                Console.WriteLine("Default constructor of aviation");
            }
            public Aviation(string name, float maxSpeed, float weight, int gun, float armor , float penetration) : base(name, maxSpeed, weight, gun, armor, penetration)
            {
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
            public Helicopter(string name, float maxSpeed, float weight, int gun, float armor, float penetration) : base(name, maxSpeed, weight, gun, armor, penetration)
            {
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
        
        static void Main(string[] args)
        {
            Tank t1 = new Tank("Abrams",78,60,120,700,650);
            MBT Oplot = MBT.CreateMBT("Oplot", 78, 50, 125, 600, 600);
            Penetration pen = new Penetration(Equipment.Penetrate);
            Console.WriteLine(pen(t1,Oplot));
            Console.WriteLine(pen(Oplot,t1));
        }
    }
}
