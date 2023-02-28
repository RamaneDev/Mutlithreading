using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public class Consumer
    {
        Cell cell;
        int quantity = 1;
        public Consumer(Cell box, int request)
        {
            cell = box;
            quantity = request;
        }
        public void ThreadRun()
        {
            int value;
            for (int looper = 1; looper <= quantity; looper++)
                value = cell.Read(); // Consume the result by putting it in value
        }
    }
}
