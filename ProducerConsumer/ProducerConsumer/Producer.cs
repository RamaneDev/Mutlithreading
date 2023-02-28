using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public class Producer
    {
        Cell cell;
        int quantity = 1;
        public Producer(Cell box, int request)
        {
            cell = box;
            quantity = request;
        }
        public void ThreadRun()
        {
            for (int looper = 1; looper <= quantity; looper++)
                cell.Write(looper); // "producing"
        }
    }
}
