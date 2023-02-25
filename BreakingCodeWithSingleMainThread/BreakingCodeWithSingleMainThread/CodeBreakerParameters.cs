using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakingCodeWithSingleMainThread
{
    internal class CodeBreakerParameters
    {
        // The first char position in the 4 chars code to 
        // process
        private int priFirstCharNumber;
        
        // The last char position in the 4 chars code to 
        // process
        private int priLastCharNumber;
        
        // The maximum number of the Unicode character
        private int priMaxUnicodeCharCode;
        public int FirstCharNumber
        {
            get
            {
                return priFirstCharNumber;
            }
            set
            {
                priFirstCharNumber = value;
            }
        }
        public int LastCharNumber
        {
            get
            {
                return priLastCharNumber;
            }
            set
            {
                priLastCharNumber = value;
            }
        }
        public int MaxUnicodeCharCode
        {
            get
            {
                return priMaxUnicodeCharCode;
            }
            set
            {
                priMaxUnicodeCharCode = value;
            }
        }
    }
}

