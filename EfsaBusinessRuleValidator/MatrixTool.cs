using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EfsaBusinessRuleValidator
{
    class MatrixTool
    {


        Dictionary<Tuple<string, string>, string> _d = new Dictionary<Tuple<string, string>, string>();

        public MatrixTool()
        {
            
            var tablea = ReadLines(() => Assembly.GetExecutingAssembly()
                                    .GetManifestResourceStream("tablea"),
                      Encoding.UTF8)
                .ToList();
            _d = tablea.Select(l => l.Split(',')).ToDictionary(l => Tuple.Create(l[0], l[1]), l => l[2]);
        }

        /// <summary>
        /// Använder resursfilen tablea som är en stor csv med kolumnerna produkt, param och partyp. 
        /// </summary>
        /// <param name="prodcode"></param>
        /// <param name="paramcode"></param>
        /// <returns></returns>
        public string GetPartypFromTableA(string prodcode, string paramcode)
        {
            string partyp;
            _d.TryGetValue(Tuple.Create(prodcode, paramcode), out partyp);
            return partyp;
        }
        /// <summary>
        /// A lazy mans extensionmethod for reading all lines. #Jon Skeet
        /// </summary>
        /// <param name="streamProvider"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public IEnumerable<string> ReadLines(Func<Stream> streamProvider,
                                     Encoding encoding)
        {
            using (var stream = streamProvider())
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
