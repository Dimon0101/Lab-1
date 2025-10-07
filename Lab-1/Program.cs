using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Lab_1
{
    internal class Program
    {
        class Equipment
        {
            protected string name;
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
                    if (value < 12.7f && value >= 380)
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

            }
            public Equipment(string name, float maxSpeed, float weight, int gun)
            {
                this.name = name;
                MaxSpeed = maxSpeed;
                Weight = weight;
                this.gun = gun;
            }
            public virtual void Move()
            {
                Console.WriteLine("Standart move of equipment");
            }
            public virtual void Shot()
            {
                Console.WriteLine("Standart shot from equipment");
            }
        }
        class Tank : Equipment
        {
            protected float armor;
            public Tank()
            {

            }
            public Tank(string name, float maxSpeed, float weight, int gun, float armor)
            {
                this.name = name;
                MaxSpeed = maxSpeed;
                Weight = weight;
                Gun = gun;
                this.armor = armor;
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

            }
            public Aviation(string name, float maxSpeed, float weight, int gun, string addictionWepon)
            {
                this.name = name;
                MaxSpeed = maxSpeed;
                Weight = weight;
                Gun = gun;
                this.addictionWeapon = addictionWepon;
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
            MBT Oplot = MBT.CreateMBT("Oplot", 73.2f,51.0f,125,450);
            MBT Oplot2 = MBT.CreateMBT("Oplot", 73.2f, 51.0f, 125, 450);
        }
    }
}
