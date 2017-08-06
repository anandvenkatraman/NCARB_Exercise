using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using TestApi1.Models;
namespace TestApi1.Caching
{
    public static class WebApiCache
    {
        static ObjectCache cache = new MemoryCache("WebApiCache");
        static object locker = new object();

        public static readonly string PersonsKey = "PersonsKey";

        // clear entire cache
        public static void Clear()
        {
            foreach (var item in cache)
                cache.Remove(item.Key);
        }

        // clears single cache entry
        public static void Clear(string key)
        {
            cache.Remove(key);
        }

        // add to cache helper
        static void Add(string key, object value, DateTimeOffset expiration, CacheItemPriority priority = CacheItemPriority.Default)
        {
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = expiration;
            policy.Priority = priority;

            var item = new CacheItem(key, value);
            cache.Add(item, policy);
        }


        public static List<Person> Persons
        {
            get
            {
                var list = cache[PersonsKey] as List<Person>;
                if (list == null)
                {
                    lock (locker)
                    {
                        list = new List<Person>();
                        Add(PersonsKey, list, DateTime.Now.AddHours(1));
                    }
                }

                return list;
            }
        }

        // clears Persons cache
        public static void ClearPersons()
        {
            Clear(PersonsKey);
        }
        public static void AddPerson(Person p)
        {
            if (Persons.Count == 0)
            {
                p.Id = 1;
                Persons.Add(p);
            }
            else
            {
                p.Id = Persons.OrderByDescending(o => o.Id).FirstOrDefault().Id + 1;
                Persons.Add(p);
            }
        }
        public static void UpdatePerson(Person oldP,Person newP)
        {
                var p = Persons.Where(o => o.Id == oldP.Id).FirstOrDefault();
                p.FirstName = newP.FirstName;
                p.LastName = newP.LastName;
                p.JobTitle = newP.JobTitle;
        }
        public static void RemovePerson(int pId)
        {
            Persons.Remove(Persons.Where(o => o.Id == pId).FirstOrDefault());
        }

        public static void GeneratePersonsData()
        {
            ClearPersons();

            #region start generating persons
            Persons.Add(new Person() { Id = 1, FirstName = "Judy", LastName = "Schneider", JobTitle = "CEO" });
            Persons.Add(new Person() { Id = 2, FirstName = "Jocelyn", LastName = "Bissett", JobTitle = "Representative" });
            Persons.Add(new Person() { Id = 3, FirstName = "Rob", LastName = "Dhillon", JobTitle = "Chief" });
            Persons.Add(new Person() { Id = 4, FirstName = "Rodrigo", LastName = "Graham", JobTitle = "Director" });
            Persons.Add(new Person() { Id = 5, FirstName = "Aeron", LastName = "Flores", JobTitle = "VP" });
            Persons.Add(new Person() { Id = 6, FirstName = "Benjamin", LastName = "Lonstein", JobTitle = "COO" });
            Persons.Add(new Person() { Id = 7, FirstName = "Gabriel", LastName = "Dhaliwal", JobTitle = "CTO" });
            Persons.Add(new Person() { Id = 8, FirstName = "Michael", LastName = "Bissett", JobTitle = "Scientist" });
            Persons.Add(new Person() { Id = 9, FirstName = "Avi", LastName = "Coffman", JobTitle = "Blogger" });
            Persons.Add(new Person() { Id = 10, FirstName = "Daniel", LastName = "Dischinger", JobTitle = "Representative" });
            Persons.Add(new Person() { Id = 11, FirstName = "Andrew", LastName = "Schneider", JobTitle = "Director" });
            Persons.Add(new Person() { Id = 12, FirstName = "Christina", LastName = "Mourani", JobTitle = "Blogger" });
            Persons.Add(new Person() { Id = 13, FirstName = "Baoqiang", LastName = "Lonstein", JobTitle = "Scientist" });
            Persons.Add(new Person() { Id = 14, FirstName = "Bob", LastName = "Shortland", JobTitle = "Architect" });
            Persons.Add(new Person() { Id = 15, FirstName = "Sunil", LastName = "Budman", JobTitle = "Blogger" });
            Persons.Add(new Person() { Id = 16, FirstName = "Juergen", LastName = "Chhibber", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 17, FirstName = "Sudip", LastName = "Comras", JobTitle = "Architect" });
            Persons.Add(new Person() { Id = 18, FirstName = "Steve", LastName = "Manchanda", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 19, FirstName = "Sonya", LastName = "FitzGerald", JobTitle = "Architect" });
            Persons.Add(new Person() { Id = 20, FirstName = "Tracey", LastName = "Fahey", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 21, FirstName = "Judy", LastName = "Schneider", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 22, FirstName = "Mark", LastName = "Bissett", JobTitle = "Blogger" });
            Persons.Add(new Person() { Id = 23, FirstName = "Jocelyn", LastName = "Galloway", JobTitle = "Architect" });
            Persons.Add(new Person() { Id = 24, FirstName = "Derek", LastName = "Aamisepp", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 25, FirstName = "Jeff", LastName = "Rosenberg", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 26, FirstName = "Tracey", LastName = "McCallion", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 27, FirstName = "Mark", LastName = "Aguilera", JobTitle = "Blogger" });
            Persons.Add(new Person() { Id = 28, FirstName = "Tracey", LastName = "Bissett", JobTitle = "Architect" });
            Persons.Add(new Person() { Id = 29, FirstName = "Mark", LastName = "Gonzalez", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 30, FirstName = "Jocelyn", LastName = "Bissett", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 31, FirstName = "Judy", LastName = "Schneider", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 32, FirstName = "Chris", LastName = "Shuttleworth", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 33, FirstName = "Kamal", LastName = "Bissett", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 34, FirstName = "Lindsey", LastName = "Cowburn", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 35, FirstName = "Brianna", LastName = "Bissett", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 36, FirstName = "Timothy", LastName = "Meacham", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 37, FirstName = "Justine", LastName = "McCallion", JobTitle = "Architect" });
            Persons.Add(new Person() { Id = 38, FirstName = "Fernando", LastName = "Murad", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 39, FirstName = "Valerie", LastName = "Sankauskas", JobTitle = "Blogger" });
            Persons.Add(new Person() { Id = 40, FirstName = "Ronghui", LastName = "Bissett", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 41, FirstName = "Judy", LastName = "Schneider", JobTitle = "Architect" });
            Persons.Add(new Person() { Id = 42, FirstName = "Wendy", LastName = "Bergin", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 43, FirstName = "Elena", LastName = "Bissett", JobTitle = "Architect" });
            Persons.Add(new Person() { Id = 44, FirstName = "Jennifer", LastName = "Fleischmann", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 45, FirstName = "Kenneth", LastName = "Bissett", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 46, FirstName = "Todd", LastName = "Fleischmann", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 47, FirstName = "Frank", LastName = "Melmon", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 48, FirstName = "Enrico", LastName = "McSorley", JobTitle = "Blogger" });
            Persons.Add(new Person() { Id = 49, FirstName = "Correy", LastName = "Fox", JobTitle = "Analyst" });
            Persons.Add(new Person() { Id = 50, FirstName = "Jocelyn", LastName = "Oliva", JobTitle = "Analyst" });
            #endregion
        }

    }
}