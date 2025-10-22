using System;
using System.Runtime.ConstrainedExecution;
using System.Collections.Generic;

namespace Lab_1
{
    public delegate void Penetration(Equipment WhoShot, Equipment InWhoShot);
    interface IEquipment
    {
        void Move();
        void Shot(Equipment target);
    }
    interface ILogistic
    {
        void Move();
    }
    public abstract class Equipment : IDisposable, IEquipment, ILogistic
    {
        public event Penetration OnPenetrationEvent;
        string name;
        bool disposed = false;
        float maxSpeed;
        float weight;
        float gun;
        float armor;
        float penetration;
        protected bool Disposed
        {
            get { return disposed; }
        }
        public string Name
        {
            get { return name; }
            set
            {
                if(!string.IsNullOrEmpty(value))
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
        protected float Armor
        {
            get { return armor; }
            set
            {
                if (value >= 0)
                {
                    armor = value;
                }
                else
                {
                    Console.WriteLine("Wrong value of armor");
                }
            }
        }
        protected float Penetration
        {
            get { return penetration; }
            set
            {
                if (value >= 50 && value <= 1200)
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
            Console.WriteLine($"Created {Name}");
        }
        public abstract void Move();
        public abstract void Shot(Equipment target);
        void ILogistic.Move()
        {
            Console.WriteLine("Logistic move");
        }
        public static void IsDestroyedEquipment(Equipment WhoShot, Equipment InWhoShot)
        {
            if (WhoShot.Penetration > InWhoShot.Armor)
            {
                Console.WriteLine("Equipment destroyed");
                if (InWhoShot is MBT)
                {
                    MBT.Destroy();
                }
                else
                {
                    InWhoShot.Dispose();
                }
            }
            else
            {
                Console.WriteLine("Equipment don`t destroyed");
            }
        }
        protected virtual void PenetrationEventStart(Equipment whoShot, Equipment inWhoShot)
        {
            OnPenetrationEvent?.Invoke(whoShot, inWhoShot);
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
            if (!disposed)
            {
                if (disposing)
                {
                    Console.WriteLine($"Equipment {name} is deliting");
                }
                Console.WriteLine($"Disposed {name}");
                disposed = true;
            }
        }
    }
    public static class ExtentionEquipment
    {
        public static void Getinfo(this Equipment target)
        {
            Console.WriteLine("Extention method");
            Console.WriteLine($"{target.Name}");
        }
    }
    class Tank : Equipment
    {
        public Tank()
        {
            Armor = 10;
            Console.WriteLine($"Defaul constructor of tank{Name}, {MaxSpeed}, {Weight}, {Gun}");
        }
        public Tank(string name, float maxSpeed, float weight, int gun, float armor, float penetration) : base(name, maxSpeed, weight, gun, armor, penetration)
        {
            Console.WriteLine("Parametric constructor of tank");
        }
        public override void Shot(Equipment target)
        {
            if (target != null && this != null && !Disposed)
            {
                Console.WriteLine($"{this.Name} shot at {target.Name}");
                PenetrationEventStart(this, target);
            }
        }
        public override void Move()
        {
            Console.WriteLine("Tank is moving fast enough");
        }
    }
    class MBT : Tank
    {
        static MBT alreadyExists;
        private MBT(string name, float maxSpeed, float weight, int gun, float armor, float penetarion) : base(name, maxSpeed, weight, gun, armor, penetarion)
        {
            Console.WriteLine("Private constructor of MBT");
        }

        public static MBT CreateMBT(string name, float maxSpeed, float weight, int gun, float armor, float penetarion)
        {
            if (alreadyExists == null)
            {
                MBT.alreadyExists = new MBT(name, maxSpeed, weight, gun, armor, penetarion);
                Console.WriteLine("Oplot was created");
                return alreadyExists;
            }
            else
            {
                Console.WriteLine("Oplot already exists");
                return alreadyExists;
            }
        }
        public static void Destroy()
        {
            if ((MBT.alreadyExists != null))
            {
                MBT.alreadyExists.Dispose();
            }
            alreadyExists = null;
        }
        public override void Move()
        {
            Console.WriteLine("Oplot is moving");
        }
        public override void Shot(Equipment target)
        {
            if (target != null && this != null && !Disposed)
            {
                Console.WriteLine($"{this.Name} shot at {target.Name}");
                PenetrationEventStart(this, target);
            }
        }
    }
    class Aviation : Equipment
    {
        public Aviation()
        {
            Console.WriteLine("Default constructor of aviation");
        }
        public Aviation(string name, float maxSpeed, float weight, int gun, float armor, float penetration) : base(name, maxSpeed, weight, gun, armor, penetration)
        {
            Console.WriteLine("Parametric constructor of aviation");
        }
        public override void Move()
        {
            Console.WriteLine("Aviation is flying faster than sound");
        }
        public override void Shot(Equipment target)
        {
            if (target != null && this != null && !Disposed)
            {
                Console.WriteLine($"{this.Name} shot at {target.Name}");
                PenetrationEventStart(this, target);
            }
        }
    }
    class Helicopter : Aviation
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
        public override void Shot(Equipment target)
        {
            if (target != null && this != null && !Disposed)
            {
                Console.WriteLine($"{this.Name} shot at {target.Name}");
                PenetrationEventStart(this, target);
            }
        }
    }
    class Storage : IComparable, System.Collections.IEnumerable, System.Collections.IEnumerator
    {
        private List<Equipment> equipmentList = new List<Equipment>();
        private int position = -1;

        public Equipment this[string name]
        {
            get
            {
                foreach (Equipment item in equipmentList)
                {
                    if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        return item;
                    }
                }
                return null;
            }
        }

        public void Add(Equipment item)
        {
            equipmentList.Add(item);
        }
        public int CompareTo(object obj)
        {
            if (obj is Storage otherStorage)
            {
                return this.equipmentList.Count.CompareTo(otherStorage.equipmentList.Count);
            }
            throw new ArgumentException("Object is not a Storage");
        }
        public void Sort()
        {
            equipmentList.Sort();
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            Reset();
            return this;
        }

        public bool MoveNext()
        {
            position++;
            return (position < equipmentList.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        public object Current
        {
            get
            {
                try
                {
                    return equipmentList[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException("Enumeration has either not started or has finished.");
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Tank Abrams = new Tank("Abrams", 78, 60, 120, 700, 650);
            MBT Oplot = MBT.CreateMBT("Oplot", 78, 50, 125, 600, 600);
            Aviation F16 = new Aviation("F-16", 1500, 20, 30, 10, 1000);
            ILogistic t2 = Abrams;
            Abrams.Move();
            t2.Move();
            Storage storageOfEquipment = new Storage();
            storageOfEquipment.Add(Abrams);
            storageOfEquipment.Add(Oplot);
            storageOfEquipment.Add(F16);
            foreach (Equipment item in storageOfEquipment)
            {
                item.Getinfo();
            }
            storageOfEquipment["Oplot"].Getinfo();
            Abrams.OnPenetrationEvent += delegate (Equipment whoShot, Equipment inWhoShot)
                {
                    Console.WriteLine($"{whoShot.Name} стріляв у {inWhoShot.Name}");
                    Equipment.IsDestroyedEquipment(whoShot, inWhoShot);
                };
            Oplot.OnPenetrationEvent += (whoShot, inWhoShot) =>
            {
                Console.WriteLine($"{whoShot.Name} стріляв у {inWhoShot.Name}");
                Equipment.IsDestroyedEquipment(whoShot, inWhoShot);
            };
            Action<Equipment, Equipment> actionHandler = (whoShot, inWhoShot) =>
            {
                Console.WriteLine($"{whoShot.Name} стріляв у {inWhoShot.Name}");
                Equipment.IsDestroyedEquipment(whoShot, inWhoShot);
            };
            F16.OnPenetrationEvent += actionHandler.Invoke;
            Func<float, float, bool> isCriticalPenetration = (pen, armor) => (pen - armor) > 50;
            Console.WriteLine(isCriticalPenetration(600, 1000));
            Abrams.Shot(Oplot);
            Oplot.Shot(Abrams);
            F16.Shot(Abrams);
            Abrams.Shot(F16);
        }
    }
}
