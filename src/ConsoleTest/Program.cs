//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Wonderful Components">
//     Copyright (c) Faculty of Informatics, University of Eötvös Loránd,
//     Budapest, Hungary. All rights reserved.
// </copyright>
// <author>David Hornyák</author>
//-----------------------------------------------------------------------

namespace ConsoleTest
{
    using System;
    using System.Threading.Tasks;
    using DomainModel.Contracts;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;

    /// <summary>
    /// Represents the main program of the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Loads the configurations for Unity container and resolves the <see cref="IScoreStore"/> dependency.
        /// See App.config for more details.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            using (var container = new UnityContainer().LoadConfiguration())
            {
                var store = container.Resolve<IScoreStore>();
                var dependingClass = new ScoreDepedentClass(store);
                Task.Run(() => dependingClass.DemonstrateStore());
            }

            Console.ReadKey();
        }
    }
}
