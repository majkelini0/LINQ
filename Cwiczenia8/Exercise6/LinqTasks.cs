﻿using System.Xml.Linq;
using Exercise6.Models;

namespace Exercise6
{
    public static class LinqTasks
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        static LinqTasks()
        {
            #region Load depts

            List<Dept> depts =
            [
                new Dept
                {
                    Deptno = 1,
                    Dname = "Research",
                    Loc = "Warsaw"
                },
                new Dept
                {
                    Deptno = 2,
                    Dname = "Human Resources",
                    Loc = "New York"
                },
                new Dept
                {
                    Deptno = 3,
                    Dname = "IT",
                    Loc = "Los Angeles"
                }
            ];

            Depts = depts;

            #endregion

            #region Load emps

            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            List<Emp> emps =
            [
                e1, e2, e3, e4, e5, e6, e7, e8, e9, e10
            ];

            Emps = emps;

            #endregion
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public static IEnumerable<Emp> Task1()
        {
            // Method syntax
            var methodSyntax = Emps.Where(x => x.Job == "Backend programmer");
            
            // Query syntax
            var querySyntax = from x in Emps where x.Job == "Backend programmer" select x;
            
            IEnumerable<Emp> result = methodSyntax;
            return result;
        }

        /// <summary>
        ///     SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public static IEnumerable<Emp> Task2()
        {
            var methodSyntax = Emps
                .Where(x => x.Job == "Frontend programmer" && x.Salary > 1000)
                .OrderByDescending(x => x.Ename);
            
            IEnumerable<Emp> result = methodSyntax;
            return result;
        }


        /// <summary>
        ///     SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public static int Task3()
        {
            var methodSyntax = Emps.Max(x => x.Salary);

            int result = methodSyntax;
            return result;
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public static IEnumerable<Emp> Task4()
        {
            var methodSyntax = Emps
                .Where(x => x.Salary == Emps.Max(x => x.Salary));

            IEnumerable<Emp> result = methodSyntax;
            return result;
        }

        /// <summary>
        ///    SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public static IEnumerable<object> Task5()
        {
            var methodSyntax = Emps
                .Select(x => new { Nazwisko = x.Ename, Praca = x.Job });
            
            IEnumerable<object> result = methodSyntax;
            return result;
        }

        /// <summary>
        ///     SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        ///     INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        ///     Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public static IEnumerable<object> Task6()
        {
            var methodSyntax = Emps.Join(
                Depts,
                Emps => Emps.Deptno,
                Depts => Depts.Deptno,
                (Emps, Depts) => new
                {
                    Emps.Ename, Emps.Job, Depts.Dname
                }
                );

            IEnumerable<object> result = methodSyntax;
            return result;
        }

        /// <summary>
        ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public static IEnumerable<object> Task7()
        {
            var methodSyntax = Emps
                .GroupBy(x => x.Job)
                .Select(x => new {Praca = x.Key, LiczbaPracownikow = x.Count()});
            
            IEnumerable<object> result = methodSyntax;
            return result;
        }

        /// <summary>
        ///     Zwróć wartość "true" jeśli choć jeden
        ///     z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public static bool Task8()
        {
            var methodSyntax = Emps
                .Select(x => x.Job)
                .Contains("Backend programmer");
            
            bool result = methodSyntax;
            return result;
        }

        /// <summary>
        ///     SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        ///     ORDER BY HireDate DESC;
        /// </summary>
        public static Emp Task9()
        {
            var methodSyntax = Emps
                .Where(x => x.Job == "Frontend programmer")
                .OrderByDescending(x => x.HireDate)
                .FirstOrDefault();
                
            Emp result = methodSyntax;
            return result;
        }

        /// <summary>
        ///     SELECT Ename, Job, Hiredate FROM Emps
        ///     UNION
        ///     SELECT "Brak wartości", null, null;
        /// </summary>
        public static IEnumerable<object> Task10()
        {
            var methodSyntax = Emps
                .Select(x => new { x.Ename, x.Job, x.HireDate })
                .Union(new[] { new { Ename = "Brak wartości", Job = (string)null, HireDate = (DateTime?)null } });
            
            IEnumerable<object> result = methodSyntax;
            return result;
        }

        /// <summary>
        /// Wykorzystując LINQ pobierz pracowników podzielony na departamenty pamiętając, że:
        /// 1. Interesują nas tylko departamenty z liczbą pracowników powyżej 1
        /// 2. Chcemy zwrócić listę obiektów o następującej srukturze:
        ///    [
        ///      {name: "RESEARCH", numOfEmployees: 3},
        ///      {name: "SALES", numOfEmployees: 5},
        ///      ...
        ///    ]
        /// 3. Wykorzystaj typy anonimowe
        /// </summary>
        public static IEnumerable<object> Task11()
        {
            // var methodSyntax = Emps
            //     .GroupBy(x => x.Deptno)
            //     .Where(x => x.Count() > 1)
            //     .Select(x => new { name = x.Key, numOfEmployees = x.Count() })
            //     .ToList();
            var methodSyntax = Emps
                .Join(
                    Depts,
                    Emps => Emps.Deptno,
                    Depts => Depts.Deptno,
                    (Emps, Depts) => new { Depts.Dname, Emps })
                .GroupBy(x => x.Dname)
                .Where(x => x.Count() > 1)
                .Select(x => new { name = x.Key, numOfEmployees = x.Count() });

            IEnumerable<object> result = methodSyntax;
            return result;
        }

        /// <summary>
        /// Napisz własną metodę rozszerzeń, która pozwoli skompilować się poniższemu fragmentowi kodu.
        /// Metodę dodaj do klasy CustomExtensionMethods, która zdefiniowana jest poniżej.
        /// 
        /// Metoda powinna zwrócić tylko tych pracowników, którzy mają min. 1 bezpośredniego podwładnego.
        /// Pracownicy powinny w ramach kolekcji być posortowani po nazwisku (rosnąco) i pensji (malejąco).
        /// </summary>
        public static IEnumerable<Emp> Task12()
        {
            //var methodSyntax = 

            IEnumerable<Emp> result = Emps.Task12();
            return result;
        }

        /// <summary>
        /// Poniższa metoda powinna zwracać pojedyczną liczbę int.
        /// Na wejściu przyjmujemy listę liczb całkowitych.
        /// Spróbuj z pomocą LINQ'a odnaleźć tę liczbę, które występuja w tablicy int'ów nieparzystą liczbę razy.
        /// Zakładamy, że zawsze będzie jedna taka liczba.
        /// Np: {1,1,1,1,1,1,10,1,1,1,1} => 10
        /// </summary>
        public static int Task13(int[] arr)
        {
            int result = arr.GroupBy(x => x)
                .First(group => group.Count() % 2 != 0)
                .Key;

            return result;
        }

        /// <summary>
        /// Zwróć tylko te departamenty, które mają 5 pracowników lub nie mają pracowników w ogóle.
        /// Posortuj rezultat po nazwie departament rosnąco.
        /// </summary>
        public static IEnumerable<Dept> Task14()
        {
            var methodSyntax = Depts
                .Select(dept => new 
                {
                    Dept = dept,
                    EmployeeCount = Emps.Count(emp => emp.Deptno == dept.Deptno)
                })
                .Where(x => x.EmployeeCount == 5 || x.EmployeeCount == 0)
                .Select(x => x.Dept)
                .OrderBy(x => x.Dname);
            
            IEnumerable<Dept> result = methodSyntax;
            return result;
        }
    }

    public static class CustomExtensionMethods
    {
        public static IEnumerable<Emp> Task12(this IEnumerable<Emp> empss)
        {
            // Metoda powinna zwrócić tylko tych pracowników, którzy mają min. 1 bezpośredniego podwładnego.
            // Pracownicy powinny w ramach kolekcji być posortowani po nazwisku (rosnąco) i pensji (malejąco).
            return empss
                .Where(manager => empss.Any(empl => empl.Mgr == manager))
                .OrderBy(x => x.Ename)
                .ThenByDescending(x => x.Salary);
        }
        //Put your extension methods here
    }
}