using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakingCodeWithSingleMainThread
{
    internal class CodeBreakerProgress
    {
        // The char position in the 4 chars code
        private int priCharNumber;
        // The Unicode char code
        private int priCharCode;
        // The decoding process percentage completed
        private int priPercentageCompleted;
        public int CharNumber
        {
            get
            {
                return priCharNumber;
            }
            set
            {
                priCharNumber = value;
            }
        }
        public int CharCode
        {
            get
            {
                return priCharCode;
            }
            set
            {
                priCharCode = value;
            }
        }

        public int PercentageCompleted
        {
            get
            {
                return priPercentageCompleted;
            }
            set
            {
                priPercentageCompleted = value;
            }
        }
    }
}

