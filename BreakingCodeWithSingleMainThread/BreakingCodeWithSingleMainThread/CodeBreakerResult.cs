using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakingCodeWithSingleMainThread
{
    internal class CodeBreakerResult
    {
        // The first char position in the 4 chars code to 
        // process
        private int priFirstCharNumber;
        // The last char position in the 4 chars code to 
        // process
        private int priLastCharNumber;
        // The part of the broken code 
        private string prsBrokenCode;
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
        public string BrokenCode
        {
            get
            {
                return prsBrokenCode;
            }


            set
            {
                prsBrokenCode = value;
            }
        }
    }

}









