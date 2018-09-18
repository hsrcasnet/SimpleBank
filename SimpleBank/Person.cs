using System;

namespace SimpleBank
{
    public class Person
    {
        public Person(string testPerson1Name)
        {
            this.Id = Guid.NewGuid();
            this.Name = testPerson1Name;
            this.Cash = new Money(0);
        }

        public Guid Id { get; }

        public string Name { get; }

        public Money Cash { get; set; }

    }
}