using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quirky.net.Contracts;


namespace quirky.net.Entities.Util
{
    public class Resolver
    {
        /// <summary>
        /// crappy little IoC container
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <returns></returns>
        public static I Resolve<I>() where I : class
        {
            //if(typeof(I)  == typeof(IConfiguration)) return (new HardCodedConfiguration() as I);

            return default(I);
        }

    }
}
